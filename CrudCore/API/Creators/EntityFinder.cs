using CrudCore.Objects;
using CrudCore.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CrudCore.API.Creators;

public class EntityFinder<TEntity> where TEntity : class, IIdentifiable, new()
{
    private readonly IQueryable<TEntity> _entitySet;
    private readonly CancellationToken _cancellationToken;

    private bool _showNotFoundError = false;

    public EntityFinder(IQueryable<TEntity> set, CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        _entitySet = set;
    }

    public EntityFinder<TEntity> ShowNotFoundError()
    {
        _showNotFoundError = true;
        return this;
    }

    public async Task<TEntity> FindById(int id)
    {
        var entity = await _entitySet.FirstOrDefaultAsync(e => e.Id == id, _cancellationToken);

        if (entity == null && _showNotFoundError)
        {
            var entityName = ObjectAttributeGetter.GetObjectName<TEntity>();
            throw new NotFoundException<int>(entityName, id);
        }

        return entity;
    }
}
