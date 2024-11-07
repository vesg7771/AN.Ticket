using AN.Ticket.Application.DTOs.PaymantPlan;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Domain.Entities;
using AN.Ticket.WebUI.ViewModels.Setting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet]
        public IActionResult Index(SettingViewModel settingViewModel)
        {
            if (TempData["SettingViewModel"] != null)
            {
                settingViewModel = JsonConvert.DeserializeObject<SettingViewModel>(TempData["SettingViewModel"].ToString());
            }
            else
            {
                settingViewModel = new SettingViewModel();
            }

            return View(settingViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> PaymentPlan()
        {
            try
            {
                var paymentPlans = await _paymentPlanService.GetAllAsync();

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
                return StatusCode(500, $"Erro interno do servidor. {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditPaymentPlan(PaymantPlanDto paymentPlanDto)
        {

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

            }
            else
            {
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

            }
            else
            {
                TempData["ErrorMessage"] = $"Ocorreu um erro ao tentar criar o plano de pagamento: Dados inválidos";
            }

            return RedirectToAction("PaymentPlan");

        }

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