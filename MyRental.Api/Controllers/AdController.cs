﻿using System.Net.Mime;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRental.Services.Areas.Ads.Dto;
using MyRental.Services.Areas.Ads.Services;

namespace MyRental.Api.Controllers;

/// <summary>
/// Controller to manage Ad data
/// </summary>
[ApiController]
[Route("api/adverts")]
[Produces(MediaTypeNames.Application.Json)]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AdController : ControllerBase
{
    private readonly IAdService _adService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="adService"></param>
    public AdController(IAdService adService)
    {
        _adService = adService;
    }

    /// <summary>
    /// Get List of all Ads
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<AdDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync()
    {
        var ads = await _adService.GetListAsync();

        return Ok(ads);
    }

    /// <summary>
    /// Get Ad by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AdDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var ad = await _adService.GetByIdAsync(id);

        return Ok(ad);
    }

    /// <summary>
    /// Create Ad
    /// </summary>
    /// <param name="adInput"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(AdDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] AdDtoInput adInput)
    {
        var adId = await _adService.CreateAsync(adInput);
        var ad = await _adService.GetByIdAsync(adId);

        return Ok(ad);
    }

    /// <summary>
    /// Update Ad by Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="adInput"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(AdDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateByIdAsync([FromRoute] int id, [FromBody] AdDtoInput adInput)
    {
        var adId = await _adService.UpdateByIdAsync(id, adInput);
        var ad = await _adService.GetByIdAsync(adId);

        return Ok(ad);
    }

    /// <summary>
    /// Delete Ad by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
    {
        await _adService.DeleteByIdAsync(id);

        return Ok();
    }
}