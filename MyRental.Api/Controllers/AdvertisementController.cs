using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRental.Services.Areas.Advertisements;
using MyRental.Services.Areas.Advertisements.Dto;

namespace MyRental.Api.Controllers;

[ApiController]
[Route("api/advertisements")]
[Produces(MediaTypeNames.Application.Json)]
public class AdvertisementController : ControllerBase
{
    private readonly IAdvertisementService _advertisementService;
    
    public AdvertisementController(IAdvertisementService advertisementService)
    {
        _advertisementService = advertisementService;
    }

    /// <summary>
    /// Get List of Advertisements
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    [ProducesResponseType(typeof(IList<AdvertisementDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync([FromQuery] AdvertisementFilterDto advertisementFilter)
    {
        var advertisements = await _advertisementService.GetListAsync(advertisementFilter);

        return Ok(advertisements);
    }

    /// <summary>
    /// Get Advertisement by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AdvertisementDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var ad = await _advertisementService.GetByIdAsync(id);

        return Ok(ad);
    }

    /// <summary>
    /// Create Advertisement
    /// </summary>
    /// <param name="advertisementInput"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(AdvertisementDto), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromBody] AdvertisementDtoInput advertisementInput)
    {
        var advertisementId = await _advertisementService.CreateAsync(advertisementInput);
        var advertisement = await _advertisementService.GetByIdAsync(advertisementId);

        return Ok(advertisement);
    }

    /// <summary>
    /// Update Advertisement by Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="advertisementInput"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(AdvertisementDto), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> UpdateByIdAsync([FromRoute] int id, 
        [FromBody] AdvertisementDtoInput advertisementInput)
    {
        var advertisementId = await _advertisementService.UpdateByIdAsync(id, advertisementInput);
        var advertisement = await _advertisementService.GetByIdAsync(advertisementId);

        return Ok(advertisement);
    }

    /// <summary>
    /// Delete Advertisement by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
    {
        await _advertisementService.DeleteByIdAsync(id);

        return Ok();
    }
}