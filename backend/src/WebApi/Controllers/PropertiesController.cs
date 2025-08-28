using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("properties")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyRepository _props;
    private readonly IFavoriteRepository _favorites;

    public PropertiesController(IPropertyRepository props, IFavoriteRepository favorites)
    {
        _props = props;
        _favorites = favorites;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] int? bedrooms, [FromQuery] string? city, [FromQuery] ListingType? listingType)
    {
        var filter = new PropertyFilter
        {
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            Bedrooms = bedrooms,
            City = city,
            ListingType = listingType
        };
        var items = await _props.GetAsync(filter);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var item = await _props.GetByIdAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [Authorize]
    [HttpPost("/favorites/{propertyId}")]
    public async Task<IActionResult> ToggleFavorite(long propertyId)
    {
        var idClaim = User.FindFirst("sub")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (idClaim == null || !long.TryParse(idClaim, out var userId)) return Unauthorized();
        await _favorites.ToggleAsync(userId, propertyId);
        return NoContent();
    }
}
