using AN.Ticket.Application.DTOs.Ticket;
using AN.Ticket.Application.Interfaces.Base;
using Microsoft.AspNetCore.Http;
using DomainEntity = AN.Ticket.Domain.Entities;

namespace AN.Ticket.Application.Interfaces;
public interface ITicketService
    : IService<TicketDto, DomainEntity.Ticket>
{
    Task<bool> AssignTicketToUserAsync(Guid ticketId, Guid userId);
    Task<bool> CreateTicketAsync(CreateTicketDto createTicket);
    Task<bool> ResolveTicketAsync(TicketResolutionDto ticketResolutionDto);
    Task<TicketDetailsDto> GetTicketDetailsAsync(Guid ticketId);
    Task<IEnumerable<TicketDto>> GetTicketsByUserIdAsync(Guid userId);
    Task<IEnumerable<TicketDto>> GetTicketsNotAssignedAsync();
    Task<bool> ReplyToTicketAsync(Guid ticketId, string messageText, Guid userId, List<IFormFile> attachments);
    Task<bool> DeleteTicketAsync(Guid ticketId);
    Task<bool> UpdateTicketAsync(EditTicketDto editTicketDto);
}
