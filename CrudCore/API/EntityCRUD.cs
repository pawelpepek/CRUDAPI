﻿using AutoMapper;
using CrudCore.API.EntityFunctions;
using CrudCore.Objects;
using Microsoft.EntityFrameworkCore;

namespace CrudCore.API;

public class EntityCRUD<TEntity> where TEntity : class, IIdentifiable, new()
{
    private readonly IMapper _mapper;
    protected readonly DbContext _context;
    protected readonly CancellationToken _cancellationToken;
    protected readonly DbSet<TEntity> _set;

    protected EntityCreator<TEntity> Creator { get; }
    protected EntityReader<TEntity> Reader { get; }
    protected EntityReader<TEntity> Reader2 { get; }
    protected EntityUpdater<TEntity> Updater { get; }
    protected EntityDeleter<TEntity> Deleter { get; }

    public EntityCRUD(DbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

        _set = _context.Set<TEntity>();

        Creator = new EntityCreator<TEntity>(context, _mapper);
        Reader = new EntityReader<TEntity>(context, _mapper);
        Reader2 = new EntityReader<TEntity>(context, _mapper);
        Updater = new EntityUpdater<TEntity>(context, _mapper);
        Deleter = new EntityDeleter<TEntity>(context, _mapper);
    }

}
