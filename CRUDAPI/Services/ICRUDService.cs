using CRUDAPI.Entities;

namespace CRUDAPI.Services;
public interface ICRUDService<TEntity> where TEntity : class, IIdentifiable, new()
{
    Task<int> AddEntity<TDto>(TDto dto) where TDto : class;
    Task<List<TEntity>> GetAll();
    Task<TDto> GetDtoById<TDto>(int id);
    Task<List<TDto>> GetDtos<TDto>();
    Task<TEntity> GetEntityById(int id);
    Task RemoveEntity(int id);
    CRUDService<TEntity> SetDuplicates(bool duplicates);
    Task UpdateEntity<TDto>(int id, TDto dto);
}