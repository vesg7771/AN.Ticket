using AN.Ticket.Domain.Enums;

namespace AN.Ticket.Application.DTOs;
public class TicketDto
{
    public Guid Id { get; set; }
    public string ContactName { get; set; }
    public string AccountName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Subject { get; set; }
    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ClosedAt { get; set; }
    public List<TicketMessageDto> Messages { get; set; }
    public List<ActivityDto> Activities { get; set; }
    public List<InteractionHistoryDto> InteractionHistories { get; set; }
    public SatisfactionRatingDto SatisfactionRating { get; set; }
    public DateTime? FirstResponseAt { get; set; }
}
