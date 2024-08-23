using Application.Abstractions.Services;
using Application.Extensions;
using DrMadWill.Layers.Repository;
using DrMadWill.Layers.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories.CQRS;
using Persistence.Services;
using Persistence.Services.Sys;
using AppContext = Persistence.Context.AppContext;

namespace Persistence;

public static class ServiceRegistration
{

    public static void AddPersistenceServices(this IServiceCollection services)
    {
        if (services == null) throw new AggregateException("service is null");
        services.AddDbContext<AppContext>(options => options.UseSqlServer(Configuration.ConnectionString));
        #region Repositories

        services.LayerRepositoriesRegister<AppUnitOfWork, AppQueryRepositories>();
        #endregion

        #region Services
        services.LayerServicesRegister<AppServiceManager>();
        #endregion
 
        
        // services.AddScoped<IOrderService, OrderService>();
    }

}