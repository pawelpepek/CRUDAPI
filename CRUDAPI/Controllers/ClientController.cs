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
[Authorize(Roles = RoleNames.Admin)]
public class ClientController
    : EntityControllerTemplate<Client, ClientDto, ClientSimplyDto, CreateClientDto>
{
    public ClientController(ApplicationDbContext context, IMapper mapper)
        : base(context, mapper) { }
}
