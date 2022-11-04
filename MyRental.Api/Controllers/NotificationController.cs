using System.Net.Mime;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRental.Services.Areas.Notifications;
using MyRental.Services.Areas.Notifications.Data;

namespace MyRental.Api.Controllers;

/// <summary>
/// Controller to subscribe/unsubscribe to notifications
/// </summary>
[ApiController]
[Route("api/notifications")]
[Produces(MediaTypeNames.Application.Json)]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="notificationService"></param>
    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    /// <summary>
    /// Get list of subscribed emails
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<string>), StatusCodes.Status200OK)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public async Task<IActionResult> GetListAsync()
    {
        var list = await _notificationService.GetListAsync();

        return Ok(list);
    }

    /// <summary>
    /// Notify subscribed emails
    /// </summary>
    /// <param name="letter"></param>
    /// <returns></returns>
    [HttpPost("send")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public async Task<IActionResult> NotifyAsync([FromBody] Letter letter)
    {
        await _notificationService.NotifyAsync(letter);

        return Ok();
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
        await _notificationService.SubscribeToNotificationsAsync(email);

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
        await _notificationService.UnsubscribeFromNotificationsAsync(email);

        return Ok();
    }
}