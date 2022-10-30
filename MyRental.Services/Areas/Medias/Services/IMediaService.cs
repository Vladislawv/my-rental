using MyRental.Services.Areas.Medias.Dto;

namespace MyRental.Services.Areas.Medias.Services;

public interface IMediaService
{
    public Task<MediaDto> GetByIdAsync(int id);
    public Task<int> CreateAsync(MediaDtoInput mediaInput);
    public Task DeleteByIdAsync(int id);
}