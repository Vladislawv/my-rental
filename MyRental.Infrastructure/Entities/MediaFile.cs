namespace MyRental.Infrastructure.Entities;

public class MediaFile : IEntity
{
    public int Id { get; set; }
    public int AdvertisementId { get; set; }
    public string Data { get; set; }
    public string Name { get; set; }
    public string Extension { get; set; }
    public int Length { get; set; }
    
    public virtual Advertisement Advertisement { get; set; }
}