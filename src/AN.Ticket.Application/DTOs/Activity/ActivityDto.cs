using AN.Ticket.Application.DTOs.Contact;
using AN.Ticket.Domain.Enums;

namespace AN.Ticket.Application.DTOs.Activity;
public class ActivityDto
{
    public Guid Id { get; set; }
    public string? Subject { get; set; }
    public ActivityType Type { get; set; }
    public string? Description { get; set; }
    public DateTime ScheduledDate { get; set; }
    public TimeSpan? Duration { get; set; }
    public ActivityPriority Priority { get; set; }
    public Guid? ContactId { get; set; }
    public ContactDto? Contact { get; set; }
}
