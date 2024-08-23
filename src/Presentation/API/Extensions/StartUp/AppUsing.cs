using Application;
using DrMadWill.EventBus.Base.Abstractions;
using DrMadWill.Extensions.Cros;
using Persistence.Context;
using Serilog;
using Serilog.Events;
using AppContext = Persistence.Context.AppContext;

namespace API.Extensions.StartUp;

public static class AppUsing
{
    public static WebApplication UseAppUsing(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseSerilogRequestLogging(options =>
        {
            options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Verbose;
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                diagnosticContext.Set("RequestPath", httpContext.Request.Path);
            };
        });
        app.UseAppHealthChecks();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        CrosExtension.UsingCrosServices(app);
        app.UseAuthorization();
        var eventBus = app.Services.GetService<IEventBus>();
        eventBus!.UsingSubScribes();
        app.MigrateDbContext<AppContext>();
        return app;
    }
}