using StackExchange.Redis;

namespace MamisSolidarias.WebAPI.Donations.Extensions;

internal static class RedisExtensions
{
    
    public static void AddRedis(this IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger(typeof(RedisExtensions));
        var connectionString = configuration.GetConnectionString("Redis");

        if (connectionString is null)
        {
            logger.LogError("Redis configuration not found.");
            throw new ArgumentNullException(nameof(connectionString),"Redis configuration not found.");
        }
        services.AddSingleton(ConnectionMultiplexer.Connect(connectionString));
    }
}