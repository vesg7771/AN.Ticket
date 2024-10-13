using AN.Ticket.Domain.Enums;

namespace AN.Ticket.Application.DTOs.Ticket;
public class TicketSummaryDto
{
    public Guid Id { get; set; }
    public string Subject { get; set; }
    public string ContactName { get; set; }
    public TicketPriority Priority { get; set; }
    public DateTime DueDate { get; set; }
    public TicketStatus Status { get; set; }
}