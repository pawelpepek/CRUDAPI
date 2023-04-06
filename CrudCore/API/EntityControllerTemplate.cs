using AutoMapper;
using CrudCore.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudCore.API;

[ApiController]
public class EntityControllerTemplate<TEntity, TDto, TCreateDto> : EntityControllerStruct<TEntity>
    where TEntity : class, IIdentifiable, new()
    where TDto : class
    where TCreateDto : class
{
    public EntityControllerTemplate(DbContext context, IMapper mapper)
    : base(context, mapper) { }

    [HttpPost]
    public async Task<int> AddNew([FromBody] TCreateDto dto)
        => await Creator.AddEntity(dto);

    [HttpGet]
    public async Task<List<TDto>> GetAll()
        => await Reader.GetDtos<TDto>();

    [HttpGet("{id}")]
    public async Task<TDto> GetProduct(int id)
        => await Reader.GetDtoById<TDto>(id);

    [HttpPut("{id}")]
    public async Task Update(int id, [FromBody] TCreateDto dto)
        => await Updater.UpdateEntity(id, dto);

    [HttpDelete("{id}")]
    public async Task Remove(int id)
        => await Deleter.RemoveEntity(id);
}