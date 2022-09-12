using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using MyRental.Services;
using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Api.Controllers;

/// <summary>
/// Controller to manage User data
/// </summary>
[ApiController]
[Route("api/users")]
[Produces(MediaTypeNames.Application.Json)]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userService"></param>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    /// <summary>
    /// Get List of Users
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<UserDto>), StatusCodes.Status200OK)]
    public async Task<IList<UserDto>> GetListAsync()
    {
        var users = await _userService.GetListAsync();

        return users;
    }

    /// <summary>
    /// Get User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
    {
        var user = await _userService.GetByIdAsync(id);
        
        return Ok(user);
    }

    /// <summary>
    /// Create new User
    /// </summary>
    /// <param name="userDtoInput"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] UserDtoInput userDtoInput)
    {
        await _userService.CreateAsync(userDtoInput);

        return Ok();
    }

    /// <summary>
    /// Update User
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userDtoInput"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] UserDtoInput userDtoInput)
    {
        var user = await _userService.UpdateAsync(id, userDtoInput);
        
        return Ok(user);
    }

    /// <summary>
    /// Delete User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] string id)
    {
        await _userService.DeleteByIdAsync(id);

        return Ok();
    }
}