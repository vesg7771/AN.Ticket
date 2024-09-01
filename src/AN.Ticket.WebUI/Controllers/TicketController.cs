using AN.Ticket.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.WebUI.Controllers;
[Authorize]
public class TicketController : Controller
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    public async Task<IActionResult> Index()
    {
        var tickets = await _ticketService.GetAllAsync();
        return View(tickets);
    }
}
