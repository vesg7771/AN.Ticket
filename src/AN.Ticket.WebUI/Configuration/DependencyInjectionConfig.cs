using AN.Ticket.Domain.Accounts;
using AN.Ticket.Domain.Interfaces.Base;
using AN.Ticket.Infra.Data.Identity;
using AN.Ticket.Infra.Data.Identity.Services;
using AN.Ticket.Infra.Data.Repositories.Base;

namespace AN.Ticket.WebUI.Configuration;

public static class DependencyInjectionConfig
{
    public static void AddRegister(this IServiceCollection services)
    {
        #region Base
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        #region Identity
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        services.AddScoped<IAuthenticate, AuthenticateService>();
        #endregion

        #region Services
        #endregion

        #region Repositories
        #endregion
    }
}