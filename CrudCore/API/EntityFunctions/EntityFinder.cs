﻿using CrudCore.Objects;
using CrudCore.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CrudCore.API.EntityFunctions;

public class EntityFinder<TEntity>
    where TEntity : class, IIdentifiable, new()
{
    private readonly IQueryable<TEntity> _entitySet;

    private bool _showNotFoundError = false;

    public EntityFinder(IQueryable<TEntity> set)
    {
        _entitySet = set;
    }

    public EntityFinder<TEntity> ShowNotFoundError()
    {
        _showNotFoundError = true;
        return this;
    }

    public async Task<TEntity> FindById(int id)
    {
        var entity = await _entitySet.FirstOrDefaultAsync(e => e.Id == id);

        if (entity == null && _showNotFoundError)
        {
            throw NotFoundException<TEntity>.Generate(id);
        }

        return entity;
    }
}
