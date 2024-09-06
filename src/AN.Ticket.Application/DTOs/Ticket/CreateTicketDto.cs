using AN.Ticket.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AN.Ticket.Application.DTOs.Ticket;
public class CreateTicketDto
{
    [Required(ErrorMessage = "O nome do contato é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do contato deve ter no máximo 100 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O nome da conta é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome da conta deve ter no máximo 100 caracteres.")]
    public string AccountName { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Por favor, insira um e-mail válido.")]
    [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [Phone(ErrorMessage = "Por favor, insira um número de telefone válido.")]
    [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "O assunto é obrigatório.")]
    [StringLength(200, ErrorMessage = "O assunto deve ter no máximo 200 caracteres.")]
    public string Subject { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "O status é obrigatório.")]
    public TicketStatus Status { get; set; }

    [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
    [DataType(DataType.Date, ErrorMessage = "Por favor, insira uma data de vencimento válida.")]
    [NotDefaultDate]
    public DateTime DueDate { get; set; }

    [Required(ErrorMessage = "A prioridade é obrigatória.")]
    public TicketPriority Priority { get; set; }

    public Guid UserId { get; set; }

    public string? AttachmentFile { get; set; }
}

public class NotDefaultDateAttribute : ValidationAttribute
{
    public NotDefaultDateAttribute()
    {
        ErrorMessage = "A data de vencimento não pode ser a data padrão.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("A data de vencimento é obrigatória.");
        }

        if (value is DateTime date && date == DateTime.MinValue)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
