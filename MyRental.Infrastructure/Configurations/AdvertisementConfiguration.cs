using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRental.Infrastructure.Entities;

namespace MyRental.Infrastructure.Configurations;

public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        builder.HasOne(ad => ad.User)
            .WithMany(user => user.Advertisements)
            .HasForeignKey(ad => ad.UserId);
    }
}