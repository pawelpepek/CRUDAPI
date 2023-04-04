using AutoMapper;
using CRUDAPI.Entities;
using CRUDAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Services.CRUD;

public abstract class CRUDStruct<TEntity> where TEntity : class, IIdentifiable, new()
{
    private readonly IMapper _mapper;
    protected readonly ApplicationDbContext _context;
    protected readonly CancellationToken _cancellationToken;
    protected readonly DbSet<TEntity> _set;

    protected EntityCreator<TEntity> Creator { get; }
    protected EntityReader<TEntity> Reader { get; }
    protected EntityUpdater<TEntity> Updater { get; }
    protected EntityDeleter<TEntity> Deleter { get; }

    public CRUDStruct(ApplicationDbContext context, IMapper mapper, CancellationToken cancellationToken)
        : this(context, mapper)
    {
        _cancellationToken = cancellationToken;

        Creator = new EntityCreator<TEntity>(context, _mapper, cancellationToken);
        Reader = new EntityReader<TEntity>(context, _mapper, cancellationToken);
        Updater = new EntityUpdater<TEntity>(context, _mapper, cancellationToken);
        Deleter = new EntityDeleter<TEntity>(context, _mapper, cancellationToken);
    }
    public CRUDStruct(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

        _set = _context.Set<TEntity>();

        Creator = new EntityCreator<TEntity>(context, _mapper);
        Reader = new EntityReader<TEntity>(context, _mapper);
        Updater = new EntityUpdater<TEntity>(context, _mapper);
        Deleter = new EntityDeleter<TEntity>(context, _mapper);
    }

}
