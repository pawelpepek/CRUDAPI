using CRUDAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Infrastructure;
public interface IApplicationDbContext
{
    DbSet<Product> Products { get; set; }
}