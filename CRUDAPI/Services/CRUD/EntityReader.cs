using AutoMapper;
using CRUDAPI.Entities;
using CRUDAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Services.CRUD;

public class EntityReader<TEntity> : EntityFunctionTemplate<TEntity> 
    where TEntity : class, IIdentifiable, new()
{
    protected bool _asNoTracking = true;

    public EntityReader(ApplicationDbContext context, IMapper mapper, CancellationToken cancellationToken)
        : base(context, mapper, cancellationToken) { }
    public EntityReader(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

    public EntityReader<TEntity> SetAsNoTracking(bool tracking = true)
    {
        _asNoTracking = tracking;
        return this;
    }

    public async Task<List<TDto>> GetDtos<TDto>()
    {
        var entities = await GetAll();
        return entities.Select(_mapper.Map<TDto>).ToList();
    }

    public async Task<TDto> GetDtoById<TDto>(int id)
    {
        var entity = await GetEntityById(id);
        return _mapper.Map<TDto>(entity);
    }

    private async Task<List<TEntity>> GetAll()
    {
        var set = _asNoTracking ? _set.AsNoTracking() : _set;
        return await _includeFunc(set).ToListAsync(_cancellationToken);
    }
}
