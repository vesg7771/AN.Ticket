using AN.Ticket.Application.DTOs.PaymantPlan;

namespace AN.Ticket.Application.DTOs.Contact;
public class ContactDetailsDto
{
    public Guid ContactId { get; set; }
    public string FullName { get; set; }
    public string Departament { get; set; }
    public string ProfileImageUrl { get; set; }
    public string Tag { get; set; }
    public DateTime? LastInteraction { get; set; }

    public int TotalTickets { get; set; }
    public int OnHoldTickets { get; set; }
    public TimeSpan AverageResponseTime { get; set; }
    public TimeSpan TotalResponseTime { get; set; }

    public string Source { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public string ResponseTimeStatus { get; set; }
    public string AssignedTo { get; set; }
    public DateTime FirstContactDate { get; set; }

    public List<PaymentActivityDto> PaymentActivities { get; set; }
}
