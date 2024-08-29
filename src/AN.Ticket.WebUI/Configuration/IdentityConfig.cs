using AN.Ticket.Infra.Data.Context;
using AN.Ticket.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace AN.Ticket.WebUI.Configuration;

public static class IdentityConfig
{
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromHours(5);
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.SlidingExpiration = true;
        });

        return services;
    }
}
