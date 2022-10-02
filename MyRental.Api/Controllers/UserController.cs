using System.Net.Mime;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRental.Services.Areas.Users.Dto;
using MyRental.Services.RoleService;
using MyRental.Services.UserService;

namespace MyRental.Api.Controllers;

/// <summary>
/// Controller to manage User data
/// </summary>
[ApiController]
[Route("api/users")]
[Produces(MediaTypeNames.Application.Json)]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userService"></param>
    /// <param name="roleService"></param>
    public UserController(IUserService userService, IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
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
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] UserDtoInput userInput)
    {
        var id = await _userService.CreateAsync(userInput);
        var user = await _userService.GetByIdAsync(id);
        
        return Ok(user);
    }

    /// <summary>
    /// Update User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userInput"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateByIdAsync([FromRoute] int id, [FromBody] UserDtoInput userInput)
    {
        var userId = await _userService.UpdateByIdAsync(id, userInput);
        var user = await _userService.GetByIdAsync(userId);
        
        return Ok(user);
    }

    /// <summary>
    /// Delete User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
    {
        var result = await _userService.DeleteByIdAsync(id);

        return Ok(result);
    }

    /// <summary>
    /// Add Admin role to User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("roles/{id:int}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddAdminRoleByIdAsync([FromRoute] int id)
    {
        var result = await _roleService.AddAdminRoleByIdAsync(id);

        return Ok(result);
    }

    /// <summary>
    /// Remove Admin role from User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("roles/{id:int}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveAdminRoleByIdAsync([FromRoute] int id)
    {
        var result = await _roleService.RemoveAdminRoleByIdAsync(id);

        return Ok(result);
    }
}