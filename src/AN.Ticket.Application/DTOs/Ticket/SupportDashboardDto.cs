using AN.Ticket.Application.DTOs.Activity;
using AN.Ticket.Application.DTOs.SatisfactionRating;
using AN.Ticket.Application.Helpers.Pagination;

namespace AN.Ticket.Application.DTOs.Ticket;
public class SupportDashboardDto
{
    public int TotalPendingTickets { get; set; }
    public int CurrentPendingTickets { get; set; }
    public int PreviousPendingTickets { get; set; }
    public int OpenActivities { get; set; }
    public int ClosedActivities { get; set; }
    public int TotalRecentRatings { get; set; }

    public PagedResult<TicketSummaryDto> RecentTickets { get; set; } = new PagedResult<TicketSummaryDto>();
    public List<MonthlyTicketDataDto> MonthlyTickets { get; set; } = new List<MonthlyTicketDataDto>();
    public PagedResult<ActivitySummaryDto> PagedActivities { get; set; } = new PagedResult<ActivitySummaryDto>();
    public List<SatisfactionRatingSummaryDto> RecentRatings { get; set; } = new List<SatisfactionRatingSummaryDto>();
}