using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Domain.Interfaces;
public interface IActivityRepository
    : IRepository<Activity>
{
    Task<List<Activity>> GetByIdsAsync(List<Guid> ids);
    Task<(IEnumerable<Activity> Items, int TotalCount)> GetPaginatedActivitiesAsync(int pageNumber, int pageSize, string searchTerm = "");
}
