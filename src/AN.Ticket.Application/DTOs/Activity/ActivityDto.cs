using AN.Ticket.Application.DTOs.Contact;
using AN.Ticket.Application.DTOs.Ticket;
using AN.Ticket.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AN.Ticket.Application.DTOs.Activity;
public class ActivityDto
{
    [Required(ErrorMessage = "O campo Id é obrigatório.")]
    public Guid Id { get; set; }

    [StringLength(100, ErrorMessage = "O campo Assunto pode ter no máximo 100 caracteres.")]
    public string? Subject { get; set; }

    [Required(ErrorMessage = "O campo Tipo é obrigatório.")]
    public ActivityType Type { get; set; }

    [StringLength(500, ErrorMessage = "O campo Descrição pode ter no máximo 500 caracteres.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "O campo Data Agendada é obrigatório.")]
    public DateTime ScheduledDate { get; set; }

    public TimeSpan? Duration { get; set; }

    [Required(ErrorMessage = "O campo Prioridade é obrigatório.")]
    public ActivityPriority Priority { get; set; }

    public Guid? ContactId { get; set; }

    public ContactDto? Contact { get; set; }

    public Guid TicketId { get; set; }

    public TicketDto? Ticket { get; set; }
}
