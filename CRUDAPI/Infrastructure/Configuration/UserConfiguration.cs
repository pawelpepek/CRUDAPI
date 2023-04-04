using CRUDAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.Login)
               .HasMaxLength(25)
               .IsRequired();

        builder.HasOne(p => p.Role)
               .WithMany(r=>r.Users)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();
    }
}