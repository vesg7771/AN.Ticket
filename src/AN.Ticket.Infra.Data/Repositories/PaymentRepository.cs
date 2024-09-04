using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Infra.Data.Context;
using AN.Ticket.Infra.Data.Repositories.Base;

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
}
