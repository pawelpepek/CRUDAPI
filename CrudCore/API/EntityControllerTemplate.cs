using AutoMapper;
using CrudCore.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudCore.API;

public class EntityControllerTemplate<TEntity, TDto, TCreateDto>
    : EntityControllerTemplate<TEntity, TDto, TDto, TCreateDto>
    where TEntity : class, IIdentifiable, new()
    where TDto : class
    where TCreateDto : class
{
    public EntityControllerTemplate(DbContext context, IMapper mapper)
    : base(context, mapper) {}
}

[ApiController]
public class EntityControllerTemplate<TEntity, TDto, TOneDto, TCreateDto>
    : EntityCRUD<TEntity>
    where TEntity : class, IIdentifiable, new()
    where TDto : class
    where TOneDto : class
    where TCreateDto : class
{
    public EntityControllerTemplate(DbContext context, IMapper mapper)
      : base(context, mapper) { }

    [HttpPost]
    public virtual async Task<int> AddNew([FromBody] TCreateDto dto)
       => await Creator.AddEntity(dto);

    [HttpGet]
    public virtual async Task<List<TDto>> GetAll()
        => await Reader.GetDtos<TDto>();

    [HttpGet("{id}")]
    public virtual async Task<TOneDto> GetDtoById(int id)
        => await Reader2.GetDtoById<TOneDto>(id);

    [HttpPut("{id}")]
    public virtual async Task Update(int id, [FromBody] TCreateDto dto)
        => await Updater.UpdateEntity(id, dto);

    [HttpDelete("{id}")]
    public virtual async Task Remove(int id)
        => await Deleter.RemoveEntity(id);
}
