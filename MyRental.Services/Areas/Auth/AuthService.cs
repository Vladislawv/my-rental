using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyRental.Services.Areas.Notifications;
using MyRental.Services.Areas.Users;
using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Services.Areas.Auth;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly INotificationService _notificationService;

    public AuthService(IUserService userService, INotificationService notificationService, IConfiguration configuration)
    {
        _userService = userService;
        _notificationService = notificationService;
        _configuration = configuration;
    }

    public async Task RegisterAsync(bool isSubscribed, UserDtoInput userInput)
    {
        await _userService.CreateAsync(userInput);

        if (isSubscribed) await _notificationService.SubscribeToNotificationsAsync(userInput.Email);
        
        await _notificationService.NotifyOfRegisterAsync(userInput.Email);
    }

    public async Task<string> LoginAsync(LoginDto login)
    {
        var loggedInUser = await _userService
            .GetByLoginAsync(login);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, loggedInUser.Id + ""),
            new Claim(ClaimTypes.Name, loggedInUser.UserName),
            new Claim(ClaimTypes.Email, loggedInUser.Email),
            new Claim(ClaimTypes.MobilePhone, loggedInUser.PhoneNumber),
            new Claim(ClaimTypes.Role, loggedInUser.Roles.ElementAt(0).Name)
        };
        
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(40),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                SecurityAlgorithms.HmacSha256)
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}