using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Infra.Data.Context;
using AN.Ticket.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AN.Ticket.Infra.Data.Repositories;

public class TeamRepository : Repository<Team>, ITeamRepository
{
    public TeamRepository(ApplicationDbContext context)
        : base(context)
    { }

    public async Task<IEnumerable<Team>> GetAllIncludeMembersAsync()
    {
        return await Entities
            .Include(t => t.Members)
            .ToListAsync();
    }

    public async Task<IEnumerable<Team>> GetAllByUserId(Guid userId)
    {
        return await Entities
            .Include(t => t.Members)
            .Where(t => t.Members.Any(m => m.Id == userId))
            .ToListAsync();
    }

    public async Task<(IEnumerable<User> Items, int TotalCount)> GetPagedTeamMembersAsync(Guid teamId, int pageNumber, int pageSize, string searchTerm = "")
    {
        var query = Entities
            .Include(t => t.Members)
            .Where(t => t.Id == teamId)
            .SelectMany(t => t.Members)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(m => m.FullName.Contains(searchTerm) || m.Email.Contains(searchTerm));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(m => m.FullName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<Team> GetByIdIncludeMembersAsync(Guid teamId)
    {
        return await Entities
            .Include(t => t.Members)
            .FirstOrDefaultAsync(t => t.Id == teamId);
    }
}
