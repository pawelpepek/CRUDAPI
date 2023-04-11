using AutoMapper;
using CrudCore.Objects;
using Microsoft.EntityFrameworkCore;

namespace CrudCore.API.Creators;

public abstract class EntityFunctionTemplate<TEntity> 
    where TEntity : class, IIdentifiable, new()
{
    protected readonly IMapper _mapper;
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _set;

    protected Func<IQueryable<TEntity>, IQueryable<TEntity>> _includeFunc = (entity) => entity;
    protected Action<TEntity> _entityAction = (entity) => { };

    public EntityFunctionTemplate(DbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

        _set = context.Set<TEntity>();
    }

    public EntityFunctionTemplate<TEntity> SetEntityAction(Action<TEntity> entityAction)
    {
        _entityAction = entityAction;
        return this;
    }

    public EntityFunctionTemplate<TEntity> SetIncludeFunction(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc)
    {
        _includeFunc = includeFunc;
        return this;
    }

    protected async Task<TEntity> GetEntityById(int id)
    {
        var set = _includeFunc(_set);

        return await new EntityFinder<TEntity>(set)
                .ShowNotFoundError()
                .FindById(id);
    }

    protected async Task SaveChanges()
        => await _context.SaveChangesAsync();
}
