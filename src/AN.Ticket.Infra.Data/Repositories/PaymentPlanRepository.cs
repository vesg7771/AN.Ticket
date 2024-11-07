using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Infra.Data.Context;
using AN.Ticket.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AN.Ticket.Infra.Data.Repositories;
public class PaymentPlanRepository
    : Repository<PaymentPlan>, IPaymentPlanRepository
{
    public PaymentPlanRepository(ApplicationDbContext context)
        : base(context)
    { }

    public async Task<(IEnumerable<PaymentPlan> plans, int totalItems)> GetPaginatedPlansAsync(int pageNumber, int pageSize, string searchTerm = "")
    {
        var query = Entities.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(a =>
                a.Description.Contains(searchTerm) ||
                a.Value.ToString().Contains(searchTerm)
            );
        }

        var totalItems = await query.CountAsync();

        var plans = await query
            .OrderBy(p => p.Description)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (plans, totalItems);
    }
}
