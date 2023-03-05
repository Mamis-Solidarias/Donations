using HotChocolate.Diagnostics;
using MamisSolidarias.Infrastructure.Donations;
using MamisSolidarias.Utils.Security;
using StackExchange.Redis;

namespace MamisSolidarias.WebAPI.Donations.Extensions;

internal static class GraphQlExtensions
{

    public static void AddGraphQl(this IServiceCollection services, IConfiguration configuration,
        ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger("GraphQL");

        var builder = services.AddGraphQLServer()
            .AddQueryType(t => t.Name("Query"))
            .AddDonationsTypes()
            .AddInstrumentation(t =>
            {
                t.Scopes = ActivityScopes.All;
                t.IncludeDocument = true;
                t.RequestDetails = RequestDetails.All;
                t.IncludeDataLoaderKeys = true;
            })
            .AddAuthorization()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .RegisterDbContext<DonationsDbContext>()
            .InitializeOnStartup();

        var options = configuration.GetSection("GraphQl").Get<GraphQlOptions>();

        if (options is null)
        {
            logger.LogWarning("GraphQl gateway options not found");
            return;
        }

        builder.PublishSchemaDefinition(t =>
        {
            t.SetName($"{Services.Donations}gql");
            t.AddTypeExtensionsFromFile("./Stitching.graphql");
            t.PublishToRedis(options.GlobalSchemaName,
                sp => sp.GetRequiredService<ConnectionMultiplexer>()
            );
        });
    }

    private sealed record GraphQlOptions(string GlobalSchemaName);
}