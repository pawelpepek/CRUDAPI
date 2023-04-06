using CrudCore.Objects;
using System.ComponentModel;

namespace CRUDAPI.Entities;


[DisplayName("klient")]
public class Client:IIdentifiable
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public virtual List<Order> Orders { get; set; }

}
