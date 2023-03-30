using CRUDAPI.Dtos;
using CRUDAPI.Entities;
using CRUDAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPI.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    private readonly ICRUDService<Product> _crud;

    public ProductController(ICRUDService<Product> crud)
    {
        _crud = crud;
    }

    [HttpPost]
    public async Task<int> AddNew([FromBody] CreateProductDto dto)
    {
        return await _crud.AddEntity(dto);
    }

    [HttpGet]
    public async Task<List<ProductDto>> GetAll()
    {
        return await _crud.GetDtos<ProductDto>();
    }

    [HttpGet("{id}")]
    public async Task<ProductDto> GetProduct(int id)
    {
        return await _crud.GetDtoById<ProductDto>(id);
    }

    [HttpPut("{id}")]
    public async Task Update(int id, [FromBody] CreateProductDto dto)
    {
        await _crud.UpdateEntity(id, dto);
    }

    [HttpDelete("{id}")]
    public async Task Remove(int id)
    {
        await _crud.RemoveEntity(id);
    }
}