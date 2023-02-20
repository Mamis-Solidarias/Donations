using FastEndpoints;
using MamisSolidarias.Infrastructure.Donations;
using MamisSolidarias.Infrastructure.Donations.Models;
using MamisSolidarias.Messages;
using MassTransit;

namespace MamisSolidarias.WebAPI.Donations.Endpoints.Monetary.Campaign.POST;

internal sealed class Endpoint: Endpoint<Request,Response>
{
    private readonly DonationsDbContext _dbContext;
    private readonly IBus _bus;

    public Endpoint(DonationsDbContext dbContext, IBus bus)
    {
        _dbContext = dbContext;
        _bus = bus;
    }

    public override void Configure()
    {
        Post("monetary/campaign");
        Policies(Utils.Security.Policies.CanWrite);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var donation = Map(req);

        await _dbContext.MonetaryDonations.AddAsync(donation, ct);
        await _dbContext.SaveChangesAsync(ct);

        await _bus.Publish(Map(req,donation.Id),ct);

        await SendAsync(new Response(donation.Id), 201, ct);
    }

    private static MonetaryDonation Map(Request req)
    {
        return new()
        {
            DonorId = req.DonorId,
            Type = DonationType.Campaign,
            Amount = req.Amount,
            Currency = req.Currency,
            Motive = $"Donación de {req.Amount} {req.Currency} para la campaña {req.Campaign}"
        };
    }
    
    private static DonationAddedToCampaign Map(Request req,Guid donationId)
    {
        return new(donationId,req.DonorId ,req.ParticipantId, req.CampaignId, req.Campaign);
    }
}