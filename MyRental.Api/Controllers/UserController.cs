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
    public async Task<IActionResult> GetListAsync()
    {
        var users = await _userService.GetListAsync();

        return Ok(users);
    }

    /// <summary>
    /// Get User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var user = await _userService.GetByIdAsync(id);
        
        return Ok(user);
    }

    /// <summary>
    /// Create new User
    /// </summary>
    /// <param name="userInput"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] UserDtoInput userInput)
    {
        var id = await _userService.CreateAsync(userInput);

        return Ok(id);
    }

    /// <summary>
    /// Update User
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userInput"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UserDtoInput userInput)
    {
        await _userService.UpdateAsync(id, userInput);
        
        return Ok();
    }

    /// <summary>
    /// Delete User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
    {
        await _userService.DeleteByIdAsync(id);

        return Ok();
    }
}