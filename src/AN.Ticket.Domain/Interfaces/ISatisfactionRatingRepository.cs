using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Domain.Interfaces;

public interface ISatisfactionRatingRepository
    : IRepository<SatisfactionRating>
{
    Task<bool> ExistsByTicketIdAsync(Guid ticketId);
    Task<SatisfactionRating?> GetByTicketIdAsync(Guid ticketId);
}
