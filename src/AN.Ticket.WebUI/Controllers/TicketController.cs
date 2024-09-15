﻿using AN.Ticket.Application.DTOs.Ticket;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Infra.Data.Identity;
using AN.Ticket.WebUI.Components;
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
        {
            TempData["ErrorMessage"] = "Ticket ID inválido.";
            return RedirectToAction(nameof(UserTickets));
        }

        var user = await GetCurrentUserAsync();
        if (user is null)
            return Unauthorized();

        var success = await _ticketService.AssignTicketToUserAsync(ticketId, Guid.Parse(user.Id));

        if (!success)
        {
            TempData["ErrorMessage"] = "Não foi possível atribuir o ticket.";
            return RedirectToAction(nameof(UserTickets));
        }

        TempData["SuccessMessage"] = "Ticket atribuído com sucesso!";
        return RedirectToAction(nameof(UserTickets));
    }

    [HttpGet]
    public IActionResult CreateTicket() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTicket(CreateTicketDto model)
    {
        if (!ModelState.IsValid)
            return View(model);

        model.UserId = await GetCurrentUserId();
        var success = await _ticketService.CreateTicketAsync(model);

        if (!success)
        {
            TempData["ErrorMessage"] = "Não foi possível criar o ticket.";
            return View(model);
        }

        TempData["SuccessMessage"] = "Ticket criado com sucesso!";
        return RedirectToAction(nameof(UserTickets));
    }

    [HttpGet]
    [Route("Ticket/Details/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        if (id == Guid.Empty)
        {
            TempData["ErrorMessage"] = "ID do ticket inválido.";
            return RedirectToAction(nameof(UserTickets));
        }

        var ticket = await _ticketService.GetTicketDetailsAsync(id);
        if (ticket is null)
        {
            TempData["ErrorMessage"] = "Ticket não encontrado.";
            return RedirectToAction(nameof(UserTickets));
        }

        return View(ticket);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResolveTicket(TicketResolutionDto resolution)
    {
        if (!ModelState.IsValid)
        {
            var ticketDetails = await _ticketService.GetTicketDetailsAsync(resolution.TicketId);
            if (ticketDetails == null)
            {
                TempData["ErrorMessage"] = "Ticket não encontrado.";
                return RedirectToAction(nameof(UserTickets));
            }

            TempData["ErrorMessage"] = "Ops! Ocorreu um erro ao tentar salvar a resolução do Ticket. Verifique a aba resolução.";
            ticketDetails.Resolution = resolution;
            return View("Details", ticketDetails);
        }

        var success = await _ticketService.ResolveTicketAsync(resolution);

        if (!success)
        {
            TempData["ErrorMessage"] = "Não foi possível resolver o ticket. Por favor, tente novamente mais tarde.";
            return RedirectToAction(nameof(Details), new { id = resolution.TicketId });
        }

        TempData["SuccessMessage"] = "Ticket resolvido com sucesso!";
        return RedirectToAction(nameof(UserTickets));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ReplyToTicket(Guid ticketId, string responseText, List<IFormFile> attachments)
    {
        var user = await GetCurrentUserAsync();
        if (user == null)
        {
            return Unauthorized();
        }

        if (string.IsNullOrWhiteSpace(responseText))
        {
            TempData["ErrorMessage"] = "A mensagem de resposta não pode ser vazia.";
            return RedirectToAction(nameof(Details), new { id = ticketId });
        }

        var success = await _ticketService.ReplyToTicketAsync(ticketId, responseText, Guid.Parse(user.Id), attachments);

        if (!success)
        {
            TempData["ErrorMessage"] = "Não foi possível enviar a resposta. Por favor, tente novamente.";
            return RedirectToAction(nameof(Details), new { id = ticketId });
        }

        TempData["SuccessMessage"] = "Resposta enviada com sucesso!";
        return RedirectToAction(nameof(Details), new { id = ticketId });
    }


    [HttpGet]
    public async Task<IActionResult> GetContactDetails(Guid contactId)
    {
        var contactDetailsViewComponent = new ContactDetailsViewComponent(_contactService);
        var result = await contactDetailsViewComponent.InvokeAsync(contactId);
        return ViewComponent("ContactDetails", new { contactId });
    }

    private async Task<ApplicationUser?> GetCurrentUserAsync()
        => await _userManager.GetUserAsync(User);

    private async Task<Guid> GetCurrentUserId()
    {
        var user = await GetCurrentUserAsync();
        return user != null ? Guid.Parse(user.Id) : Guid.Empty;
    }
}
