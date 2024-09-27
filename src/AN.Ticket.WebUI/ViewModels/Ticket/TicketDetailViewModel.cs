using AN.Ticket.Application.DTOs.Ticket;
using AN.Ticket.Application.DTOs.User;
using AN.Ticket.Domain.Enums;

namespace AN.Ticket.WebUI.ViewModels.Ticket;

public class TicketDetailViewModel
{
    public Guid Id { get; set; }
    public int TicketCode { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Subject { get; set; }
    public string EmailMessageId { get; set; }
    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ClosedAt { get; set; }
    public DateTime? FirstResponseAt { get; set; }
    public Guid? UserId { get; set; }
    public UserDto? User { get; set; }
    public List<TicketMessageDto> Messages { get; set; }
}
