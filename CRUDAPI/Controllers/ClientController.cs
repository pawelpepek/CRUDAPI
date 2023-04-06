using AutoMapper;
using CRUDAPI.Dtos;
using CRUDAPI.Entities;
using CRUDAPI.Infrastructure;
using CrudCore.API;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPI.Controllers;

[Route("api/client")]
public class ClientController : EntityControllerTemplate<Client, ClientDto, CreateClientDto>
{
    public ClientController(ICRUDService<Client> crud) : base(crud) { }
}

public class ClientService : CRUDService<Client>
{
    public ClientService(ApplicationDbContext context, IMapper mapper, CancellationToken cancellationToken)
        : base(context, mapper, cancellationToken) { }
    public ClientService(ApplicationDbContext context, IMapper mapper)
      : base(context, mapper)
    { }
}
