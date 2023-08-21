using FastEndpoints;
using MamisSolidarias.Infrastructure.Donations;
using MamisSolidarias.Infrastructure.Donations.Models;

namespace MamisSolidarias.WebAPI.Donations.Endpoints.Monetary.Program.POST;

internal sealed class Endpoint : Endpoint<Request, Response>
{
    private readonly DonationsDbContext _dbContext;

    public Endpoint(DonationsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("monetary/program");
        Policies(Utils.Security.Policies.CanWrite);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var donation = Map(req);
        
        await _dbContext.MonetaryDonations.AddAsync(donation, ct);
        await _dbContext.SaveChangesAsync(ct);

        await SendAsync(new Response(donation.Id), 201, ct);
    }

    private static MonetaryDonation Map(Request request)
    {
        return new()
        {
            DonorId = request.DonorId,
            Motive = $"{request.Program}",
            Type = DonationType.Recurring,
            Amount = request.Amount,
            Currency = request.Currency
        };
    }
}