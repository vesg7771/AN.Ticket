using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Infra.Data.Context;
using AN.Ticket.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AN.Ticket.Infra.Data.Repositories;
public class ActivityRepository
    : Repository<Activity>, IActivityRepository
{
    public ActivityRepository(ApplicationDbContext context)
        : base(context)
    { }

    public async Task<(IEnumerable<Activity> Items, int TotalCount)> GetPaginatedActivitiesAsync(
        int pageNumber,
        int pageSize,
        string searchTerm = ""
    )
    {
        var query = Entities.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(a =>
                a.Subject.Contains(searchTerm) ||
                a.Description.Contains(searchTerm)
            );
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(a => a.ScheduledDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<List<Activity>> GetByIdsAsync(List<Guid> ids)
        => await Entities.Where(a => ids.Contains(a.Id)).ToListAsync();
}
