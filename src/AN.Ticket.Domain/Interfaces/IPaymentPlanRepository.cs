using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Domain.Interfaces;
public interface IPaymentPlanRepository
    : IRepository<PaymentPlan>
{
    Task<(IEnumerable<PaymentPlan> plans, int totalItems)> GetPaginatedPlansAsync(int pageNumber, int pageSize, string searchTerm = "");
}
