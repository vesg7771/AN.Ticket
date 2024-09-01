using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Infra.Data.Context;
using AN.Ticket.Infra.Data.Repositories.Base;

namespace AN.Ticket.Infra.Data.Repositories;
public class TicketMessageRepository
    : Repository<TicketMessage>, ITicketMessageRepository
{
    public TicketMessageRepository(
        ApplicationDbContext context
    )
        : base(context)
    {
    }

    public async Task SaveListAsync(List<TicketMessage> ticketMessages)
    {
        await Entities.AddRangeAsync(ticketMessages);
    }
}
