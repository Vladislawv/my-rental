using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyRental.Services.Areas.Users;
using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Api.Controllers;

/// <summary>
/// Controller for Authenticate and Authorization
/// </summary>
[ApiController]
[Route("api/auth")]
[Produces(MediaTypeNames.Application.Json)]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="userService"></param>
    public AuthController(IConfiguration configuration, IUserService userService)
    {
        _configuration = configuration;
        _userService = userService;
        
    }

    /// <summary>
    /// Register User
    /// </summary>
    /// <returns></returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterAsync([FromBody] UserDtoInput userInput)
    {
        await _userService.CreateAsync(userInput);

        var login = new LoginDto
        {
            Email = userInput.Email,
            Password = userInput.Password
        };

        return await LoginAsync(login);
    }
    
    
    /// <summary>
    /// Login User
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto login)
    {
        var loggedInUser = await _userService
            .GetByLoginAsync(login);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, loggedInUser.Id + ""),
            new Claim(ClaimTypes.Name, loggedInUser.UserName),
            new Claim(ClaimTypes.Email, loggedInUser.Email),
            new Claim(ClaimTypes.MobilePhone, loggedInUser.PhoneNumber),
            new Claim(ClaimTypes.Role, await _userService.GetRoleNameByIdAsync(loggedInUser.Id))
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
        
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        
        return Ok(tokenString);
    }
}