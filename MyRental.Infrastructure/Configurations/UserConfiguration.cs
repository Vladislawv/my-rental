using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRental.Infrastructure.Entities;

namespace MyRental.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    { 
        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<UserRole>(
                ur => ur.HasOne(userRole => userRole.Role)
                    .WithMany()
                    .HasForeignKey(userRole => userRole.RoleId),
                ur => ur.HasOne(userRole => userRole.User)
                    .WithMany()
                    .HasForeignKey(userRole => userRole.UserId));

        builder.Navigation(u => u.Roles).AutoInclude();
    }
}