using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AN.Ticket.Application.DTOs.PaymantPlan;
using AN.Ticket.Application.Helpers.Pagination;
using AN.Ticket.Application.Interfaces.Base;
using AN.Ticket.Domain.Entities;

namespace AN.Ticket.Application.Interfaces
{
    public interface IPaymantPlanService: IService<PaymantPlanDto, PaymentPlan>
    {
        new Task<IEnumerable<PaymantPlanDto>> GetAllAsync();
        Task<bool> CreateAsync(PaymantPlanDto paymentPlan);
        Task<bool> DeleteAsync(Guid guid);
        Task<bool> UpdateAsync(PaymantPlanDto paymentPlan);
        Task<PagedResult<PaymantPlanDto>> GetPaginatedPlansAsync(int pageNumber, int pageSize, string searchTerm = "");
    }
}