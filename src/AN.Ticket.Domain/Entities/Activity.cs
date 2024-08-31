using AN.Ticket.Domain.Entities.Base;
using AN.Ticket.Domain.Enums;

namespace AN.Ticket.Domain.Entities;

public class Activity : EntityBase
{
    public ActivityType Type { get; private set; }
    public string Description { get; private set; }
    public DateTime ScheduledDate { get; private set; }
    public Guid TicketId { get; private set; }
    public Ticket Ticket { get; private set; }

    protected Activity() { }

    public Activity(
        ActivityType type,
        string description,
        DateTime scheduledDate,
        Guid ticketId
    )
    {
        if (string.IsNullOrEmpty(description)) throw new ArgumentNullException(nameof(description), "Description cannot be null or empty.");
        if (scheduledDate == default) throw new ArgumentException("ScheduledDate must be a valid date.", nameof(scheduledDate));
        if (ticketId == Guid.Empty) throw new ArgumentException("TicketId cannot be empty.", nameof(ticketId));

        Type = type;
        Description = description;
        ScheduledDate = scheduledDate;
        TicketId = ticketId;
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
}

