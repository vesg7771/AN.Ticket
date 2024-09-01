using AN.Ticket.Application.DTOs;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Application.Services.Base;
using AN.Ticket.Domain.Interfaces.Base;
using DomainEntity = AN.Ticket.Domain.Entities;

namespace AN.Ticket.Application.Services;
public class TicketService : Service<TicketDto, DomainEntity.Ticket>, ITicketService
{
    private readonly IRepository<DomainEntity.Ticket> _ticketRepository;


    public TicketService(
        IRepository<DomainEntity.Ticket> repository
    )
        : base(repository)
    {
        _ticketRepository = repository;
    }
}
