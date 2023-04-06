using AutoMapper;
using CrudCore.Objects;
using Microsoft.EntityFrameworkCore;

namespace CrudCore.API.Creators;

public class EntityUpdater<TEntity> : EntityFunctionTemplate<TEntity>
    where TEntity : class, IIdentifiable, new()
{
    public EntityUpdater(DbContext context, IMapper mapper, CancellationToken cancellationToken)
        : base(context, mapper, cancellationToken) { }
    public EntityUpdater(DbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task UpdateEntity<TDto>(int id, TDto dto)
    {
        var entity = await GetEntityById(id);

        //TODO: Deep Clone
    }
}
