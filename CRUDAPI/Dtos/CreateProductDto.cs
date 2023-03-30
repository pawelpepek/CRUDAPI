using CRUDAPI.Entities.Enums;

namespace CRUDAPI.Dtos;

public class CreateProductDto
{
    public string Name { get; set; }
    public string Code { get; set; }
    public ProductQulity Quality { get; set; }
    public decimal Price { get; set; }
}
