using Microsoft.AspNetCore.Mvc;

namespace MyRental.Api.Controllers;

[ApiController]
[Route("controller")]
public class HomeController : ControllerBase
{
    [HttpGet("roberto")]
    public IActionResult GetRoberto()
    {
        throw new Exception();
        return Ok();
    }
}