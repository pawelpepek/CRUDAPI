namespace CRUDAPI.Entities;

public class Client:IIdentifiable
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public int LastName { get; set; }

    public virtual List<Order> Orders { get; set; }

}
