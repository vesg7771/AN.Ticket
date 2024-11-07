using AN.Ticket.Application.DTOs.PaymantPlan;

namespace AN.Ticket.WebUI.ViewModels.PaymantPlan;

public class PaymantPlanListViewModel
{
    public IEnumerable<PaymantPlanDto> PaymentPlans { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public string SearchTerm { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}
