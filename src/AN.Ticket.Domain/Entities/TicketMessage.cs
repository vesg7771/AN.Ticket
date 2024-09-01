using AN.Ticket.Domain.Entities.Base;

namespace AN.Ticket.Domain.Entities;
public class TicketMessage
    : EntityBase
{
    public Guid TicketId { get; set; }
    public Ticket? Ticket { get; set; }
    public string? Message { get; set; }
    public Guid? UserId { get; set; }
    public User? User { get; set; }
    public DateTime? SentAt { get; set; }

    protected TicketMessage() { }

    public TicketMessage(
        //Guid userId,
        string message,
        DateTime sentAt
    )
    {
        //UserId = userId;
        Message = message;
        SentAt = sentAt == DateTime.MinValue ? DateTime.Now : sentAt;
    }
}
