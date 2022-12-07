using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRental.Infrastructure.Entities;

namespace MyRental.Infrastructure.Configurations;

public class MediaFileConfiguration : IEntityTypeConfiguration<MediaFile>
{
    public void Configure(EntityTypeBuilder<MediaFile> builder)
    {
        builder.HasOne(file => file.Advertisement)
            .WithMany(ad => ad.Medias)
            .HasForeignKey(file => file.AdvertisementId);
    }
}