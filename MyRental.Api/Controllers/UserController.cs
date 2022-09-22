using System.Net.Mime;
using FluentValidation;
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
    private readonly IValidator<UserDtoInput> _validator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userService"></param>
    /// <param name="validator"></param>
    public UserController(IUserService userService, IValidator<UserDtoInput> validator)
    {
        _userService = userService;
        _validator = validator;
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
        var result = await _validator.ValidateAsync(userInput);
        if (!result.IsValid) throw new Exception(result.Errors.Aggregate("", (current, error) => current + error.ErrorMessage + " "));
        
        var id = await _userService.CreateAsync(userInput);
        var user = await _userService.GetByIdAsync(id);
        
        return Ok(user);
    }

    /// <summary>
    /// Update User
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userInput"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UserDtoInput userInput)
    {
        var result = await _validator.ValidateAsync(userInput);
        if (!result.IsValid) throw new Exception(result.Errors.Aggregate("", (current, error) => current + error.ErrorMessage + " "));
        
        var userId = await _userService.UpdateAsync(id, userInput);
        var user = await _userService.GetByIdAsync(userId);
        
        return Ok(user);
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