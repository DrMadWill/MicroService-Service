using Serilog;

namespace API.Extensions.StartUp;

public static class AppHosts
{
    private static IConfiguration GetSerilogConfig() =>
        new ConfigurationManager()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("Config/Serilog/serilog.json", optional: false)
            .AddJsonFile($"Config/Serilog/serilog.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
            .AddEnvironmentVariables()
            .Build(); 
    
    public static ConfigureHostBuilder UseAppHosts(this ConfigureHostBuilder host)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(GetSerilogConfig())
            .Filter.ByExcluding(c => c.Properties.ContainsKey("RequestPath") && c.Properties["RequestPath"].ToString().Contains("/health"))
            .CreateLogger();
        
        host.UseSerilog();
        
        return host;
    }
}