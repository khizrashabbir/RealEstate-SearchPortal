namespace Domain.Entities;

public enum ListingType
{
    Rent = 0,
    Sale = 1
}

public class Property
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ListingType ListingType { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int CarSpots { get; set; }
    public string Description { get; set; } = string.Empty;
}
