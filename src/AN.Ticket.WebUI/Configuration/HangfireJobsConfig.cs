using AN.Ticket.Application.Interfaces;
using Hangfire;

namespace AN.Ticket.WebUI.Configuration;

public static class HangfireJobsConfig
{
    public static void ConfigureRecurringJobs(IConfiguration configuration)
    {
        BackgroundJob.Enqueue<IEmailMonitoringService>(
            service => service.StartMonitoringAsync(CancellationToken.None)
        );
    }
}
