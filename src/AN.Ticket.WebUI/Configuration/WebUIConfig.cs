using AN.Ticket.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AN.Ticket.WebUI.Configuration;

public static class WebUIConfig
{
    public static IServiceCollection AddWebUI(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AtlasBd");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            )
        );

        services.AddControllersWithViews();
        services.AddCustomAuthentication();
        services.AddRegister(configuration);

        return services;
    }

    public static IApplicationBuilder UseWebUI(
        this IApplicationBuilder app, IWebHostEnvironment env
    )
    {
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}