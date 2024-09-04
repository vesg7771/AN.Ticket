using AN.Ticket.Application.Interfaces;
using AN.Ticket.Infra.Data.Identity;
using AN.Ticket.WebUI.Components;
using AN.Ticket.WebUI.ViewModels.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.WebUI.Controllers;

[Authorize]
public class TicketController : Controller
{
    private readonly ITicketService _ticketService;
    private readonly IContactService _contactService;
    private readonly UserManager<ApplicationUser> _userManager;

    public TicketController(
        ITicketService ticketService,
        IContactService contactService,
        UserManager<ApplicationUser> userManager
    )
    {
        _ticketService = ticketService;
        _contactService = contactService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> UserTickets()
    {
        var user = await GetCurrentUserAsync();
        if (user is null)
            return Unauthorized();

        var tickets = await _ticketService.GetTicketsByUserIdAsync(Guid.Parse(user.Id));
        return View(tickets.OrderByDescending(x => x.Priority));
    }

    [HttpGet]
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

    [HttpGet]
    public IActionResult CreateTicket()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateTicket(CreateTicketViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ToastMessage"] = "Por favor, corrija os erros.";
            return View(model);
        }


        return RedirectToAction("Index");
    }


    public async Task<IActionResult> GetContactDetails(Guid contactId)
    {
        var contactDetailsViewComponent = new ContactDetailsViewComponent(_contactService);
        var result = await contactDetailsViewComponent.InvokeAsync(contactId);
        return ViewComponent("ContactDetails", new { contactId });
    }

    private async Task<ApplicationUser?> GetCurrentUserAsync()
    {
        return await _userManager.GetUserAsync(User);
    }
}
