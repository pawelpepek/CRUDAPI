using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRUDAPI.Entities;


[DisplayName("klient")]
public class Client:IIdentifiable
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public virtual List<Order> Orders { get; set; }

}
