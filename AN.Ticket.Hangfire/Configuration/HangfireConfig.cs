using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AN.Ticket.Hangfire.Configuration;
public static class HangfireConfig
{
    public static void AddHangfireConfiguration(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddHangfire(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
            config.UseSimpleAssemblyNameTypeSerializer();
            config.UseRecommendedSerializerSettings();
            config.UseStorage(new MySqlStorage(configuration.GetConnectionString("AtlasHanfireBd"),
                new MySqlStorageOptions()));
        });

        var workerCount = configuration.GetValue<int>("Hangfire:WorkerCount");
        if (workerCount <= 0)
            return;

        services.AddHangfireServer(options =>
        {
            options.WorkerCount = workerCount;
            options.Queues = new[]
            {
                "default",
                Enums.TypeQueue.synchronization.ToString(),
                Enums.TypeQueue.support_ticket.ToString(),
                Enums.TypeQueue.ticket_response.ToString(),
                Enums.TypeQueue.general.ToString()
            };
        });
    }

    public static void UseHangfireConfiguration(
        this IApplicationBuilder app,
        IConfiguration configuration
    )
    {
        if (!configuration.GetValue<bool>("Hangfire:EnableDashboard"))
            return;

        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = new[] { new HangfireAuthorizationFilter() },
            DisplayStorageConnectionString = false
        });
    }
}
