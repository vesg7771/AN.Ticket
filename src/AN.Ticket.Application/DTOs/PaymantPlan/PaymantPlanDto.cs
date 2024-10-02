using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AN.Ticket.Application.DTOs.PaymantPlan
{
    public class PaymantPlanDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
    }
}