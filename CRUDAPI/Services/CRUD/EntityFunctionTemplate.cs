using AutoMapper;
using CRUDAPI.Entities;
using CRUDAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Services.CRUD;

public abstract class EntityFunctionTemplate<TEntity> where TEntity : class, IIdentifiable, new()
{
    protected readonly IMapper _mapper;
    protected readonly ApplicationDbContext _context;
    protected readonly CancellationToken _cancellationToken;
    protected readonly DbSet<TEntity> _set;

    protected Func<IQueryable<TEntity>, IQueryable<TEntity>> _includeFunc = (entity) => entity;
    protected Action<TEntity> _entityValidationAction = (entity) => { };

    public EntityFunctionTemplate(ApplicationDbContext context, IMapper mapper, CancellationToken cancellationToken)
        : this(context, mapper)
    {
        _cancellationToken = cancellationToken;
    }
    public EntityFunctionTemplate(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

        _set = context.Set<TEntity>();
    }

    public EntityFunctionTemplate<TEntity> SetEntityValidationAction(Action<TEntity> entityValidationAction)
    {
        _entityValidationAction = entityValidationAction;
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

        return await new EntityFinder<TEntity>(set, _cancellationToken)
                .ShowNotFoundError()
                .FindById(id);
    }

    protected async Task SaveChanges()
        => await _context.SaveChangesAsync(_cancellationToken);
}
