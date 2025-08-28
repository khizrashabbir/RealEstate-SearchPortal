using Domain.Entities;

namespace Application.Interfaces;

public interface IFavoriteRepository
{
    Task ToggleAsync(long userId, long propertyId);
    Task<IEnumerable<Property>> GetFavoritesAsync(long userId);
}
