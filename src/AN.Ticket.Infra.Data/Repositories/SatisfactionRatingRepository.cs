using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Infra.Data.Context;
using AN.Ticket.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AN.Ticket.Infra.Data.Repositories;
public class SatisfactionRatingRepository
    : Repository<SatisfactionRating>, ISatisfactionRatingRepository
{
    public SatisfactionRatingRepository(ApplicationDbContext context)
        : base(context)
    { }

    public async Task<SatisfactionRating?> GetByTicketIdAsync(Guid ticketId)
    {
        return await Entities
            .FirstOrDefaultAsync(x => x.TicketId == ticketId);
    }

    public async Task<bool> ExistsByTicketIdAsync(Guid ticketId)
    {
        return await Entities
            .AnyAsync(x => x.TicketId == ticketId);
    }
}
