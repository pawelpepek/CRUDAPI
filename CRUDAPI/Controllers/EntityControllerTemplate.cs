using CRUDAPI.Dtos;
using CRUDAPI.Entities;
using CRUDAPI.Services.CRUD;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPI.Controllers;

[ApiController]
public class EntityControllerTemplate<TEntity,TDto, TCreateDto>
    where TEntity : class,IIdentifiable, new()
    where TDto : class
    where TCreateDto : class
{
    protected readonly ICRUDService<TEntity> _crud;

    public EntityControllerTemplate(ICRUDService<TEntity> crud)
        => _crud = crud;

    [HttpPost]
    public async Task<int> AddNew([FromBody] TCreateDto dto)
        => await _crud.AddEntity(dto);

    [HttpGet]
    public async Task<List<TDto>> GetAll()
        => await _crud.GetDtos<TDto>();

    [HttpGet("{id}")]
    public async Task<TDto> GetProduct(int id)
        => await _crud.GetDtoById<TDto>(id);

    [HttpPut("{id}")]
    public async Task Update(int id, [FromBody] TCreateDto dto)
        => await _crud.UpdateEntity(id, dto);

    [HttpDelete("{id}")]
    public async Task Remove(int id)
        => await _crud.RemoveEntity(id);
}