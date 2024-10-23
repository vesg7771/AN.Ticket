namespace AN.Ticket.Application.DTOs.PaymantPlan;
public class PaymentActivityDto
{
    public Guid PaymentId { get; set; }
    public string Code { get; set; }
    public string Status { get; set; }
    public string Method { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
}
