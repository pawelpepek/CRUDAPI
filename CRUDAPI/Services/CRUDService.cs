using AutoMapper;
using CRUDAPI.Entities;
using CRUDAPI.Helpers;
using CRUDAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRUDAPI.Services;

public class CRUDService<TEntity> : ICRUDService<TEntity> where TEntity : class, IIdentifiable, new()
{
    private bool _duplicate = false;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;
    private readonly CancellationToken _cancellationToken;
    private readonly DbSet<TEntity> _set;

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

    public CRUDService<TEntity> SetDuplicates(bool duplicates)
    {
        _duplicate = duplicates;
        return this;
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


        if (!_duplicate)
        {
            //TODO
        }

        _set.Add(entity);
        await SaveChanges();

        return entity.Id;
    }

    public async Task<TDto> GetDtoById<TDto>(int id)
    {
        var entity = await GetEntityById(id);

        if(entity == null)
        {
            throw new Exception("Not Found");
        }

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
        => await new EntityFinder<TEntity>(_context, _cancellationToken).FindById(id);

    private async Task SaveChanges()
        => await _context.SaveChangesAsync(_cancellationToken);
}
