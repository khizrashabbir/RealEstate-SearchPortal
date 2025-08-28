using Domain.Entities;

namespace Application.Interfaces;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAsync(PropertyFilter filter);
    Task<Property?> GetByIdAsync(long id);
}

public class PropertyFilter
{
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? Bedrooms { get; set; }
    public string? City { get; set; }
    public ListingType? ListingType { get; set; }
}
