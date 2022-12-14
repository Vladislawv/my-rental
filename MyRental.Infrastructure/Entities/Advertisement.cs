namespace MyRental.Infrastructure.Entities;

public class Advertisement : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Area { get; set; }
    public string Title { get; set; }
    public int Rooms { get; set; }
    public int Square { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<MediaFile> Medias { get; set; }
}