using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace API.Extensions.StartUp;

public static class HealthCheckExtension
{
    public static WebApplication UseAppHealthChecks(this WebApplication? app)
    {
        if (app is null) throw new Exception("app is not null here");
        app.UseHealthChecks("/api/health",new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                await context.Response.WriteAsync("Ok");
            }
        });


        return app;

    }


}