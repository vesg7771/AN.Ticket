namespace AN.Ticket.Application.Interfaces;
public interface IEmailMonitoringService
{
    Task StartMonitoringAsync(CancellationToken cancellationToken);
}
