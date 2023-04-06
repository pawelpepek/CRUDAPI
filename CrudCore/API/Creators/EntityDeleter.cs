using AutoMapper;
using CrudCore.Objects;
using Microsoft.EntityFrameworkCore;

namespace CrudCore.API.Creators;

public class EntityDeleter<TEntity> : EntityFunctionTemplate<TEntity>
    where TEntity : class, IIdentifiable, new()
{
    public EntityDeleter(DbContext context, IMapper mapper, CancellationToken cancellationToken)
        : base(context, mapper, cancellationToken) { }
    public EntityDeleter(DbContext context, IMapper mapper) : base(context, mapper) { }

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
