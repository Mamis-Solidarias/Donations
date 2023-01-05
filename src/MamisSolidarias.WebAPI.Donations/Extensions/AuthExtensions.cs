using FastEndpoints.Security;
using MamisSolidarias.Utils.Security;

namespace MamisSolidarias.WebAPI.Donations.Extensions;

internal static class AuthExtensions
{
    private sealed record JwtOptions(string Issuer,string Key);
    public static void AddAuth(this IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger("Auth");
        
        var options = configuration.GetSection("Jwt").Get<JwtOptions>();

        if (options is null)
        {
            logger.LogError("Jwt options not found");
            throw new ArgumentException("Jwt options not found");
        }
        
        services.AddJWTBearerAuth(
            options.Key,
            tokenValidation: parameters => parameters.ValidIssuer = options.Issuer
        );
        // TODO: Add Policy
        // services.AddAuthorization(t => t.ConfigurePolicies(Services.Beneficiaries));

    }
}