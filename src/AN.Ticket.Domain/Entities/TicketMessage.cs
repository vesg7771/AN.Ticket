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
        string message,
        DateTime sentAt
    )
    {
        Message = message;
        SentAt = sentAt == DateTime.MinValue ? DateTime.Now : sentAt;
    }

    public void AssignTicket(Guid ticketId)
    {
        if (ticketId == Guid.Empty) throw new ArgumentException("Ticket ID cannot be empty.", nameof(ticketId));
        TicketId = ticketId;
    }

    public void AssignUser(Guid userId)
    {
        if (userId == Guid.Empty) throw new ArgumentException("User ID cannot be empty.", nameof(userId));
        UserId = userId;
    }
}
