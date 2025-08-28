using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FavoriteRepository : IFavoriteRepository
{
    private readonly AppDbContext _db;
    public FavoriteRepository(AppDbContext db) => _db = db;

    public async Task ToggleAsync(long userId, long propertyId)
    {
        var fav = await _db.Favorites.FindAsync(userId, propertyId);
        if (fav == null)
        {
            _db.Favorites.Add(new Favorite { UserId = userId, PropertyId = propertyId, CreatedAt = DateTime.UtcNow });
        }
        else
        {
            _db.Favorites.Remove(fav);
        }
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Property>> GetFavoritesAsync(long userId)
    {
        var query = from f in _db.Favorites.AsNoTracking()
                    join p in _db.Properties.AsNoTracking() on f.PropertyId equals p.Id
                    where f.UserId == userId
                    orderby f.CreatedAt descending
                    select p;
        return await query.ToListAsync();
    }
}
