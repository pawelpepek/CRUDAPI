using AutoMapper;
using CRUDAPI.Dtos;
using CRUDAPI.Entities;
using CRUDAPI.Entities.Helpers;
using CRUDAPI.Infrastructure;
using CrudCore.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPI.Controllers;

[Route("api/client")]
[Authorize(Roles = RoleNames.All)]
public class ClientController : EntityControllerTemplate<Client, ClientDto, CreateClientDto>
{
    public ClientController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [AllowAnonymous]
    public override Task<List<ClientDto>> GetAll()
        => base.GetAll();

    [AllowAnonymous]
    public override Task<ClientDto> GetDtoById(int id)
        => base.GetDtoById(id);

    [Authorize(Roles = RoleNames.Admin)]
    public override Task Remove(int id)
    {
        return base.Remove(id);
    }
}
