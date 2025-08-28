using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Look for appsettings in WebApi project by default, then Infrastructure
        var basePath = Directory.GetCurrentDirectory();
        var webApiPath = Path.Combine(basePath, "..", "WebApi");
        var configBasePath = Directory.Exists(webApiPath) ? webApiPath : basePath;

        var builder = new ConfigurationBuilder()
            .SetBasePath(configBasePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables();


        var config = builder.Build();
        var conn = config.GetConnectionString("Default") ??
                   "Server=(localdb)\\MSSQLLocalDB;Database=RealEstate;Trusted_Connection=True;TrustServerCertificate=True;";

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(conn);

        return new AppDbContext(optionsBuilder.Options);
    }
}
