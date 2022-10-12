using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRental.Infrastructure.Entities;

namespace MyRental.Infrastructure.Configurations;

public class AdConfiguration : IEntityTypeConfiguration<Ad>
{
    public void Configure(EntityTypeBuilder<Ad> builder)
    {
        builder.HasOne(ad => ad.User)
            .WithMany(user => user.Ads)
            .HasForeignKey(ad => ad.UserId);
    }
}