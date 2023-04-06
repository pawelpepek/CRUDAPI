using CrudCore.Objects;
using Microsoft.Extensions.DependencyInjection;

namespace CrudCore.API
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCRUD<TEntity>(this IServiceCollection services)
            where TEntity : class, IIdentifiable, new()
            => services.AddCRUD<TEntity, CRUDService<TEntity>>();

        public static IServiceCollection AddCRUD<TEntity, TService>(this IServiceCollection services)
            where TEntity : class, IIdentifiable, new()
            where TService : class, ICRUDService<TEntity>
        {
            services.AddScoped<ICRUDService<TEntity>, TService>();
            return services;
        }
    }
}
