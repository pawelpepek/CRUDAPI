using AutoMapper;
using CRUDAPI.Entities;
using CRUDAPI.Common.Helpers;
using CRUDAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using CRUDAPI.Common.Exceptions;

namespace CRUDAPI.Services;

public class CRUDService<TEntity> : ICRUDService<TEntity> where TEntity : class, IIdentifiable, new()
{
    private readonly IMapper _mapper;
    protected readonly ApplicationDbContext _context;
    protected readonly CancellationToken _cancellationToken;
    protected readonly DbSet<TEntity> _set;

    public CRUDService(ApplicationDbContext context, IMapper mapper, CancellationToken cancellationToken)
        : this(context, mapper)
    {
        _cancellationToken = cancellationToken;
    }
    public CRUDService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

        _set = _context.Set<TEntity>();
    }

    public async Task<List<TDto>> GetDtos<TDto>()
    {
        var entities = await GetAll();

        return entities.Any()
            ? entities.Select(_mapper.Map<TDto>).ToList()
            : new List<TDto>();
    }

    public async Task<List<TEntity>> GetAll()
            => await _set.ToListAsync(_cancellationToken);

    public async Task<int> AddEntity<TDto>(TDto dto) where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);

        if (IsNewEntityOk(entity))
        {
            _set.Add(entity);
            await SaveChanges();

            return entity.Id;
        }
        else
        {
            return 0;
        }

    }

    protected virtual bool IsNewEntityOk(TEntity entity) => true;

    public async Task<TDto> GetDtoById<TDto>(int id)
    {
        var entity = await GetEntityById(id);
        return _mapper.Map<TDto>(entity);
    }

    public async Task UpdateEntity<TDto>( int id, TDto dto)
    {
        var entity = await GetEntityById(id);

        //TODO: Deep Clone
    }

    public async Task RemoveEntity(int id)
    {
        var entity = await GetEntityById(id);

        if (entity != null)
        {
            _set.Remove(entity);
            await SaveChanges();
        }
    }

    public async Task<TEntity> GetEntityById(int id)
        => await new EntityFinder<TEntity>(_context, _cancellationToken)
                .ShowNotFoundError()
                .FindById(id);

    private async Task SaveChanges()
        => await _context.SaveChangesAsync(_cancellationToken);
}
