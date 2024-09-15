using AN.Ticket.Domain.Enums;

namespace AN.Ticket.Application.DTOs.Ticket;

public class EditTicketDto
{
    public Guid Id { get; set; }
    public string Subject { get; set; }
    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }
    public DateTime DueDate { get; set; }
    public Guid ContactId { get; set; }
    public string ContactName { get; set; }
    public string AccountName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public int TicketCode { get; set; }
    public int TotalMessages { get; set; }
    public int TotalActivities { get; set; }
    public int TotalAttachments { get; set; }
    public int SatisfactionStars { get; set; }
}
