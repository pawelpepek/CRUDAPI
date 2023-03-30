using CRUDAPI.Entities.Enums;

namespace CRUDAPI.Entities;

public class Product : IIdentifiable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public ProductQulity Quality {get;set;}
}
