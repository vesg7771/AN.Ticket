using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Domain.Interfaces;

public interface ITeamRepository : IRepository<Team>
{
    Task<IEnumerable<Team>> GetAllIncludeMembersAsync();
    Task<Team> GetByIdIncludeMembersAsync(Guid teamId);
    Task<IEnumerable<Team>> GetAllByUserId(Guid userId);
    Task<(IEnumerable<User> Items, int TotalCount)> GetPagedTeamMembersAsync(Guid teamId, int pageNumber, int pageSize, string searchTerm = "");
}
