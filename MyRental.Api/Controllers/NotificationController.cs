using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using MyRental.Services.Areas.Notifications;

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