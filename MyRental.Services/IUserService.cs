using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Services;

public interface IUserService
{
    public Task<IList<UserDto>> GetListAsync();
    public Task<UserDto> GetByIdAsync(int id);
    public Task CreateAsync(UserDtoInput user);
    public Task<UserDto> UpdateAsync(int id, UserDtoInput userDtoInput);
    public Task DeleteByIdAsync(int id);
}