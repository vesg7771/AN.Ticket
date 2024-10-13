namespace AN.Ticket.Application.DTOs.Ticket;
public class MonthlyTicketDataDto
{
    public string Month { get; set; }
    public double AverageResponseTimeHours { get; set; }
    public double? PercentageChange { get; set; }
}