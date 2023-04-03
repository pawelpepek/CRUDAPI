using AutoMapper;
using CRUDAPI.Common.Exceptions;
using CRUDAPI.Entities;
using CRUDAPI.Infrastructure;

namespace CRUDAPI.Services.CRUD;

public class EntityDeleter<TEntity> : EntityFunctionTemplate<TEntity> where TEntity : class, IIdentifiable, new()
{
    public EntityDeleter(ApplicationDbContext context, IMapper mapper, CancellationToken cancellationToken)
        : base(context, mapper, cancellationToken) { }
    public EntityDeleter(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task RemoveEntity(int id)
    {
        var entity = await GetEntityById(id);

        if (entity != null)
        {
            _entityValidationAction(entity);
            _set.Remove(entity);
            await SaveChanges();
        }
    }
}
