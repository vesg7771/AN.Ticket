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

    public async Task<IEnumerable<Payment>> GetByContactIdAsync(Guid contactId)
    {
        return await Entities
            .Where(p => p.ContactId == contactId)
            .Include(p => p.PaymentPlan)
            .ToListAsync();
    }

    public async Task<Guid?> GetPaymentPlanIdByContactIdAsync(Guid contactId)
    {
        if (contactId == Guid.Empty) throw new ArgumentException("ContactId cannot be empty.", nameof(contactId));

        var paymentPlanId = await Entities
            .Where(p => p.ContactId == contactId)
            .Select(p => p.PaymentPlanId)
            .FirstOrDefaultAsync();

        return paymentPlanId != Guid.Empty ? paymentPlanId : (Guid?)null;
    }
}
