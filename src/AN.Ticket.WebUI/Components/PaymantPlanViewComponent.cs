using AN.Ticket.Application.Interfaces;
using AN.Ticket.WebUI.ViewModels.PaymantPlan;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.WebUI.Components;

[ViewComponent]
public class PaymantPlanViewComponent : ViewComponent
{
    private readonly IPaymantPlanService _paymentPlanService;

    public PaymantPlanViewComponent(
        IPaymantPlanService paymentPlanService
    )
        => _paymentPlanService = paymentPlanService;

    public async Task<IViewComponentResult> InvokeAsync(int pageNumber = 1, int pageSize = 10, string searchTerm = "")
    {
        var paginatedPlans = await _paymentPlanService.GetPaginatedPlansAsync(pageNumber, pageSize, searchTerm);

        var viewModel = new PaymantPlanListViewModel
        {
            PaymentPlans = paginatedPlans.Items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = paginatedPlans.TotalItems,
            SearchTerm = searchTerm
        };

        return View(viewModel);
    }
}
