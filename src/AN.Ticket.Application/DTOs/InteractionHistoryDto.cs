namespace AN.Ticket.Application.DTOs;
public class InteractionHistoryDto
{
    public Guid Id { get; set; }
    public DateTime InteractionDate { get; set; }
    public string Description { get; set; }
    public Guid TicketId { get; set; }
    public UserDto User { get; set; }
}
