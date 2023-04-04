using CRUDAPI.Dtos;
using CRUDAPI.Entities;
using CRUDAPI.Services.CRUD;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPI.Controllers;

[Route("api/client")]
public class ClientController : EntityControllerTemplate<Client, ClientDto, CreateClientDto>
{
    public ClientController(ICRUDService<Client> crud) : base(crud) { }
}