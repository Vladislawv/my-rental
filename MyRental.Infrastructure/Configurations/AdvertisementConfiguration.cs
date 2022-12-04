using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRental.Infrastructure.Entities;

namespace MyRental.Infrastructure.Configurations;

public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        builder.HasOne(advertisement => advertisement.User)
            .WithMany(user => user.Advertisements)
            .HasForeignKey(advertisement => advertisement.UserId);
    }
}