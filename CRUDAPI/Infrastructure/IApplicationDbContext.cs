using CRUDAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Infrastructure;
public interface IApplicationDbContext
{
    DbSet<Product> Products { get; set; }
    DbSet<Client> Clients { get; set; }
    DbSet<ProductAmount> ProductAmounts { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
}