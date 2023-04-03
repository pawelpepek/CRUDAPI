using AutoMapper;
using CRUDAPI.Entities;
using CRUDAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using CRUDAPI.Services.CRUD;

namespace CRUDAPI.Services;

public class CRUDService<TEntity> : ICRUDService<TEntity> where TEntity : class, IIdentifiable, new()
{
    private readonly IMapper _mapper;
    protected readonly ApplicationDbContext _context;
    protected readonly CancellationToken _cancellationToken;
    protected readonly DbSet<TEntity> _set;

    protected readonly EntityCreator<TEntity> _creator;
    protected readonly EntityReader<TEntity> _reader;
    protected readonly EntityUpdater<TEntity> _updater;
    protected readonly EntityDeleter<TEntity> _deleter;

    public CRUDService(ApplicationDbContext context, IMapper mapper, CancellationToken cancellationToken)
        : this(context, mapper)
    {
        _cancellationToken = cancellationToken;

        _creator = new EntityCreator<TEntity>(context, _mapper, cancellationToken);
        _reader = new EntityReader<TEntity>(context, _mapper, cancellationToken);
        _updater = new EntityUpdater<TEntity>(context, _mapper, cancellationToken);
        _deleter = new EntityDeleter<TEntity>(context, _mapper, cancellationToken);
    }
    public CRUDService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

        _set = _context.Set<TEntity>();

        _creator = new EntityCreator<TEntity>(context, _mapper);
        _reader = new EntityReader<TEntity>(context, _mapper);
        _updater = new EntityUpdater<TEntity>(context, _mapper);
        _deleter = new EntityDeleter<TEntity>(context, _mapper);
    }
    
    public CRUDService<TEntity> SetReaderNoTracking()
    {
        _reader.SetAsNoTracking();
        return this;
    }

    public async Task<int> AddEntity<TDto>(TDto dto) where TDto : class
        => await _creator.AddEntity<TDto>(dto);

    public async Task<List<TDto>> GetDtos<TDto>() => await _reader.GetDtos<TDto>();

    public async Task<TDto> GetDtoById<TDto>(int id) => await _reader.GetDtoById<TDto>(id);

    public async Task UpdateEntity<TDto>(int id, TDto dto) => await _updater.UpdateEntity<TDto>(id, dto);

    public async Task RemoveEntity(int id) => await _deleter.RemoveEntity(id);
}
