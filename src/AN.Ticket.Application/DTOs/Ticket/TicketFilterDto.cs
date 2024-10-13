using AN.Ticket.Domain.Enums;

namespace AN.Ticket.Application.DTOs.Ticket;
public class TicketFilterDto
{
    public TicketStatus? Status { get; set; }
    public TicketPriority? Priority { get; set; }
    public string ContactName { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}