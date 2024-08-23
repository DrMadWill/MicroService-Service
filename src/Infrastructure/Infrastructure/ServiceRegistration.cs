
using Application.Abstractions.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        if (services == null) throw new AggregateException("service is null");
        services.AddScoped<IApiRequestService, ApiRequestService>();
    }
}