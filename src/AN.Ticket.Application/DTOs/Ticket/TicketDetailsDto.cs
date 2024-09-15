namespace AN.Ticket.Application.DTOs.Ticket;
public class TicketDetailsDto
{
    public TicketDto Ticket { get; set; }
    public TicketResolutionDto Resolution { get; set; }
    public TicketApprovalDto Approval { get; set; }
}
