using AN.Ticket.Domain.Entities.Base;
using AN.Ticket.Domain.Enums;

namespace AN.Ticket.Domain.Entities;

public class SatisfactionRating : EntityBase
{
    public SatisfactionRatingValue? Rating { get; private set; }
    public string? Comment { get; private set; }
    public Guid TicketId { get; private set; }
    public Ticket Ticket { get; private set; }

    public SatisfactionRating() { }

    public SatisfactionRating(
        SatisfactionRatingValue rating,
        Guid ticketId,
        string comment = null
    )
    {
        Rating = rating;
        TicketId = ticketId;
        Comment = comment;
    }

    public void UpdateRating(SatisfactionRatingValue rating, string comment = null)
    {
        Rating = rating;
        Comment = comment;
    }
}

