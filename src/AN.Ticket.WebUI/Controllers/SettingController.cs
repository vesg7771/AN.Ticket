using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AN.Ticket.Application.DTOs.PaymantPlan;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Application.Services;
using AN.Ticket.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace AN.Ticket.WebUI.Controllers
{
    public class SettingController : Controller
    {
        private readonly ILogger<SettingController> _logger;
        private readonly IPaymantPlanService _paymentPlanService;


        public SettingController(
            ILogger<SettingController> logger,
            IPaymantPlanService paymentPlanService
        )
        {
            _paymentPlanService = paymentPlanService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PaymentPlan()
        {
            try
            {
                var paymentPlans = await _paymentPlanService.GetAllAsync(); // Supondo que retorne List<PaymentPlan>

                var paymentsPlanDTOs = paymentPlans.Select(plan => new PaymantPlanDto
                {
                    Id = plan.Id,
                    Description = plan.Description,
                    Value = plan.Value
                }).ToList();

                return View(paymentsPlanDTOs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao acessar o método: {ex.Message}");
                return StatusCode(500, $"Erro interno do servidor. {ex.Message}");
            }
        }

        [HttpPost]
         public async Task<IActionResult> EditPaymentPlan(PaymantPlanDto paymentPlanDto){

             if (ModelState.IsValid)
            {
                try
                {
                    await _paymentPlanService.UpdateAsync(paymentPlanDto);
                    TempData["SuccessMessage"] = "Plano de pagamento criado com sucesso!";
                }
                catch (System.Exception error)
                {
                    TempData["ErrorMessage"] = $"Ocorreu um erro ao editar o plano de pagamento: {error.Message}"; 
                }

            }else{
                TempData["ErrorMessage"] = $"Ocorreu um erro ao tentar editar o plano de pagamento: Dados inválidos";
            }
            return RedirectToAction("PaymentPlan");
         }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentPlan(PaymantPlanDto paymentPlanDto)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _paymentPlanService.CreateAsync(paymentPlanDto);
                    TempData["SuccessMessage"] = "Plano de pagamento criado com sucesso!";
                }
                catch (System.Exception error)
                {
                    TempData["ErrorMessage"] = $"Ocorreu um erro ao criar o plano de pagamento: {error.Message}"; 
                }

            }else{
                TempData["ErrorMessage"] = $"Ocorreu um erro ao tentar criar o plano de pagamento: Dados inválidos";
            }
           
            return RedirectToAction("PaymentPlan");

        }

        //[HttpDelete]
        public async Task<IActionResult> DeletePaymentPlan(Guid id)
        {
            PaymentPlan paymantPlan = await _paymentPlanService.GetByIdAsync(id);

            if (paymantPlan.Id != Guid.Empty)
            {
                try
                {
                    bool suces = await _paymentPlanService.DeleteAsync(paymantPlan.Id);
                    TempData["SuccessMessage"] = "Plano de pagamento deletado com sucesso!";
                }
                catch (System.Exception error)
                {
                    TempData["ErrorMessage"] = $"Ocorreu um erro ao deletar o plano de pagamento: {error.Message}";
                }
            }
            else
            {
                TempData["ErrorMessage"] = $"Ocorreu um erro: Plano de pagamento não encontrado";
            }

            return RedirectToAction("PaymentPlan");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}