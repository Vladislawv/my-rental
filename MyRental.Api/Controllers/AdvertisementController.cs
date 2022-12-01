using System.Net.Mime;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRental.Services.Areas.Advertisements;
using MyRental.Services.Areas.Advertisements.Dto;

namespace MyRental.Api.Controllers;

/// <summary>
/// Controller to manage Advertisement data
/// </summary>
[ApiController]
[Route("api/advertisements")]
[Produces(MediaTypeNames.Application.Json)]
public class AdvertisementController : ControllerBase
{
    private readonly IAdvertisementService _advertisementService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="advertisementService"></param>
    public AdvertisementController(IAdvertisementService advertisementService)
    {
        _advertisementService = advertisementService;
    }

    /// <summary>
    /// Get List of Advertisements
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<AdvertisementDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync([FromQuery] AdvertisementFilterDto advertisementFilter)
    {
        var ads = await _advertisementService.GetListAsync(advertisementFilter);

        return Ok(ads);
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateAsync([FromBody] AdvertisementDtoInput advertisementInput)
    {
        var adId = await _advertisementService.CreateAsync(advertisementInput);
        var ad = await _advertisementService.GetByIdAsync(adId);

        return Ok(ad);
    }

    /// <summary>
    /// Update Advertisement by Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="advertisementInput"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(AdvertisementDto), StatusCodes.Status200OK)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UpdateByIdAsync([FromRoute] int id, [FromBody] AdvertisementDtoInput advertisementInput)
    {
        var adId = await _advertisementService.UpdateByIdAsync(id, advertisementInput);
        var ad = await _advertisementService.GetByIdAsync(adId);

        return Ok(ad);
    }

    /// <summary>
    /// Delete Advertisement by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
    {
        await _advertisementService.DeleteByIdAsync(id);

        return Ok();
    }
}