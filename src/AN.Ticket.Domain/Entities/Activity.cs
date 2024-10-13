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
    public ActivityStatus Status { get; private set; }
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
        ActivityPriority priority = ActivityPriority.Low,
        ActivityStatus status = ActivityStatus.Open,
        Guid? contactId = null
    )
    {
        if (scheduledDate == default) throw new ArgumentException("ScheduledDate must be a valid date.", nameof(scheduledDate));
        if (ticketId == Guid.Empty) throw new ArgumentException("TicketId cannot be empty.", nameof(ticketId));

        Type = type;
        Description = description;
        ScheduledDate = scheduledDate;
        TicketId = ticketId;
        Subject = subject;
        Duration = duration;
        Priority = priority;
        Status = status;
        ContactId = contactId;
    }

    public void UpdateDescription(string newDescription)
    {
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

    public void UpdateActivity(
        string? newSubject,
        ActivityType newType,
        string? newDescription,
        DateTime newScheduledDate,
        TimeSpan? newDuration,
        ActivityPriority newPriority,
        Guid? newContactId
    )
    {
        UpdateSubject(newSubject);
        UpdateType(newType);
        UpdateDescription(newDescription ?? Description);
        UpdateScheduledDate(newScheduledDate);
        UpdateDuration(newDuration ?? Duration.GetValueOrDefault());
        UpdatePriority(newPriority);
        UpdateContact(newContactId ?? ContactId);
    }

    public void UpdateStatus(ActivityStatus newStatus)
        => Status = newStatus;

    public void CloseActivity()
        => Status = ActivityStatus.Closed;
}

