using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using MyRental.Infrastructure;

namespace MyRental.Api.Controllers;

/// <summary>
/// User controller with CRUD operations of User entity
/// </summary>
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class UserController : ControllerBase
{
    private readonly MyRentalContext _context;

    /// <summary>
    /// Constructor of User controller that requires DbContext
    /// </summary>
    /// <param name="context"></param>
    public UserController(MyRentalContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Returns all users
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("/api/users")]
    public Task<IActionResult> GetAllUsersAsync()
    {
        return null;
    }

    /// <summary>
    /// Returns user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("/api/users/{id}")]
    public Task<IActionResult> GetUserByIdAsync(string id)
    {
        return null;
    }

    /// <summary>
    /// Create new user
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("/api/users")]
    public Task<IActionResult> CreateUserAsync()
    {
        return null;
    }

    /// <summary>
    /// Updates user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("/api/users/{id}")]
    public Task<IActionResult> UpdateUserAsync(string id)
    {
        return null;
    }

    /// <summary>
    /// Delete user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("/api/users/{id}")]
    public Task<IActionResult> DeleteUserAsync(string id)
    {
        return null;
    }
}