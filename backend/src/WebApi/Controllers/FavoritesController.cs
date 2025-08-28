using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("favorites")]
[Authorize]
public class FavoritesController : ControllerBase
{
    private readonly IFavoriteRepository _favorites;
    public FavoritesController(IFavoriteRepository favorites) => _favorites = favorites;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
    var idClaim = User.FindFirst("sub")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    if (idClaim == null || !long.TryParse(idClaim, out var userId)) return Unauthorized();
    var items = await _favorites.GetFavoritesAsync(userId);
        return Ok(items);
    }
}
