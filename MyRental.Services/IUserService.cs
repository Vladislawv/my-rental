using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Services;

public interface IUserService
{
    public Task<IList<UserDto>> GetListAsync();
    public Task<UserDto> GetByIdAsync(string id);
    public Task CreateAsync(UserDtoInput user);
    public Task<UserDto> UpdateAsync(string id, UserDtoInput userDtoInput);
    public Task DeleteByIdAsync(string id);
}