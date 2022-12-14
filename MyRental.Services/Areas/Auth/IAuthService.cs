using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Services.Areas.Auth;

public interface IAuthService
{
    public Task RegisterAsync(bool isSubscribed, UserDtoInput userInput);
    public Task<string> LoginAsync(LoginDto login);
}