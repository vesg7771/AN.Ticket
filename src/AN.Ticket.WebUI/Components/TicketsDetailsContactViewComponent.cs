using AN.Ticket.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.WebUI.Components;

[ViewComponent]
public class TicketsDetailsContactViewComponent : ViewComponent
{
    private readonly ITicketService _ticketService;

    public TicketsDetailsContactViewComponent(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    public async Task<IViewComponentResult> InvokeAsync(List<string> emails, bool showAll = false)
    {
        var tickets = await _ticketService.GetTicketsByContactIdAsync(emails, showAll);
        return View(tickets);
    }
}
