using FastEndpoints;
using MamisSolidarias.Infrastructure.Donations;

namespace MamisSolidarias.WebAPI.Donations.Endpoints.Test;

internal class Endpoint : Endpoint<Request, Response>
{
    private readonly DbService _db;

    public Endpoint(DonationsDbContext dbContext, DbService? db)
    {
        _db = db ?? new DbService(dbContext);
    }

    public override void Configure()
    {
        Get("user/{Name}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Logger.LogInformation("Hello");
        if (req.Name != "lucas")
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendAsync(new Response
        {
            Email = "mymail@mail.com",
            Id = new Random().Next(1,10),
            Name = "Lucassss"
        }, cancellation: ct);
    }
}