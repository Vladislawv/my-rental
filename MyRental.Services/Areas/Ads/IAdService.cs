using MyRental.Services.Areas.Ads.Dto;

namespace MyRental.Services.Areas.Ads;

public interface IAdService
{
    public Task<IList<AdDto>> GetListAsync();
    public Task<IList<AdDto>> GetUserListAsync(int id);
    public Task<IList<AdDto>> GetFilteredListAsync(FilterDtoInput filter);
    public Task<AdDto> GetByIdAsync(int id);
    public Task<int> CreateAsync(AdDtoInput adInput);
    public Task<int> UpdateByIdAsync(int id, AdDtoInput adInput);
    public Task DeleteByIdAsync(int id);
}