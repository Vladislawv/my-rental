using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using MyRental.Services.Areas.Users;

namespace MyRental.Api.Controllers;

/// <summary>
/// Controller to subscribe/unsubscribe to notifications
/// </summary>
[ApiController]
[Route("api/notifications")]
[Produces(MediaTypeNames.Application.Json)]
public class NotificationController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userService"></param>
    public NotificationController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Subscribe to notifications
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SubscribeToNotificationsAsync([FromBody] string email)
    {
        await _userService.SubscribeToNotificationsAsync(email);

        return Ok();
    }

    /// <summary>
    /// Unsubscribe from notifications
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UnsubscribeFromNotificationsAsync([FromBody] string email)
    {
        await _userService.UnsubscribeFromNotificationsAsync(email);

        return Ok();
    }
}