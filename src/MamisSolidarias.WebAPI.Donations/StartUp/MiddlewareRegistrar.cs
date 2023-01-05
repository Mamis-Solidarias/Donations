using FastEndpoints;
using FastEndpoints.Swagger;
using MamisSolidarias.Infrastructure.Donations;
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
        app.UseFastEndpoints();
        app.MapGraphQL();
        app.RunMigrations();

        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(t => t.ConfigureDefaults());
        }
    }
}