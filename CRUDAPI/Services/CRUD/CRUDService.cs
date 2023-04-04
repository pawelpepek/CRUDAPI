using AutoMapper;
using CRUDAPI.Entities;
using CRUDAPI.Infrastructure;

namespace CRUDAPI.Services.CRUD;

public class CRUDService<TEntity>
    : CRUDStruct<TEntity>, ICRUDService<TEntity>
    where TEntity : class, IIdentifiable, new()
{

    public CRUDService(ApplicationDbContext context, IMapper mapper, CancellationToken cancellationToken)
        : base(context, mapper, cancellationToken) { }
    public CRUDService(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<int> AddEntity<TDto>(TDto dto) where TDto : class
        => await Creator.AddEntity(dto);

    public async Task<List<TDto>> GetDtos<TDto>() => await Reader.GetDtos<TDto>();

    public async Task<TDto> GetDtoById<TDto>(int id) => await Reader.GetDtoById<TDto>(id);

    public async Task UpdateEntity<TDto>(int id, TDto dto) => await Updater.UpdateEntity(id, dto);

    public async Task RemoveEntity(int id) => await Deleter.RemoveEntity(id);
}
