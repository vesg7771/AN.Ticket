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
        public IActionResult CreatePaymentPlan(PaymentPlan paymentPlan)
        {
            //TODO:Implementar cadastro
            Console.WriteLine("Descrição:" + paymentPlan.Description);
            Console.WriteLine("Valor:" + paymentPlan.Value);
            return RedirectToAction("GetPaymentsPlans");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}