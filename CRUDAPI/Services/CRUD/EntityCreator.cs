using AutoMapper;
using CRUDAPI.Entities;
using CRUDAPI.Infrastructure;

namespace CRUDAPI.Services.CRUD;

public class EntityCreator<TEntity> : EntityFunctionTemplate<TEntity> where TEntity : class, IIdentifiable, new()
{
    public EntityCreator(ApplicationDbContext context, IMapper mapper, CancellationToken cancellationToken)
        : base(context, mapper, cancellationToken) { }
    public EntityCreator(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<int> AddEntity<TDto>(TDto dto) where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);

        _entityValidationAction(entity);

        _set.Add(entity);
        await SaveChanges();

        return entity.Id;
    }
}
