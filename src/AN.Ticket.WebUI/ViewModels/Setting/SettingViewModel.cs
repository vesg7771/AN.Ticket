using AN.Ticket.Application.DTOs.PaymantPlan;
using AN.Ticket.WebUI.ViewModels.Account;

namespace AN.Ticket.WebUI.ViewModels.Setting;

public class SettingViewModel
{
    public bool tabActive { get; set; }
    public List<PaymantPlanDto> PaymentPlans { get; set; }
    public SecuritySettingViewModel SecuritySetting { get; set; }
}
