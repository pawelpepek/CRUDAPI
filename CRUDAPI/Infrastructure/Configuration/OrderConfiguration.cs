using CRUDAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Infrastructure.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(p => p.Client)
               .WithMany(c => c.Orders)
               .HasForeignKey(p => p.ClientId)
               .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasMany(p => p.Products)
               .WithOne(pa => pa.Order)
               .OnDelete(DeleteBehavior.ClientCascade);
    }
}