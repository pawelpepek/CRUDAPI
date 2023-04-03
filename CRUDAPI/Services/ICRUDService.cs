using CRUDAPI.Entities;
using CRUDAPI.Services.CRUD;

namespace CRUDAPI.Services;
public interface ICRUDService<TEntity> where TEntity : class, IIdentifiable, new()
{
    Task<int> AddEntity<TDto>(TDto dto) where TDto : class;
    Task<TDto> GetDtoById<TDto>(int id);
    Task<List<TDto>> GetDtos<TDto>();
    Task RemoveEntity(int id);
    CRUDService<TEntity> SetReaderNoTracking();
    Task UpdateEntity<TDto>(int id, TDto dto);
}