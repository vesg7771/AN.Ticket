using AN.Ticket.Application.Helpers.EmailSender;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Application.Services;
using AN.Ticket.Domain.Accounts;
using AN.Ticket.Domain.Interfaces.Base;
using AN.Ticket.Infra.Data.Identity;
using AN.Ticket.Infra.Data.Repositories.Base;

namespace AN.Ticket.WebUI.Configuration;

public static class DependencyInjectionConfig
{
    public static void AddRegister(this IServiceCollection services, IConfiguration configuration)
    {
        #region Base
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        #region Identity
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        #endregion

        #region Services
        #endregion

        #region Repositories
        #endregion

        #region SMTP
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
        services.AddScoped<IEmailSenderService, EmailSenderService>();
        #endregion
    }
}