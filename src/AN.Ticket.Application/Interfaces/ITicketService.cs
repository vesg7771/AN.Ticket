using AN.Ticket.Application.DTOs.Ticket;
using AN.Ticket.Application.Interfaces.Base;
using DomainEntity = AN.Ticket.Domain.Entities;

namespace AN.Ticket.Application.Interfaces;
public interface ITicketService
    : IService<TicketDto, DomainEntity.Ticket>
{
    Task<bool> AssignTicketToUserAsync(Guid ticketId, Guid userId);
    Task<bool> CreateTicketAsync(CreateTicketDto createTicket);
    Task<IEnumerable<TicketDto>> GetTicketsByUserIdAsync(Guid userId);
    Task<IEnumerable<TicketDto>> GetTicketsNotAssignedAsync();
}
