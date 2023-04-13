# CRUDAPI and CrudCore NuGet package (net7.0)

Generic controller template implementing basic CRUD functions:
- create entity from `TCreateDto`
- read entities using `List<TDto>`
- read entity with given id using `TOneDto`
- update entity with given id from `TCreateDto`
- delete entity with given id.

You could find this NuGet [here](https://www.nuget.org/packages/Crud.EFCore.Template).

## User Manual

### Using EntityControllerTemplate class
0. Setup controller mapping loader in your API project ones for every entity:
```c#
using CrudCore.API.Mapping;

public class AppMappingProfile : MappingProfile
{
    public AppMappingProfile() : base(Assembly.GetExecutingAssembly()) { }
}
```

1. Add to the entity interface:
```c#
using CrudCore.Objects;

[DisplayName("klient")]
public class Client:IIdentifiable
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public virtual List<Order> Orders { get; set; }

}
```
(Optional) You could add name attribute to the entity (`"klient"`).

2. Create models for the entity:
```c#
public class ClientDto : CreateClientDto
{
    public int Id { get; set; }
}
public class ClientSimplyDto
{
    public int Id { get; set; }
    public string Name { get; set; }    
}
public class CreateClientDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```

3. Create AutoMapper mapping (which inherits from the SelfMapFrom generic class):
```c#
using CrudCore.API.Mapping;

public class ClientMapping : SelfMapFrom<Client>
{
    protected override void CreateMaps(Profile profile)
    {
        profile.CreateMap<Client, ClientDto>();

        profile.CreateMap<Client, ClientSimplyDto>()
               .ForMember(p => p.Name, x => x.MapFrom(c => $"{c.FirstName} {c.LastName}"));

        profile.CreateMap<ClientDto, Client>();

        profile.CreateMap<CreateClientDto, Client>();
    }
}
```

4. Create controller (which inherits from the EntityControllerTemplate generic class):
```c#
using CrudCore.API;

[Route("api/client")]
[Authorize(Roles = RoleNames.Admin)]
public class ClientController
    : EntityControllerTemplate<Client, ClientDto, ClientSimplyDto, CreateClientDto>
{
    public ClientController(ApplicationDbContext context, IMapper mapper)
        : base(context, mapper) { }
}
```

`And that's it!`

`This is the result:`

![image](https://user-images.githubusercontent.com/67840037/231821385-6da31b65-1618-40a4-a696-10492b496bfc.png)


### Customize controllers functions
You could use entity functions:
- `Creater` to create new entity,
- `Reader` to get list of entities,
- `Reader2` to get one entity with given id,
- `Updater` to update entity with given id,
- `Deleter` to delete entity with given id.

Those functions have two methods:
```c#
public EntityFunctionTemplate<TEntity> SetEntityAction(Action<TEntity> entityAction){}
public EntityFunctionTemplate<TEntity> SetQueryableFunction(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc){}
```

`SetEntityAction` validate entity:
```c#
Creator.SetEntityAction(ValidateNewEntity);

private void ValidateNewEntity(Product entity)
    {
        var duplicate = _set.FirstOrDefault(
            p => p.Name.ToLower() == entity.Name.ToLower() && p.Quality == entity.Quality);

        if (duplicate != null)
        {
            throw new CustomException($"Produkt o nazwie ${entity.Name} istnieje już w bazie danych!");
        }

        duplicate = _set.FirstOrDefault(p => p.Code == entity.Code && p.Quality == entity.Quality);

        if (duplicate != null)
        {
            throw new CustomException($"Produkt o kodzie ${entity.Code} istnieje już w bazie danych!");
        }
    }
```
`SetQueryableFunction` manipulate entity set:
```c#
Reader.SetQueryableFunction((entities) => entities.Include(p => p.ProductAmounts));
```

The `EntityControllerTemplate` class has endpoints:
```c#
    [HttpPost]
    public virtual async Task<int> AddNew([FromBody] TCreateDto dto)
       => await Creator.AddEntity(dto);

    [HttpGet]
    public virtual async Task<List<TDto>> GetAll()
        => await Reader.GetDtos<TDto>();

    [HttpGet("{id}")]
    public virtual async Task<TOneDto> GetDtoById(int id)
        => await Reader2.GetDtoById<TOneDto>(id);

    [HttpPut("{id}")]
    public virtual async Task Update(int id, [FromBody] TCreateDto dto)
        => await Updater.UpdateEntity(id, dto);

    [HttpDelete("{id}")]
    public virtual async Task Remove(int id)
        => await Deleter.RemoveEntity(id);
```


1. For hiding endpoint use:
```c#
    [NonAction]
    public override Task Remove(int id) => base.Remove(id);
```
2. For authorization off use:
```c#
    [AllowAnonymous]
    public override Task<List<ProductDto>> GetAll() => base.GetAll();
```


### Using EntityCRUD class

For make more customisation use the `EntityCRUD` class insted of the `EntityControllerTemplate` class.
The `EntityCRUD` class isn't controller, but is the container includes basic CRUD functions.

Example:
```c#
[Controller]
[Route("api/order")]
public class OrderController : EntityCRUD<Order>
{
    public OrderController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        Reader2.SetQueryableFunction(entities => entities.Include(p => p.Products));
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
```

If you have any questions write to [pawel.pepek@gmail.com](mailto:pawel.pepek@gmail.com?subject=[GitHub]%20CrudeCore)

## License

MIT [license](https://github.com/pawelpepek/ucourses/blob/main/LICENSE)
