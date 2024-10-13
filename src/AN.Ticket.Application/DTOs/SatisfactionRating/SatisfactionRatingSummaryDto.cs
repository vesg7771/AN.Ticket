using AN.Ticket.Domain.Enums;

namespace AN.Ticket.Application.DTOs.SatisfactionRating;
public class SatisfactionRatingSummaryDto
{
    public SatisfactionRatingValue Rating { get; set; }
    public string Comment { get; set; }
    public Guid TicketId { get; set; }
    public string TicketSubject { get; set; }
    public DateTime CreatedAt { get; set; }
}
