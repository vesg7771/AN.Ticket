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

    public async Task<IEnumerable<Team>> GetAllByUserId(Guid userId)
    {
        return await Entities
            .Include(t => t.Members)
            .Where(t => t.Members.Any(m => m.Id == userId))
            .ToListAsync();
    }
}
