using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Infra.Data.Context;
using AN.Ticket.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using DomainEntity = AN.Ticket.Domain.Entities;

namespace AN.Ticket.Infra.Data.Repositories;

public class TicketRepository
    : Repository<DomainEntity.Ticket>, ITicketRepository
{
    public TicketRepository(
        ApplicationDbContext context
    )
        : base(context)
    {
    }

    public async Task<DomainEntity.Ticket> GetByEmailAndSubjectAsync(string email, string subject)
    {
        return await Entities
            .AsNoTracking()
            .Include(x => x.Messages)
            .FirstOrDefaultAsync(
                x => x.Email == email &&
                x.Subject == subject
            );
    }
}
