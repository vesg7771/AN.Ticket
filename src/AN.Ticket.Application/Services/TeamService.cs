using AN.Ticket.Application.DTOs.Team;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Application.Services.Base;
using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Domain.Interfaces.Base;
using AutoMapper;

namespace AN.Ticket.Application.Services;
public class TeamService : Service<TeamDto, Team>, ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMapper _mapper;

    public TeamService(
        IRepository<Team> repository,
        ITeamRepository teamRepository,
        IMapper mapper
    )
        : base(repository)
    {
        _teamRepository = teamRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
    {
        var teams = await GetAllAsync();
        return _mapper.Map<IEnumerable<TeamDto>>(teams);
    }

    public async Task<IEnumerable<TeamDto>> GetAllByUserId(Guid userId)
    {
        var teams = await _teamRepository.GetAllByUserId(userId);
        return _mapper.Map<IEnumerable<TeamDto>>(teams);
    }
}
