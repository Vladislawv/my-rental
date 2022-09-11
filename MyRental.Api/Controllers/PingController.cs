using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace MyRental.Api.Controllers;

/// <summary>
/// Test Controller
/// </summary>
[ApiController]
[Route("api/ping")]
[Produces(MediaTypeNames.Application.Json)]
public class PingController : ControllerBase
{
    /// <summary>
    /// Ping method with empty body
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> Ping()
    {
        return Task.FromResult<IActionResult>(Ok());
    }
}