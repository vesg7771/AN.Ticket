using AN.Ticket.Domain.Enums;

namespace AN.Ticket.Application.DTOs.Activity;

public class ActivitySummaryDto
{
    public Guid Id { get; set; }

    public string Subject { get; set; }

    public string? Description { get; set; }

    public DateTime ScheduledDate { get; set; }

    public ActivityStatus Status { get; set; }
}
