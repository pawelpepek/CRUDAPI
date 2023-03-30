using CRUDAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Infrastructure.Configuration;

public class ProductAmountConfiguration : IEntityTypeConfiguration<ProductAmount>
{
    public void Configure(EntityTypeBuilder<ProductAmount> builder)
    {
        builder.HasOne(p => p.Product)
               .WithMany(pr=>pr.ProductAmounts)
               .HasForeignKey(p => p.ProductId)
               .OnDelete(DeleteBehavior.ClientCascade);

        builder.Property(p => p.Amount)
               .IsRequired()
               .HasColumnType("decimal(8,2)");
    }
}
