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
    public string Description { get; private set; }
    public TicketStatus Status { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public DateTime DueDate { get; private set; }
    public TicketPriority Priority { get; private set; }
    public string Classification { get; private set; }
    public string AttachmentFile { get; private set; }
    public ICollection<Activity> Activities { get; private set; }
    public ICollection<InteractionHistory> InteractionHistories { get; private set; }
    public SatisfactionRating SatisfactionRating { get; private set; }
    public DateTime? FirstResponseAt { get; private set; }
    public DateTime? ClosedAt { get; private set; }

    protected Ticket() { }

    public Ticket(
        string contactName,
        string accountName,
        string email,
        string phone,
        string subject,
        string description,
        TicketStatus status,
        User user,
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
        Description = description;
        Status = status;
        User = user;
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
        if (user == null) throw new ArgumentNullException(nameof(user));
        User = user;
    }

    public void AddActivity(Activity activity)
    {
        if (activity == null) throw new ArgumentNullException(nameof(activity));
        Activities.Add(activity);
    }

    public void AddInteractionHistory(InteractionHistory history)
    {
        if (history == null) throw new ArgumentNullException(nameof(history));
        InteractionHistories.Add(history);
    }

    public void SetSatisfactionRating(SatisfactionRating rating)
    {
        if (rating == null) throw new ArgumentNullException(nameof(rating));
        SatisfactionRating = rating;
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

