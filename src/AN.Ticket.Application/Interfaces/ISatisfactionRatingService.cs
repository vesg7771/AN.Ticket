using AN.Ticket.Application.DTOs.SatisfactionRating;

namespace AN.Ticket.Application.Interfaces;
public interface ISatisfactionRatingService
{
    Task<SatisfactionRatingDto?> GetRatingByTicketIdAsync(Guid ticketId);
    Task SaveOrUpdateRatingAsync(SatisfactionRatingDto ratingDto);
}
