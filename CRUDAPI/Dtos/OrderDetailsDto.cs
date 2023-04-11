using CRUDAPI.Entities;

namespace CRUDAPI.Dtos;

public class OrderDetailsDto
{
    public List<ProductDto> Products { get; set; }
}
