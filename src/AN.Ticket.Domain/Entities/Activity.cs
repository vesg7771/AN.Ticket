using AN.Ticket.Domain.Entities.Base;
using AN.Ticket.Domain.Enums;

namespace AN.Ticket.Domain.Entities;

public class Activity : EntityBase
{
    public string? Subject { get; private set; }
    public ActivityType Type { get; private set; }
    public string? Description { get; private set; }
    public DateTime ScheduledDate { get; private set; }
    public TimeSpan? Duration { get; private set; }
    public ActivityPriority Priority { get; private set; }
    public Guid? ContactId { get; private set; }
    public Contact? Contact { get; private set; }
    public Guid? TicketId { get; private set; }
    public Ticket? Ticket { get; private set; }

    protected Activity() { }

    public Activity(
        ActivityType type,
        string description,
        DateTime scheduledDate,
        Guid ticketId,
        string? subject = null,
        TimeSpan duration = default,
        ActivityPriority priority = ActivityPriority.Normal,
        Guid? contactId = null
    )
    {
        if (string.IsNullOrEmpty(description)) throw new ArgumentNullException(nameof(description), "Description cannot be null or empty.");
        if (scheduledDate == default) throw new ArgumentException("ScheduledDate must be a valid date.", nameof(scheduledDate));
        if (ticketId == Guid.Empty) throw new ArgumentException("TicketId cannot be empty.", nameof(ticketId));

        Type = type;
        Description = description;
        ScheduledDate = scheduledDate;
        TicketId = ticketId;
        Subject = subject;
        Duration = duration;
        Priority = priority;
        ContactId = contactId;
    }

    public void UpdateDescription(string newDescription)
    {
        if (string.IsNullOrEmpty(newDescription)) throw new ArgumentNullException(nameof(newDescription), "New description cannot be null or empty.");
        Description = newDescription;
    }

    public void UpdateScheduledDate(DateTime newScheduledDate)
    {
        if (newScheduledDate == default) throw new ArgumentException("NewScheduledDate must be a valid date.", nameof(newScheduledDate));
        ScheduledDate = newScheduledDate;
    }

    public void UpdateType(ActivityType newType)
    {
        Type = newType;
    }

    public void UpdateSubject(string? newSubject)
    {
        Subject = newSubject;
    }

    public void UpdateDuration(TimeSpan newDuration)
    {
        Duration = newDuration;
    }

    public void UpdatePriority(ActivityPriority newPriority)
    {
        Priority = newPriority;
    }

    public void UpdateContact(Guid? newContactId)
    {
        ContactId = newContactId;
    }
}

