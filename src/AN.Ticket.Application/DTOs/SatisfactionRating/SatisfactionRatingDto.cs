using AN.Ticket.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AN.Ticket.Application.DTOs.SatisfactionRating;
public class SatisfactionRatingDto
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Por favor, selecione um nível de satisfação.")]
    public SatisfactionRatingValue Rating { get; set; }

    public string? Comment { get; set; }

    [Required]
    public Guid TicketId { get; set; }
}
