using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CRUDAPI.Entities;

namespace CRUDAPI.Infrastructure.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(p => p.Code)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(p => p.Quality)
               .IsRequired();

        builder.Property(p => p.Price)
               .IsRequired()
               .HasColumnType("decimal(8,2)");
    }
}