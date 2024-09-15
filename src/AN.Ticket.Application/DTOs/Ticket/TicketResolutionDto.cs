using System.ComponentModel.DataAnnotations;

namespace AN.Ticket.Application.DTOs.Ticket;
public class TicketResolutionDto
{
    public Guid? Id { get; set; }

    public Guid TicketId { get; set; }

    [Required(ErrorMessage = "O campo de detalhes da resolução é obrigatório.")]
    [StringLength(1000, ErrorMessage = "O campo detalhes da resolução deve ter no máximo 1000 caracteres.")]
    public string ResolutionDetails { get; set; }

    public bool NotifyContact { get; set; }
}
