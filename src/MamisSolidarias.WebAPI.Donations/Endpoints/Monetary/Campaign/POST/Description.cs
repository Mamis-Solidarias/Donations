using FastEndpoints;
using MamisSolidarias.Infrastructure.Donations.Models;
using MamisSolidarias.Messages;

namespace MamisSolidarias.WebAPI.Donations.Endpoints.Monetary.Campaign.POST;

internal sealed class Description : Summary<Endpoint>
{
    public Description()
    {
        Description = "Create a new campaign donation";
        ExampleRequest = new Request
        {
            DonorId = 1,
            Amount = 100,
            Currency = Currency.ARS,
            Campaign = Campaigns.JuntosALaPar,
            CampaignId = 2
        };
        Response(201,"Created new campaign donation");
        Response(400);
        Response(401);
        Response(403);
    }
}