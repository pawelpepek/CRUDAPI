using AutoMapper;
using CrudCore.Objects;
using Microsoft.EntityFrameworkCore;

namespace CrudCore.API.Creators;

public class EntityCreator<TEntity> : EntityFunctionTemplate<TEntity>
    where TEntity : class, IIdentifiable, new()
{
    public EntityCreator(DbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<int> AddEntity<TDto>(TDto dto) where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);

        _entityValidationAction(entity);

        _set.Add(entity);
        await SaveChanges();

        return entity.Id;
    }
}
