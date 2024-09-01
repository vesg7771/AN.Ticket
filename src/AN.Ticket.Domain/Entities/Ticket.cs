using AN.Ticket.Domain.Entities.Base;
using AN.Ticket.Domain.Enums;

namespace AN.Ticket.Domain.Entities;

public class Ticket : EntityBase
{
    public string ContactName { get; private set; }
    public string AccountName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Subject { get; private set; }
    public ICollection<TicketMessage>? Messages { get; private set; }
    public TicketStatus Status { get; private set; }
    public Guid? UserId { get; private set; }
    public User? User { get; private set; }
    public DateTime DueDate { get; private set; }
    public TicketPriority Priority { get; private set; }
    public string? Classification { get; private set; }
    public string? AttachmentFile { get; private set; }
    public ICollection<Activity>? Activities { get; private set; }
    public ICollection<InteractionHistory>? InteractionHistories { get; private set; }
    public SatisfactionRating? SatisfactionRating { get; private set; }
    public DateTime? FirstResponseAt { get; private set; }
    public DateTime? ClosedAt { get; private set; }

    protected Ticket() { }

    public Ticket(
        string contactName,
        string accountName,
        string email,
        string phone,
        string subject,
        TicketStatus status,
        DateTime dueDate,
        TicketPriority priority,
        string classification = null,
        string attachmentFile = null
    )
    {
        ContactName = contactName;
        AccountName = accountName;
        Email = email;
        Phone = phone;
        Subject = subject;
        Status = status;
        DueDate = dueDate;
        Priority = priority;
        Classification = classification;
        AttachmentFile = attachmentFile;
        Activities = new List<Activity>();
        InteractionHistories = new List<InteractionHistory>();
        SatisfactionRating = new SatisfactionRating();
    }

    public void AssignUsers(User user)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));
        if (user.Id == Guid.Empty) throw new ArgumentException("User ID cannot be empty.", nameof(user));

        UserId = user.Id;
        User = user;
    }

    public void AddMessages(IEnumerable<TicketMessage> messages)
    {
        if (messages is null) throw new ArgumentNullException(nameof(messages));
        Messages ??= new List<TicketMessage>();

        foreach (var message in messages)
        {
            if (message is null) throw new ArgumentNullException(nameof(message));
            Messages.Add(message);
        }
    }

    public void AddActivity(Activity activity)
    {
        if (activity is null) throw new ArgumentNullException(nameof(activity));
        Activities ??= new List<Activity>();
        Activities.Add(activity);
    }

    public void AddInteractionHistory(InteractionHistory history)
    {
        if (history is null) throw new ArgumentNullException(nameof(history));
        InteractionHistories ??= new List<InteractionHistory>();
        InteractionHistories.Add(history);
    }

    public void SetSatisfactionRating(SatisfactionRating rating)
    {
        SatisfactionRating = rating ?? throw new ArgumentNullException(nameof(rating));
    }

    public void RecordFirstResponse()
    {
        if (FirstResponseAt == null)
        {
            FirstResponseAt = DateTime.UtcNow;
        }
    }

    public void CloseTicket()
    {
        Status = TicketStatus.Closed;
        ClosedAt = DateTime.UtcNow;
    }

    public TimeSpan? GetTimeToFirstResponse()
    {
        if (FirstResponseAt == null) return null;
        return FirstResponseAt - CreatedAt;
    }

    public TimeSpan? GetTimeToClose()
    {
        if (ClosedAt == null) return null;
        return ClosedAt - CreatedAt;
    }
}

