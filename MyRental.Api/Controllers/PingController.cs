using System.Net.Mime;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> PingAsync()
    {
        return Task.FromResult<IActionResult>(Ok());
    }
}