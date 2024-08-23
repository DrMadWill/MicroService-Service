using System.Reflection;
using System.Text.Json.Serialization;
using API.Extensions.StartUp;
using Application.Extensions;
using DrMadWill.Consul;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration(i => i.AddConfiguration(GetAppConfig()));
// Add services to the container.
Configuration.Manager = builder.Configuration;
builder.Services.AddControllers()
    .AddFluentValidation
        (c =>  c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()))
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });



builder.Host.UseAppHosts();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAppServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAppUsing();

app.MapControllers();
#region Consul
app.RegisterWithConsul(app.Lifetime);
#endregion
app.Run();

IConfiguration GetAppConfig() =>
    new ConfigurationManager()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("Config/Project/appsettings.json", optional: false)
        .AddJsonFile($"Config/Project/appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
        .AddEnvironmentVariables()
        .Build();