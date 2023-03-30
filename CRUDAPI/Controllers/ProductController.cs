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

    [HttpGet]
    public async Task<List<ProductDto>> GetAll()
    {
        return await _crud.GetDtos<ProductDto>();
    }
}