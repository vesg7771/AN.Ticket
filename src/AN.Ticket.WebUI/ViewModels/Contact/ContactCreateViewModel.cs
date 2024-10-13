using AN.Ticket.Application.DTOs.Contact;
using AN.Ticket.Application.DTOs.PaymantPlan;

namespace AN.Ticket.WebUI.ViewModels.Contact;

public class ContactCreateViewModel
{
    public ContactCreateDto Contact { get; set; }
    public List<PaymantPlanDto> PaymentPlans { get; set; }
    public bool IsEditedContact { get; set; }

    public ContactCreateViewModel()
    {
        PaymentPlans = new List<PaymantPlanDto>();
    }
}
