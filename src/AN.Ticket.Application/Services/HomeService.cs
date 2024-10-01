using AN.Ticket.Application.DTOs.Home;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Domain.Enums;

namespace AN.Ticket.Application.Services;
public class HomeService : IHomeService
{
    private readonly ITicketService _ticketService;

    public HomeService(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    public async Task<HomeDto> GetHomeData(Guid userId, DateTime startOfWeek, DateTime endOfWeek, bool showInProgress)
    {
        var tickets = await _ticketService.GetTicketWithDetailsByUserAsync(userId);

        if (tickets is not null)
        {
            var filteredTickets = tickets.Where(t => t.CreatedAt >= startOfWeek && t.CreatedAt <= endOfWeek);

            if (showInProgress)
            {
                filteredTickets = filteredTickets.Where(t => t.Status == TicketStatus.InProgress);
            }

            var ticketsByDay = Enumerable.Range(0, 7)
                .Select(i => new TicketsByDayDto
                {
                    Date = startOfWeek.AddDays(i),
                    Count = filteredTickets.Count(t => t.CreatedAt.Date == startOfWeek.AddDays(i).Date)
                })
                .ToList();

            var homeData = new HomeDto
            {
                QtyOfTicketsOnhold = filteredTickets.Count(t => t.Status == TicketStatus.Onhold),
                QtyOfContactsAssociation = filteredTickets.Select(t => t.ContactName).Distinct().Count(),
                QtyOfAvaliations = filteredTickets.Count(t => t.SatisfactionRating != null),
                QtyOfTicketsClosed = filteredTickets.Count(t => t.Status == TicketStatus.Closed),
                Tickets = filteredTickets.ToList(),
                TicketsByDay = ticketsByDay.Cast<dynamic>().ToList()
            };

            return homeData;
        }

        return new HomeDto();
    }

    public class TicketsByDayDto
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}
