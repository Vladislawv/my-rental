using Microsoft.EntityFrameworkCore;
using MyRental.Infrastructure.Entities;
using MyRental.Infrastructure.Enums;

namespace MyRental.Infrastructure.Seeders;

public class RolesSeeder : ISeeder<Role>
{
    public void Seed(ModelBuilder builder)
    {
        var roles = ((RoleType[])Enum.GetValues(typeof(RoleType)))
            .Select(roleType => new Role
            {
                Id = (int)roleType,
                Name = roleType.ToString(),
                NormalizedName = roleType.ToString().Normalize(),
                Type = roleType
            });

        builder.Entity<Role>().HasData(roles);
    }
}