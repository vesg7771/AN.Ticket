using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AN.Ticket.Application.DTOs.PaymantPlan;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Application.Services.Base;
using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Application.Services
{
    public class PaymantPlanService : Service<PaymantPlanDto, PaymentPlan>,IPaymantPlanService
    {
        private readonly IPaymentPlanRepository _paymantPlanRepositorie;
        public PaymantPlanService(
            IRepository<PaymentPlan> repository,
            IPaymentPlanRepository paymantPlanRepositorie
        )
        :base(repository)
        {
            _paymantPlanRepositorie=paymantPlanRepositorie;
        }
        public Task<bool> CreateAsync(PaymantPlanDto paymentPlan)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PaymantPlanDto>> GetAllAsync()
        {
            var listPaymantsPlan= await _paymantPlanRepositorie.GetAllAsync();
            return (IEnumerable<PaymantPlanDto>)listPaymantsPlan;
        }

        public Task<PaymantPlanDto> GetGidAsync(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(PaymantPlanDto paymentPlan)
        {
            throw new NotImplementedException();
        }
    }
}