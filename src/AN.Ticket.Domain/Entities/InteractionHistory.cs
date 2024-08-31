using AN.Ticket.Domain.Entities.Base;

namespace AN.Ticket.Domain.Entities;

public class InteractionHistory : EntityBase
{
    public DateTime InteractionDate { get; private set; }
    public string Description { get; private set; }
    public Guid TicketId { get; private set; }
    public Ticket Ticket { get; private set; }
    public User User { get; private set; }

    protected InteractionHistory() { }

    public InteractionHistory(
        DateTime interactionDate,
        string description,
        Guid ticketId,
        User user
    )
    {
        if (interactionDate == default) throw new ArgumentException("InteractionDate must be a valid date.", nameof(interactionDate));
        if (string.IsNullOrEmpty(description)) throw new ArgumentNullException(nameof(description), "Description cannot be null or empty.");
        if (ticketId == Guid.Empty) throw new ArgumentException("TicketId cannot be empty.", nameof(ticketId));
        if (user == null) throw new ArgumentNullException(nameof(user), "User cannot be null.");

        InteractionDate = interactionDate;
        Description = description;
        TicketId = ticketId;
        User = user;
    }

    public void UpdateDescription(string newDescription)
    {
        if (string.IsNullOrEmpty(newDescription)) throw new ArgumentNullException(nameof(newDescription), "New description cannot be null or empty.");
        Description = newDescription;
    }

    public void UpdateInteractionDate(DateTime newInteractionDate)
    {
        if (newInteractionDate == default) throw new ArgumentException("NewInteractionDate must be a valid date.", nameof(newInteractionDate));
        InteractionDate = newInteractionDate;
    }
}

