using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Services.UserService;

public interface IUserService
{
    public Task<IList<UserDto>> GetListAsync();
    public Task<UserDto> GetByIdAsync(int id);
    public Task<UserDto> GetByLoginAsync(LoginDto login);
    public Task<int> CreateAsync(UserDtoInput userInput);
    public Task<int> UpdateByIdAsync(int id, UserDtoInput userInput);
    public Task<bool> DeleteByIdAsync(int id);
    public Task<string> GetRoleNameByIdAsync(int id);
    public Task<(bool Result, string ErrorMessage)> ValidatePasswordAsync(string password);
}