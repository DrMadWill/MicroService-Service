using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace API.Extensions.StartUp;

public static class AutoMigration
{

    public static IHost MigrateDbContext<TContext>(this IHost host,Func<TContext,IServiceProvider,Task>? seeder = null)
        where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<TContext>>();
        var context = services.GetRequiredService<TContext>();
        try
        {
            logger.LogInformation("Migrating database associated with context {DbContext}| Starting ... | Date : {date}",nameof(TContext),DateTime.Now);
            var retry = Policy.Handle<SqlException>()
                .WaitAndRetry(new TimeSpan[]
                {
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(8)
                });
            
            retry.Execute(() => InvokeSeeder( context, services,seeder));
            logger.LogInformation("Migrated database associated with context {DbContext} | Ending ... | Date : {date}",nameof(TContext),DateTime.Now);
        }
        catch (Exception e)
        {
            logger.LogInformation("Error {error} with context {DbContext} | Ending ... | Date : {date}",e,nameof(TContext),DateTime.Now);
        }
        return host;
    }


    private static void InvokeSeeder<TContext>( TContext context,
        IServiceProvider services,Func<TContext, IServiceProvider,Task>? seeder = null )
        where TContext : DbContext
    {
        context.Database.MigrateAsync().GetAwaiter().GetResult();
        seeder?.Invoke(context, services).GetAwaiter().GetResult();
    }


}