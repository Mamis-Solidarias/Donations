using FastEndpoints;
using FastEndpoints.Swagger;
using MamisSolidarias.WebAPI.Donations.Extensions;

namespace MamisSolidarias.WebAPI.Donations.StartUp;

internal static class ServiceRegistrar
{
    private static ILoggerFactory CreateLoggerFactory(IConfiguration configuration)
    {
        return LoggerFactory.Create(loggingBuilder => loggingBuilder
            .AddConfiguration(configuration)
            .AddConsole()
        );
    }

    public static void Register(WebApplicationBuilder builder)
    {
        using var loggerFactory = CreateLoggerFactory(builder.Configuration);

        builder.Services.AddDataProtection(builder.Configuration);
        builder.Services.AddOpenTelemetry(builder.Configuration, builder.Logging, loggerFactory);
        builder.Services.AddRedis(builder.Configuration, loggerFactory);

        builder.Services.AddDbContext(builder.Configuration, builder.Environment, loggerFactory);

        builder.Services.AddGraphQl(builder.Configuration, loggerFactory);
        builder.Services.AddFastEndpoints();
        builder.Services.AddAuth(builder.Configuration, loggerFactory);

        if (!builder.Environment.IsProduction())
            builder.Services.AddSwaggerDoc();
    }
}