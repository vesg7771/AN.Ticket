using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Domain.Interfaces;
public interface IPaymentRepository
    : IRepository<Payment>
{
    Task<IEnumerable<Payment>> GetByContactIdAsync(Guid contactId);
    Task<IEnumerable<Payment>> GetByContacts(List<Guid> contactIds);
    Task<Guid?> GetPaymentPlanIdByContactIdAsync(Guid contactId);
}
