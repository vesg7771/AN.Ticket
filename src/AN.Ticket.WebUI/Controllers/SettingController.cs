using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AN.Ticket.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AN.Ticket.WebUI.Controllers
{
    public class SettingController : Controller
    {
        private readonly ILogger<SettingController> _logger;


        public SettingController(
            ILogger<SettingController> logger
        )
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexPaymantPlan()
        {
            
            List<PaymentPlan> listPayments = new List<PaymentPlan>(){
                new PaymentPlan("ddd", 20),
                new PaymentPlan("ddd", 40),
                new PaymentPlan("dd", 50),
            };
            return View(listPayments);
        }
        public IActionResult CreatePaymentPlan(PaymentPlan paymentPlan){
            //TODO:Implementar cadastro
            Console.WriteLine("Descrição:"+paymentPlan.Description);
            Console.WriteLine("Valor:"+paymentPlan.Value);
            return RedirectToAction("GetPaymentsPlans");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}