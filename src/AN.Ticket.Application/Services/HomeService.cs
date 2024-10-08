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

    public async Task<HomeDto> GetHomeData(Guid userId, DateTime startDate, DateTime endDate, bool showInProgress)
    {
        var tickets = await _ticketService.GetTicketWithDetailsByUserAsync(userId);

        if (tickets is not null)
        {
            var filteredTickets = tickets.Where(t => t.CreatedAt.Date >= startDate && t.CreatedAt.Date <= endDate);

            if (showInProgress)
            {
                filteredTickets = filteredTickets.Where(t => t.Status == TicketStatus.InProgress);
            }

            var daysRange = Enumerable.Range(0, (endDate - startDate).Days + 1)
                .Select(i => startDate.AddDays(i)).ToList();

            var ticketsByDay = daysRange
                .Select(date => new TicketsByDayDto
                {
                    Date = date,
                    OpenCount = filteredTickets.Count(t => t.Status == TicketStatus.Open && t.CreatedAt.Date == date.Date),
                    OnholdCount = filteredTickets.Count(t => t.Status == TicketStatus.Onhold && t.CreatedAt.Date == date.Date),
                    InProgressCount = filteredTickets.Count(t => t.Status == TicketStatus.InProgress && t.CreatedAt.Date == date.Date),
                    ClosedCount = filteredTickets.Count(t => t.Status == TicketStatus.Closed && t.CreatedAt.Date == date.Date)
                })
                .ToList();

            var homeData = new HomeDto
            {
                QtyOfTicketsOnhold = filteredTickets.Count(t => t.Status == TicketStatus.Onhold),
                QtyOfTicketsOpen = filteredTickets.Count(t => t.Status == TicketStatus.Open),
                QtyOfTicketsInProgress = filteredTickets.Count(t => t.Status == TicketStatus.InProgress),
                QtyOfTicketsClosed = filteredTickets.Count(t => t.Status == TicketStatus.Closed),
                QtyOfContactsAssociation = filteredTickets.Select(t => t.ContactName).Distinct().Count(),
                QtyOfAvaliations = filteredTickets.Count(t => t.SatisfactionRating != null),
                Tickets = filteredTickets.ToList(),
                TicketsByDay = ticketsByDay
            };

            return homeData;
        }

        return new HomeDto();
    }


    public class TicketsByDayDto
    {
        public DateTime Date { get; set; }
        public int OpenCount { get; set; }
        public int OnholdCount { get; set; }
        public int InProgressCount { get; set; }
        public int ClosedCount { get; set; }
    }
}
