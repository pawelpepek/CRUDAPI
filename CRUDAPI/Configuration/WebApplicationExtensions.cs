using CRUDAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Configuration;

public static class WebApplicationExtensions
{
    public static WebApplication AddDbMigrations(this WebApplication app)
    {
        var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();

        return app;
    }
    public static WebApplication DbSeed(this WebApplication app)
    {
        var scope = app.Services.CreateScope();

        var seeder = scope.ServiceProvider.GetRequiredService<DbContextSeeder>();
        seeder.SeedData();

        return app;
    }
}