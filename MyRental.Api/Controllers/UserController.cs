using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyRental.Infrastructure.Entities;
using MyRental.Services;
using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Api.Controllers;

/// <summary>
/// User controller with CRUD operations of User entity
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
    [ProducesResponseType(typeof(IList<User>),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync()
    {
        var userDtoList = await _userService.GetListAsync();

        var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDtoInput, User>());
        var mapper = config.CreateMapper();

        var users = userDtoList.Select(userDto => mapper.Map<User>(userDto)).ToList();
        
        return Ok(users);
    }

    /// <summary>
    /// Get User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
    {
        var userDto = await _userService.GetByIdAsync(id);

        var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDtoInput, User>());
        var mapper = config.CreateMapper();

        var user = mapper.Map<User>(userDto);
        
        return Ok(user);
    }

    /// <summary>
    /// Create new User
    /// </summary>
    /// <param name="userInput"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] User userInput)
    {
        var userDto = await _userService.CreateAsync(userInput);

        var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDtoInput, User>());
        var mapper = config.CreateMapper();

        var user = mapper.Map<User>(userDto);

        return Ok(user);
    }

    /// <summary>
    /// Update User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute] string id)
    {
        var userDto = await _userService.UpdateAsync(id);

        var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDtoInput, User>());
        var mapper = config.CreateMapper();

        var user = mapper.Map<User>(userDto);
        
        return Ok(user);
    }

    /// <summary>
    /// Delete User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id)
    {
        await _userService.DeleteAsync(id);

        return Ok();
    }
}