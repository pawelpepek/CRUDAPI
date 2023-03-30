using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CRUDAPI.Entities;

namespace CRUDAPI.Infrastructure.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(t => t.Name)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(t => t.Code)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(t => t.Quality)
               .IsRequired();
    }
}