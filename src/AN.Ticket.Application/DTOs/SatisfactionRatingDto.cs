using AN.Ticket.Domain.Enums;

namespace AN.Ticket.Application.DTOs;
public class SatisfactionRatingDto
{
    public Guid Id { get; set; }
    public SatisfactionRatingValue? Rating { get; set; }
    public string? Comment { get; set; }
    public Guid TicketId { get; set; }
}
