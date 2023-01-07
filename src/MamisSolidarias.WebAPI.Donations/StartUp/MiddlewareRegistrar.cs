using FastEndpoints;
using FastEndpoints.Swagger;
using MamisSolidarias.Infrastructure.Donations;
using MamisSolidarias.Infrastructure.Donations.Models;
using MamisSolidarias.WebAPI.Donations.CustomJsonConverters;
using MamisSolidarias.WebAPI.Donations.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Donations.StartUp;

internal static class MiddlewareRegistrar
{
    public static void Register(WebApplication app)
    {
        app.UseDefaultExceptionHandler();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseFastEndpoints(t =>
        {
            t.Endpoints.RoutePrefix = "donations";
            t.Serializer.Options.Converters.Add(new EnumToStringJsonConverter<Currency>());
        });
        app.MapGraphQL();
        app.RunMigrations();

        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(t => t.ConfigureDefaults());
        }
    }
}