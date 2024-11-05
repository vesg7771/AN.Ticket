using AN.Ticket.Application.DTOs.Team;
using AN.Ticket.Application.DTOs.User;
using AN.Ticket.Application.Helpers.Pagination;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Application.Services.Base;
using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.EntityValidations;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Domain.Interfaces.Base;
using AutoMapper;

namespace AN.Ticket.Application.Services;
public class TeamService : Service<TeamDto, Team>, ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TeamService(
        IRepository<Team> repository,
        ITeamRepository teamRepository,
        IUserRepository userRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
        : base(repository)
    {
        _teamRepository = teamRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
    {
        var teams = await _teamRepository.GetAllIncludeMembersAsync();
        return _mapper.Map<IEnumerable<TeamDto>>(teams);
    }

    public async Task<IEnumerable<TeamDto>> GetAllByUserId(Guid userId)
    {
        var teams = await _teamRepository.GetAllByUserId(userId);
        return _mapper.Map<IEnumerable<TeamDto>>(teams);
    }

    public async Task<PagedResult<UserDto>> GetPagedTeamMembersAsync(Guid teamId, int pageNumber, int pageSize, string searchTerm = "")
    {
        var (members, totalItems) = await _teamRepository.GetPagedTeamMembersAsync(teamId, pageNumber, pageSize, searchTerm);

        var memberDtos = _mapper.Map<List<UserDto>>(members);

        return new PagedResult<UserDto>
        {
            Items = memberDtos,
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<bool> CreateTeamAsync(TeamDto teamDto)
    {
        if (!teamDto.Members.Any())
            throw new EntityValidationException("É preciso pelo menos um membro para criar a equipe");

        var team = new Team(teamDto.Name);
        var memberIds = teamDto.Members.Select(m => m.Id).ToList();
        var members = await _userRepository.GetAllByIds(memberIds);

        foreach (var member in members)
            team.AddMember(member);

        await _teamRepository.SaveAsync(team);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> UpdateTeamAsync(TeamDto teamDto)
    {
        var team = await _teamRepository.GetByIdAsync(teamDto.Id);

        if (team is null)
            throw new EntityValidationException("Equipe não encontrada");

        team.UpdateName(teamDto.Name);

        var memberIds = teamDto.Members.Select(m => m.Id).ToList();
        var members = await _userRepository.GetAllByIds(memberIds);

        foreach (var member in members)
            team.AddMember(member);

        await _teamRepository.SaveAsync(team);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> DeleteTeamAsync(Guid teamId)
    {
        var team = await _teamRepository.GetByIdIncludeMembersAsync(teamId);

        if (team is null)
            throw new EntityValidationException("Equipe não encontrada");

        team.ClearMembers();

        _teamRepository.Delete(team);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> AddMembersToTeamAsync(Guid teamId, List<Guid> userIds)
    {
        var team = await _teamRepository.GetByIdIncludeMembersAsync(teamId);
        if (team is null)
            throw new EntityValidationException("Equipe não encontrada");

        var users = await _userRepository.GetAllByIds(userIds);
        if (users.Count() != userIds.Count)
            throw new EntityValidationException("Um ou mais usuários não foram encontrados");

        foreach (var user in users)
        {
            if (!team.Members.Any(m => m.Id == user.Id))
                team.AddMember(user);
        }

        _teamRepository.Update(team);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> RemoveMembersFromTeamAsync(Guid teamId, List<Guid> userIds)
    {
        var team = await _teamRepository.GetByIdIncludeMembersAsync(teamId);
        if (team is null)
            throw new EntityValidationException("Equipe não encontrada");

        var users = await _userRepository.GetAllByIds(userIds);
        if (users.Count() != userIds.Count)
            throw new EntityValidationException("Um ou mais usuários não foram encontrados");

        foreach (var user in users)
            team.RemoveMember(user);

        _teamRepository.Update(team);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> RemoveMemberFromTeamAsync(Guid teamId, Guid userId)
    {
        var team = await _teamRepository.GetByIdIncludeMembersAsync(teamId);
        if (team is null)
            throw new EntityValidationException("Equipe não encontrada");

        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            throw new EntityValidationException("Usuário não encontrado");

        team.RemoveMember(user);

        _teamRepository.Update(team);
        await _unitOfWork.CommitAsync();

        return true;
    }
}
