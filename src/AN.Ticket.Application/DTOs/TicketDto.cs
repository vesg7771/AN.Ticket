using AN.Ticket.Domain.Enums;

namespace AN.Ticket.Application.DTOs;
public class TicketDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public TicketStatus Status { get; private set; }
    public TicketPriority Priority { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ClosedAt { get; set; }
    public List<TicketMessageDto> Messages { get; set; }
}
