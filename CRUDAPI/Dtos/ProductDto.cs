using CRUDAPI.Entities.Enums;

namespace CRUDAPI.Dtos;

public class ProductDto : CreateProductDto
{
    public int Id { get; set; }
}
