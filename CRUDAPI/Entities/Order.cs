using CrudCore.Objects;

namespace CRUDAPI.Entities;

public class Order : IIdentifiable
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public List<ProductAmount> Products { get; set; }

    public virtual Client Client { get; set; }
}
