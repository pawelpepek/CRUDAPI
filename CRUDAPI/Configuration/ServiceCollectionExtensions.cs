using CRUDAPI.Entities;
using CRUDAPI.Services;

namespace CRUDAPI.Configuration;

public static class ServiceCollectionExtensions
{
    public static void AddCRUDs(this IServiceCollection services)
    {
        services.AddScoped<ICRUDService<Product>, ProductService>()
                .AddCRUD<Client>();
    }

    private static IServiceCollection AddCRUD<TEntity>(this IServiceCollection services)
        where TEntity : class, IIdentifiable, new()
    {
        services.AddScoped<ICRUDService<TEntity>, CRUDService<TEntity>>();
        return services;
    }
}
