namespace AN.Ticket.Application.DTOs.Ticket;
public class TicketApprovalDto
{
    public bool IsApproved { get; set; }
    public string ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
}
