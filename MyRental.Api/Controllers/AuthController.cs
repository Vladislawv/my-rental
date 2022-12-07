using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRental.Services.Areas.Auth;
using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Api.Controllers;

[ApiController]
[Route("api/auth")]
[Produces(MediaTypeNames.Application.Json)]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Register User
    /// </summary>
    /// <param name="isSubscribed"></param>
    /// <param name="userInput"></param>
    /// <returns></returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterAsync([FromQuery] bool isSubscribed, [FromBody] UserDtoInput userInput)
    {
        await _authService.RegisterAsync(isSubscribed, userInput);
        
        return Ok();
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
        var token = await _authService.LoginAsync(login);
        
        return Ok(token);
    }
}