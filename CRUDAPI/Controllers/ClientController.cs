using AutoMapper;
using CRUDAPI.Dtos;
using CRUDAPI.Entities;
using CRUDAPI.Infrastructure;
using CrudCore.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Controllers;

[Route("api/client")]
public class ClientController : EntityControllerTemplate<Client, ClientDto, CreateClientDto>
{
    public ClientController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
