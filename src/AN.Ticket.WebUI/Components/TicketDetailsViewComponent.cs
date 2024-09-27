using AN.Ticket.Application.Interfaces;
using AN.Ticket.WebUI.ViewModels.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.WebUI.Components;

[ViewComponent]
public class TicketDetailsViewComponent : ViewComponent
{
    private readonly ITicketService _ticketService;

    public TicketDetailsViewComponent(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    public async Task<IViewComponentResult> InvokeAsync(Guid ticketId)
    {
        if (ticketId == Guid.Empty)
        {
            return View(new TicketDetailViewModel());
        }

        var ticket = await _ticketService.GetTicketDetailsAsync(ticketId);

        if (ticket is null)
        {
            return View(new TicketDetailViewModel());
        }

        var viewModel = new TicketDetailViewModel
        {
            Id = ticket.Ticket.Id,
            TicketCode = ticket.Ticket.TicketCode,
            Subject = ticket.Ticket.Subject,
            Priority = ticket.Ticket.Priority,
            Status = ticket.Ticket.Status,
            CreatedAt = ticket.Ticket.CreatedAt,
            DueDate = ticket.Ticket.DueDate,
            ClosedAt = ticket.Ticket.ClosedAt,
            FirstResponseAt = ticket.Ticket.FirstResponseAt,
            UserId = ticket.Ticket.UserId,
            User = ticket.Ticket.User,
            Email = ticket.Ticket.Email,
            Phone = ticket.Ticket.Phone,
            Messages = ticket.Ticket.Messages
        };

        return View(viewModel);
    }
}
