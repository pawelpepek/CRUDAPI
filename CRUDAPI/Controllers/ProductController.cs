using CRUDAPI.Dtos;
using CRUDAPI.Entities;
using CRUDAPI.Services.CRUD;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPI.Controllers;

[Route("api/product")]
public class ProductController : EntityControllerTemplate<Product, ProductDto, CreateProductDto>
{
    public ProductController(ICRUDService<Product> crud) : base(crud) { }
}