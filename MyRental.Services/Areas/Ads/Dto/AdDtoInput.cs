namespace MyRental.Services.Areas.Ads.Dto;

public class AdDtoInput
{
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
}