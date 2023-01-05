using MamisSolidarias.Infrastructure.Donations;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Donations.Extensions;

internal static class EntityFrameworkExtensions
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger(typeof(EntityFrameworkExtensions));
        
        var connectionString = configuration.GetConnectionString("DonationsDb");

        if (connectionString is null)
        {
            logger.LogError("Connection string for DonationsDb not found");
            throw new ArgumentException("Connection string not found");
        }
        
        services.AddDbContext<DonationsDbContext>(
            t =>
                t.UseNpgsql(connectionString, r =>
                        r.MigrationsAssembly("MamisSolidarias.WebAPI.Donations"))
                    .EnableSensitiveDataLogging(!env.IsProduction())
        );
    }

    public static void RunMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DonationsDbContext>();
        db.Database.Migrate();
    }
}