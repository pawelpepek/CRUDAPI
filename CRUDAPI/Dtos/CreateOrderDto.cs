using CRUDAPI.Entities;

namespace CRUDAPI.Dtos;

public class CreateOrderDto
{
    public int ClientId { get; set; }
    public List<int> ProductsIds { get; set; }
}
