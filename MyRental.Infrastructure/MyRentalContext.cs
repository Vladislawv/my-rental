using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyRental.Infrastructure.Entities;
using MyRental.Infrastructure.Seeders;

namespace MyRental.Infrastructure;

public class MyRentalContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole,
    IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    private readonly ISeeder<Role> _rolesSeeder;
    public DbSet<MediaFile> Medias { get; set; }
    public DbSet<Advertisement> Ads { get; set; }

    public MyRentalContext(DbContextOptions<MyRentalContext> options, ISeeder<Role> rolesSeeder) : base(options)
    {
        _rolesSeeder = rolesSeeder;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(MyRentalContext).Assembly);
        
        _rolesSeeder.Seed(builder);
    }
}