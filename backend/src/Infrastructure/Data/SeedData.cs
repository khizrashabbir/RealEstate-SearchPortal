using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class SeedData
{
    public static async Task EnsureSeededAsync(AppDbContext db)
    {
    // Apply any pending migrations
    await db.Database.MigrateAsync();

        if (!await db.Properties.AnyAsync())
        {
            db.Properties.AddRange(
                new Property { Title = "Sunny Family Home", Address = "12 Oak St", City = "Springfield", Price = 650000, ListingType = ListingType.Sale, Bedrooms = 4, Bathrooms = 2, CarSpots = 2, Description = "Spacious family home with a large backyard." },
                new Property { Title = "Modern Apartment", Address = "99 King Rd Apt 8", City = "Riverton", Price = 520, ListingType = ListingType.Rent, Bedrooms = 2, Bathrooms = 1, CarSpots = 1, Description = "Close to shops and public transport." },
                new Property { Title = "Beachside Cottage", Address = "5 Ocean View", City = "Seabreeze", Price = 1200000, ListingType = ListingType.Sale, Bedrooms = 3, Bathrooms = 2, CarSpots = 2, Description = "Stunning ocean views." }
            );
            await db.SaveChangesAsync();
        }
    }
}
