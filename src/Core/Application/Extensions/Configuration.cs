using Microsoft.Extensions.Configuration;

namespace Application.Extensions;

public static class Configuration
{
    public static IConfiguration? Manager { get; set; }

    public static string ConnectionString =>
        Manager.GetConnectionString("DefaultConnection");
    public static string RabbitMqConnectionString => Manager["RabbitMqConnectionString"];  
    public static string BaseApiLink => Manager["BaseApiLink"];  
}