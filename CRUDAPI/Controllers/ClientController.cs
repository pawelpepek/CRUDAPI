using CRUDAPI.Dtos;
using CRUDAPI.Entities;
using CRUDAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPI.Controllers;

[ApiController]
[Route("api/client")]
public class ClientController : ControllerBase
{
    private readonly ICRUDService<Client> _crud;

    public ClientController(ICRUDService<Client> crud)
        => _crud = crud;

    [HttpPost]
    public async Task<int> AddNew([FromBody] CreateClientDto dto)
        => await _crud.AddEntity(dto);

    [HttpGet]
    public async Task<List<ClientDto>> GetAll()
        => await _crud.GetDtos<ClientDto>();

    [HttpGet("{id}")]
    public async Task<ClientDto> GetProduct(int id)
        => await _crud.GetDtoById<ClientDto>(id);

    [HttpPut("{id}")]
    public async Task Update(int id, [FromBody] CreateClientDto dto)
        => await _crud.UpdateEntity(id, dto);

    [HttpDelete("{id}")]
    public async Task Remove(int id)
        => await _crud.RemoveEntity(id);
}