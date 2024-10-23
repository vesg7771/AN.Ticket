using AN.Ticket.Domain.Enums;

namespace AN.Ticket.Application.DTOs.Ticket;
public class TicketContactDetailsDto
{
    public Guid TicketId { get; set; }
    public string TicketCode { get; set; }
    public string TicketTitle { get; set; }
    public TicketStatus TicketStatus { get; set; }
    public string TicketType { get; set; }
    public TicketPriority Priority { get; set; }
    public string AssignedTo { get; set; }
    public DateTime RequestDate { get; set; }
}
