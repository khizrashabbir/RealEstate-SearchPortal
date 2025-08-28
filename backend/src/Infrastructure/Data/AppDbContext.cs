using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<Favorite> Favorites => Set<Favorite>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Email).IsRequired().HasMaxLength(255);
            b.HasIndex(x => x.Email).IsUnique();
            b.Property(x => x.PasswordHash).IsRequired().HasMaxLength(255);
            b.Property(x => x.CreatedAt).IsRequired();
        });

        modelBuilder.Entity<Property>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Title).IsRequired().HasMaxLength(255);
            b.Property(x => x.Address).IsRequired().HasMaxLength(255);
            b.Property(x => x.City).IsRequired().HasMaxLength(100);
            b.Property(x => x.Price).HasColumnType("decimal(18,2)").IsRequired();
            b.Property(x => x.ListingType).HasConversion<int>().IsRequired();
            b.Property(x => x.Bedrooms).IsRequired();
            b.Property(x => x.Bathrooms).IsRequired();
            b.Property(x => x.CarSpots).IsRequired();
            b.Property(x => x.Description).HasColumnType("nvarchar(max)");
        });

        modelBuilder.Entity<Favorite>(b =>
        {
            b.HasKey(x => new { x.UserId, x.PropertyId });
            b.Property(x => x.CreatedAt).IsRequired();
        });
    }
}
