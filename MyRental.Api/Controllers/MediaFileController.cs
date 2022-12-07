using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRental.Services.Areas.Medias;
using MyRental.Services.Areas.Medias.Dto;

namespace MyRental.Api.Controllers;

[ApiController]
[Route("api/media-files")]
[Produces(MediaTypeNames.Application.Json)]
[Authorize]
public class MediaFileController : ControllerBase
{
    private readonly IMediaService _mediaService;

    public MediaFileController(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    /// <summary>
    /// Get Media by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MediaDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var media = await _mediaService.GetByIdAsync(id);

        return Ok(media);
    }

    /// <summary>
    /// Create Media
    /// </summary>
    /// <param name="mediaInput"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(MediaDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] MediaDtoInput mediaInput)
    {
        var mediaId = await _mediaService.CreateAsync(mediaInput);
        var media = await _mediaService.GetByIdAsync(mediaId);
        
        return Ok(media);
    }

    /// <summary>
    /// Delete Media by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
    {
        await _mediaService.DeleteByIdAsync(id);

        return Ok();
    }
}