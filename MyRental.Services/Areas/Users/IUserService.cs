using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Services.Areas.Users;

public interface IUserService
{
    public Task<IList<UserDto>> GetListAsync();
    public Task<UserDto> GetByIdAsync(int id);
    public Task<UserDto> GetByLoginAsync(LoginDto login);
    public Task<int> CreateAsync(UserDtoInput userInput);
    public Task<int> UpdateByIdAsync(int id, UserDtoInput userInput);
    public Task DeleteByIdAsync(int id);
    public Task<(bool Result, string ErrorMessage)> ValidatePasswordAsync(string password);
}