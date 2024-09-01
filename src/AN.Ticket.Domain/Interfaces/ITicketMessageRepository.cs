using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Domain.Interfaces;
public interface ITicketMessageRepository
    : IRepository<TicketMessage>
{
    Task SaveListAsync(List<TicketMessage> ticketMessages);
}
