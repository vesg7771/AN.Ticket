using System.ComponentModel.DataAnnotations;

namespace AN.Ticket.Application.DTOs.PaymantPlan;

public class PaymantPlanDto
{
    public Guid Id { get; set; }

    [Display(Name = "Descrição")]
    [Required(ErrorMessage = "Informe a descrição para o plano!")]
    public string? Description { get; set; }

    [Display(Name = "Valor")]
    [Required(ErrorMessage = "Informe o valor do plano!")]
    [Range(1, double.MaxValue, ErrorMessage = "O valor deve ser maior que 0!")]
    public double Value { get; set; }
}