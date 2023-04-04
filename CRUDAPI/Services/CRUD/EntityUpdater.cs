using AutoMapper;
using CRUDAPI.Entities;
using CRUDAPI.Infrastructure;

namespace CRUDAPI.Services.CRUD;

public class EntityUpdater<TEntity> : EntityFunctionTemplate<TEntity> 
    where TEntity : class, IIdentifiable, new()
{
    public EntityUpdater(ApplicationDbContext context, IMapper mapper, CancellationToken cancellationToken)
        : base(context, mapper, cancellationToken) { }
    public EntityUpdater(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task UpdateEntity<TDto>(int id, TDto dto)
    {
        var entity = await GetEntityById(id);

        //TODO: Deep Clone
    }
}
