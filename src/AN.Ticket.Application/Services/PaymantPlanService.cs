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
    public class PaymantPlanService : Service<PaymantPlanDto, PaymentPlan>, IPaymantPlanService
    {
        private readonly IPaymentPlanRepository _paymantPlanRepositorie;
        private readonly IUnitOfWork _unitOfWork;
        public PaymantPlanService(
            IRepository<PaymentPlan> repository,
            IPaymentPlanRepository paymantPlanRepositorie,
            IUnitOfWork unitOfWork
        )
        : base(repository)
        {
            _paymantPlanRepositorie = paymantPlanRepositorie;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateAsync(PaymantPlanDto paymentPlanDto)
        {
            try
            {
                PaymentPlan paymentPlan = new PaymentPlan(
                    description: paymentPlanDto.Description,
                    value: paymentPlanDto.Value
                );
                await _paymantPlanRepositorie.SaveAsync(paymentPlan);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<bool> DeleteAsync(Guid guid)
        {
            try
            {
                PaymentPlan paymantPlan = await _paymantPlanRepositorie.GetByIdAsync(guid);
                _paymantPlanRepositorie.Delete(paymantPlan);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public new async Task<IEnumerable<PaymantPlanDto>> GetAllAsync()
        {
            var listPaymantsPlan = await _paymantPlanRepositorie.GetAllAsync();

            return listPaymantsPlan
            .Select(plan => new PaymantPlanDto
            {
                Id = plan.Id,
                Description = plan.Description,
                Value = plan.Value
            }).ToList();
        }

        public Task<PaymantPlanDto> GetGidAsync(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(PaymantPlanDto paymentPlanDto)
        {
            PaymentPlan paymentPlan = await _paymantPlanRepositorie.GetByIdAsync(paymentPlanDto.Id);
             _paymantPlanRepositorie.Update(paymentPlan);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}