using System.Security.Claims;
using System.Text;
using Application;
using Application.Extensions;
using DrMadWill.Consul;
using DrMadWill.Extensions.Cros;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;

namespace API.Extensions.StartUp;

public static class AppServices
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        bool isTestServer = configuration.GetSection("TestServer").Get<bool>();
        services.AddEndpointsApiExplorer();
        services.AddHttpClient();
        services.AddHttpContextAccessor();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo {
                Title = "App Api", 
                Version = "1"
            });

            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            c.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement();
            securityRequirement.Add(securitySchema, new[] { "Bearer" });
            c.AddSecurityRequirement(securityRequirement);

        });
        services.AddApplicationServices();
        services.AddInfrastructureServices();
        services.AddPersistenceServices();
        
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0); // Default version
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
           
        });
        
        services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;
            logging.RequestHeaders.Add("sec-ch-ua");
            logging.MediaTypeOptions.AddText("application/javascript");
            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;
        });
        

        services.AddAuthentication(config =>
            {
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true, 
                    ValidateIssuer = true, 
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true, 
                    ClockSkew = TimeSpan.FromMinutes(5),
                    // security switches
                    RequireExpirationTime = true,
                    ValidAudience = configuration["Token:Audience"],
                    ValidIssuer = configuration["Token:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                    NameClaimType = ClaimTypes.NameIdentifier,
                    RoleClaimType = ClaimTypes.Role
                };
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
            }).Services.ConfigureApplicationCookie(
                options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(3600);
                });
    
        services.AddHealthChecks();
       
        services.ConfigureConsul(configuration); 
        services.AddCrosServices(configuration, "AllowSpecificOrigin", isTestServer ? "TestServerUrls" : "ServiceUrls");  
        return services;
    }
}