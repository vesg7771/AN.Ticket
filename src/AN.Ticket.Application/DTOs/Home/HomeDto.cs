using AN.Ticket.Application.DTOs.Ticket;

namespace AN.Ticket.Application.DTOs.Home;
public class HomeDto
{
    public int QtyOfTicketsOnhold { get; set; } = 0;
    public int QtyOfContactsAssociation { get; set; } = 0;
    public int QtyOfAvaliations { get; set; } = 0;
    public int QtyOfTicketsClosed { get; set; } = 0;
    public bool HasOverdueTickets { get; set; }
    public List<TicketDto> Tickets { get; set; } = new List<TicketDto>();
    public List<dynamic> TicketsByDay { get; set; } = new List<dynamic>();
}
