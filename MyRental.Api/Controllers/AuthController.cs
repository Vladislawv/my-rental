using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyRental.Services.Areas.Users.Dto;
using MyRental.Services.Areas.Users.Services;

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
    private readonly IUserService _userService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userService"></param>
    public AuthController(IUserService userService)
    {
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
        var userId = await _userService.CreateAsync(userInput);
        var user = await _userService.GetByIdAsync(userId);
        
        return Ok(user);
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
        var loggedInUser = await _userService.GetByLoginAsync(login);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, loggedInUser.Id + ""),
            new Claim(ClaimTypes.Name, loggedInUser.UserName),
            new Claim(ClaimTypes.Email, loggedInUser.Email),
            new Claim(ClaimTypes.MobilePhone, loggedInUser.PhoneNumber),
            new Claim(ClaimTypes.Role, await _userService.GetRoleNameByIdAsync(loggedInUser.Id))
        };
        
        var token = new JwtSecurityToken(
            issuer: "https://localhost:5000/",
            audience: "https://localhost:5000/",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(40),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DPdfspdisdfsdsf8uds8u32ADSdas23fs8d4")),
                SecurityAlgorithms.HmacSha256)
        );
        
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        
        return Ok(tokenString);
    }
}