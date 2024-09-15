using AN.Ticket.Application.DTOs.User;

namespace AN.Ticket.Application.DTOs.Ticket;
public class TicketMessageDto
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public Guid? UserId { get; set; }
    public UserDto? User { get; set; }
    public DateTime? SentAt { get; set; }
    public bool IsFromSupport { get; set; }
}
