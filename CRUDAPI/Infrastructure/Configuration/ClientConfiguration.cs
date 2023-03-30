using CRUDAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Infrastructure.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.Property(p => p.FirstName)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(p => p.LastName)
               .HasMaxLength(20)
               .IsRequired();
    }
}