﻿using CRUDAPI.Controllers;
using CRUDAPI.Entities;
using CRUDAPI.Infrastructure;
using CRUDAPI.Services;
using CrudCore.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace CRUDAPI.Configuration;

public static class ServiceCollectionExtensions
{
    public static void AddAppAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        var authenticationSettings = new AuthenticationSettings();
        configuration.GetSection("Authentication").Bind(authenticationSettings);

        services.AddSingleton(authenticationSettings);
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = "Bearer";
            option.DefaultScheme = "Bearer";
            option.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = authenticationSettings.JwtIssuer,
                ValidAudience = authenticationSettings.JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
            };
        }
        );

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    }

    public static void AddAppDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<ApplicationDbContext>
                (options => options.UseNpgsql(configuration.GetConnectionString("DbConnection")));
        services.AddScoped<DbContextSeeder>();

        services.AddScoped<DbContextSeeder>();
    }
    public static void AddCRUDs(this IServiceCollection services)
    {
        services.AddCRUD<Client, ClientService>();
        services.AddCRUD<Product, ProductService>();
    }
}
