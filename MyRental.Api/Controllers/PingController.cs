using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace MyRental.Api.Controllers;

/// <summary>
/// Ping Controller
/// </summary>
[ApiController]
[Route("api/ping")]
[Produces(MediaTypeNames.Application.Json)]
public class PingController : ControllerBase
{
    /// <summary>
    /// Ping endpoint with empty body
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> PingAsync()
    {
        return Task.FromResult<IActionResult>(Ok());
    }
}