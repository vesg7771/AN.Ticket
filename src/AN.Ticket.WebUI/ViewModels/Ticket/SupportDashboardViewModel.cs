using AN.Ticket.Application.DTOs.Ticket;

namespace AN.Ticket.WebUI.ViewModels.Ticket;

public class SupportDashboardViewModel
{
    public SupportDashboardDto DashboardData { get; set; }
    public TicketFilterDto Filters { get; set; }

    public int TicketPageNumber { get; set; }
    public int TicketPageSize { get; set; }

    public int ActivityPageNumber { get; set; }
    public int ActivityPageSize { get; set; }
}
