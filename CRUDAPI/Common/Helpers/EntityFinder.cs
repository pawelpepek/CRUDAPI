using CRUDAPI.Common.Exceptions;
using CRUDAPI.Entities;
using CRUDAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Common.Helpers;

public class EntityFinder<TEntity> where TEntity : class, IIdentifiable, new()
{
    private readonly DbSet<TEntity> _entitySet;
    private readonly ApplicationDbContext _context;
    private readonly CancellationToken _cancellationToken;

    private bool _showNotFoundError = false;

    public EntityFinder(ApplicationDbContext context, CancellationToken cancellationToken)
    {
        _context = context;
        _cancellationToken = cancellationToken;

        _entitySet = _context.Set<TEntity>();
    }

    public EntityFinder<TEntity> ShowNotFoundError()
    {
        _showNotFoundError = true;
        return this;
    }

    public async Task<TEntity> FindById(int id)
    {
        var entity = await _entitySet.FirstOrDefaultAsync(e => e.Id == id, _cancellationToken);

        if(entity == null && _showNotFoundError)
        {
            var entityName = ObjectAttributeGetter.GetObjectName<TEntity>();
            throw new NotFoundException<int>(entityName, id);
        }

        return entity;
    }
}
