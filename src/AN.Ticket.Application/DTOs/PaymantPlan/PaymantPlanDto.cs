using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AN.Ticket.Application.DTOs.PaymantPlan
{
    public class PaymantPlanDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Informe a descrição para o plano!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Informe o valor do plano!")]
        [Range(1, double.MaxValue, ErrorMessage = "O valor deve ser maior que 0!")]

        public double Value { get; set; }
    }
}