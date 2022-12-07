using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyRental.Api.Controllers;

[ApiController]
[Route("api/ping")]
[Produces(MediaTypeNames.Application.Json)]
[Authorize(Roles = "Admin")]
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