using AutoMapper;
using CrudCore.Objects;
using Microsoft.EntityFrameworkCore;

namespace CrudCore.API.EntityFunctions;

public class EntityUpdater<TEntity> : EntityFunctionTemplate<TEntity>
    where TEntity : class, IIdentifiable, new()
{
    public EntityUpdater(DbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task UpdateEntity<TDto>(int id, TDto dto)
    {
        var entity = await GetEntityById(id);

        var newEntity=_mapper.Map<TEntity>(dto);
        _entityAction(newEntity);

        _mapper.Map(newEntity, entity);

        await SaveChanges();
    }
}
