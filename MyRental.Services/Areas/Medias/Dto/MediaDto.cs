namespace MyRental.Services.Areas.Medias.Dto;

public class MediaDto : EntityDto
{
    public int AdId { get; set; }
    public string Data { get; set; }
    public string Name { get; set; }
    public string Extension { get; set; }
    public int Length { get; set; }
}