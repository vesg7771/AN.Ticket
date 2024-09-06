using AN.Ticket.Application.DTOs.Ticket;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Application.Services.Base;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Domain.Interfaces.Base;
using AutoMapper;
using DomainEntity = AN.Ticket.Domain.Entities;

namespace AN.Ticket.Application.Services;
public class TicketService : Service<TicketDto, DomainEntity.Ticket>, ITicketService
{
    private readonly IRepository<DomainEntity.Ticket> _ticketService;
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TicketService(
        IRepository<DomainEntity.Ticket> service,
        ITicketRepository ticketRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
        : base(service)
    {
        _ticketService = service;
        _ticketRepository = ticketRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateTicketAsync(CreateTicketDto createTicket)
    {
        var ticket = new DomainEntity.Ticket(
            createTicket.Name,
            createTicket.AccountName,
            createTicket.Email,
            createTicket.Phone,
            createTicket.Subject,
            createTicket.Status,
            createTicket.DueDate,
            createTicket.Priority
        );

        if (createTicket.UserId != Guid.Empty)
            ticket.AssignUsers(createTicket.UserId);

        if (createTicket.AttachmentFile != null)
            ticket.SetAttachmentFile(createTicket.AttachmentFile);

        await _ticketRepository.SaveAsync(ticket);
        await _unitOfWork.CommitAsync();

        return true;
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
