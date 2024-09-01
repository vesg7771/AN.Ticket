using AN.Ticket.Application.DTOs;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Application.Services.Base;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Domain.Interfaces.Base;
using DomainEntity = AN.Ticket.Domain.Entities;

namespace AN.Ticket.Application.Services;
public class TicketService : Service<TicketDto, DomainEntity.Ticket>, ITicketService
{
    private readonly IRepository<DomainEntity.Ticket> _ticketService;
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TicketService(
        IRepository<DomainEntity.Ticket> service,
        ITicketRepository ticketRepository,
        IUnitOfWork unitOfWork
    )
        : base(service)
    {
        _ticketService = service;
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TicketDto>> GetTicketsByUserIdAsync(Guid userId)
    {
        var userTickets = await _ticketRepository.GetTicketsByUserIdAsync(userId);

        return userTickets.Select(t => new TicketDto
        {
            Id = t.Id,
            ContactName = t.ContactName,
            AccountName = t.AccountName,
            Email = t.Email,
            Phone = t.Phone,
            Subject = t.Subject,
            Status = t.Status,
            Priority = t.Priority,
            DueDate = t.DueDate,
            ClosedAt = t.ClosedAt,
            FirstResponseAt = t.FirstResponseAt
        }).ToList();
    }

    public async Task<IEnumerable<TicketDto>> GetTicketsNotAssignedAsync()
    {
        var tickets = await _ticketRepository.GetTicketsNotAssignedAsync();

        return tickets.Select(t => new TicketDto
        {
            Id = t.Id,
            ContactName = t.ContactName,
            AccountName = t.AccountName,
            Email = t.Email,
            Phone = t.Phone,
            Subject = t.Subject,
            Status = t.Status,
            Priority = t.Priority,
            DueDate = t.DueDate,
            ClosedAt = t.ClosedAt,
            FirstResponseAt = t.FirstResponseAt
        }).ToList();
    }

    public async Task<bool> AssignTicketToUserAsync(Guid ticketId, Guid userId)
    {
        var ticket = await _ticketRepository.GetByIdAsync(ticketId);
        if (ticket is null || ticket.UserId != null)
        {
            return false;
        }

        ticket.AssignUsers(userId);
        _ticketRepository.Update(ticket);

        await _unitOfWork.CommitAsync();

        return true;
    }
}
