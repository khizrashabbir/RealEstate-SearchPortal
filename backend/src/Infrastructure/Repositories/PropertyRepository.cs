using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly AppDbContext _db;
    public PropertyRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Property>> GetAsync(PropertyFilter filter)
    {
        var query = _db.Properties.AsNoTracking().AsQueryable();
        if (filter.MinPrice.HasValue) query = query.Where(p => p.Price >= filter.MinPrice.Value);
        if (filter.MaxPrice.HasValue) query = query.Where(p => p.Price <= filter.MaxPrice.Value);
        if (filter.Bedrooms.HasValue) query = query.Where(p => p.Bedrooms >= filter.Bedrooms.Value);
        if (!string.IsNullOrWhiteSpace(filter.City)) query = query.Where(p => p.City == filter.City);
        if (filter.ListingType.HasValue) query = query.Where(p => p.ListingType == filter.ListingType.Value);
        return await query.ToListAsync();
    }

    public async Task<Property?> GetByIdAsync(long id)
    {
        return await _db.Properties.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }
}
