using Domain.Entities;

namespace Application.DTOs;

public record PropertyFilterDto(decimal? MinPrice, decimal? MaxPrice, int? Bedrooms, string? City, ListingType? ListingType);

public record PropertyDto(
    long Id,
    string Title,
    string Address,
    string City,
    decimal Price,
    ListingType ListingType,
    int Bedrooms,
    int Bathrooms,
    int CarSpots,
    string Description
);
