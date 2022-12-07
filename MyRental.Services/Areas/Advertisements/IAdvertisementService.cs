using MyRental.Services.Areas.Advertisements.Dto;

namespace MyRental.Services.Areas.Advertisements;

public interface IAdvertisementService
{
    public Task<IList<AdvertisementDto>> GetListAsync(AdvertisementFilterDto advertisementFilter);
    public Task<AdvertisementDto> GetByIdAsync(int id);
    public Task<int> CreateAsync(AdvertisementDtoInput advertisementInput);
    public Task<int> UpdateByIdAsync(int id, AdvertisementDtoInput advertisementInput);
    public Task DeleteByIdAsync(int id);
}