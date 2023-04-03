using CRUDAPI.Entities;
using CRUDAPI.Services;

namespace CRUDAPI.Configuration;

public static class ServiceCollectionExtensions
{
    public static void AddCRUDs(this IServiceCollection services)
    {
        services.AddCRUD<Client>()
                .AddCRUD<Product, ProductService>();

    }

    private static IServiceCollection AddCRUD<TEntity>(this IServiceCollection services)
        where TEntity : class, IIdentifiable, new()
        => services.AddCRUD<TEntity, CRUDService<TEntity>>();

    private static IServiceCollection AddCRUD<TEntity, TService>(this IServiceCollection services)
        where TEntity : class, IIdentifiable, new()
        where TService : class, ICRUDService<TEntity>
    {
        services.AddScoped<ICRUDService<TEntity>, TService>();
        return services;
    }
}
