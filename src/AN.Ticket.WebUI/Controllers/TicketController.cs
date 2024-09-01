using AN.Ticket.Application.Interfaces;
using AN.Ticket.Infra.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.WebUI.Controllers;

[Authorize]
public class TicketController : Controller
{
    private readonly ITicketService _ticketService;
    private readonly UserManager<ApplicationUser> _userManager;

    public TicketController(
        ITicketService ticketService,
        UserManager<ApplicationUser> userManager
    )
    {
        _ticketService = ticketService;
        _userManager = userManager;
    }

    public async Task<IActionResult> UserTickets()
    {
        var user = await GetCurrentUserAsync();
        if (user is null)
            return Unauthorized();

        var tickets = await _ticketService.GetTicketsByUserIdAsync(Guid.Parse(user.Id));
        return View(tickets.OrderByDescending(x => x.Priority));
    }

    public async Task<IActionResult> UnassignedTickets()
    {
        var tickets = await _ticketService.GetTicketsNotAssignedAsync();
        return View(tickets.OrderByDescending(x => x.Priority));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TakeTicket(Guid ticketId)
    {
        if (ticketId == Guid.Empty)
            return BadRequest("Ticket ID inválido");

        var user = await GetCurrentUserAsync();
        if (user is null)
            return Unauthorized();

        var success = await _ticketService.AssignTicketToUserAsync(ticketId, Guid.Parse(user.Id));

        if (!success)
        {
            return BadRequest("Não foi possível atribuir o ticket");
        }

        return RedirectToAction(nameof(UserTickets));
    }

    private async Task<ApplicationUser?> GetCurrentUserAsync()
    {
        return await _userManager.GetUserAsync(User);
    }
}
