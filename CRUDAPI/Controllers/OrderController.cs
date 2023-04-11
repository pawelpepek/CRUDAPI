using AutoMapper;
using CRUDAPI.Dtos;
using CRUDAPI.Entities;
using CRUDAPI.Entities.Helpers;
using CRUDAPI.Infrastructure;
using CrudCore.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CRUDAPI.Controllers;

[Controller]
[Route("api/order")]
public class OrderController : EntityCRUD<Order>
{
    public OrderController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        Reader2.SetIncludeFunction(entities => entities.Include(p => p.Products));
    }

    [HttpGet]
    public async Task GetAll()
        => await Reader.GetDtos<OrderDto>();

    [HttpGet("{id}")]
    public async Task GetById(int id)
        => await Reader2.GetDtoById<OrderDetailsDto>(id);

    [HttpPost]
    public async Task<int> AddNew([FromBody] CreateOrderDto dto)
        => await Creator.AddEntity(dto);
}
