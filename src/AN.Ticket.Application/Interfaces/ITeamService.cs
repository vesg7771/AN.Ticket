using AN.Ticket.Application.DTOs.Team;
using AN.Ticket.Application.DTOs.User;
using AN.Ticket.Application.Helpers.Pagination;
using AN.Ticket.Application.Interfaces.Base;
using AN.Ticket.Domain.Entities;

namespace AN.Ticket.Application.Interfaces;

public interface ITeamService : IService<TeamDto, Team>
{
    Task<IEnumerable<TeamDto>> GetAllTeamsAsync();
    Task<IEnumerable<TeamDto>> GetAllByUserId(Guid userId);
    Task<PagedResult<UserDto>> GetPagedTeamMembersAsync(Guid teamId, int pageNumber, int pageSize, string searchTerm = "");
    Task<bool> CreateTeamAsync(TeamDto teamDto);
    Task<bool> AddMembersToTeamAsync(Guid teamId, List<Guid> userIds);
    Task<bool> RemoveMemberFromTeamAsync(Guid teamId, Guid userId);
    Task<bool> RemoveMembersFromTeamAsync(Guid teamId, List<Guid> userIds);
    Task<bool> DeleteTeamAsync(Guid teamId);
}
