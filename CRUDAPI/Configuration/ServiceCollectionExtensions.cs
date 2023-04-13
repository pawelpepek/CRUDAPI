using CRUDAPI.Entities;
using CRUDAPI.Infrastructure;
using CRUDAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace CRUDAPI.Configuration;

public static class ServiceCollectionExtensions
{
    public static void AddAppAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        var authenticationSettings = new AuthenticationSettings();
        configuration.GetSection("Authentication").Bind(authenticationSettings);

        services.AddSingleton(authenticationSettings);

        services.AddAuthentication(AuthenticationOptionsSetup)
                .AddJwtBearer(options => JwtBearerOptionsSetup(options, authenticationSettings));

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    }

    public static void AddAppDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<ApplicationDbContext>
                (options => options.UseNpgsql(configuration.GetConnectionString("DbConnection")));

        services.AddScoped<DbContextSeeder>();
    }

    public static void AddSwaggerGenAuthorized(this IServiceCollection services)
        => services.AddSwaggerGen(SwaggerSetup);

    private static void AuthenticationOptionsSetup(AuthenticationOptions options)
    {
        options.DefaultAuthenticateScheme = "Bearer";
        options.DefaultScheme = "Bearer";
        options.DefaultChallengeScheme = "Bearer";
    }

    private static void JwtBearerOptionsSetup(JwtBearerOptions options, AuthenticationSettings authenticationSettings)
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = authenticationSettings.JwtIssuer,
            ValidAudience = authenticationSettings.JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
        };
    }

    private static void SwaggerSetup(SwaggerGenOptions options)
    {
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };

        options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

        options.AddSecurityRequirement
        (
            new OpenApiSecurityRequirement { { jwtSecurityScheme, Array.Empty<string>() } }
        );

        options.OrderActionsBy(e => $"{e.RelativePath}_{e.HttpMethod}");
    }
}
