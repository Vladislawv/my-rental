using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Services;

public interface IUserService
{
    public Task<IList<UserDto>> GetListAsync();
    public Task<UserDto> GetByIdAsync(int id);
    public Task<int> CreateAsync(UserDtoInput userInput);
    public Task UpdateAsync(int id, UserDtoInput userInput);
    public Task DeleteByIdAsync(int id);
}