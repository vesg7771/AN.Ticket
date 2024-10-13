using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Infra.Data.Context;
using AN.Ticket.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AN.Ticket.Infra.Data.Repositories;
public class PaymentRepository
    : Repository<Payment>, IPaymentRepository
{
    public PaymentRepository(
        ApplicationDbContext context
    )
        : base(context)
    {
    }

    public async Task<IEnumerable<Payment>> GetByContacts(List<Guid> contactIds)
    {
        return await Entities
            .Where(p => contactIds.Contains(p.ContactId))
            .ToListAsync();
    }
}
