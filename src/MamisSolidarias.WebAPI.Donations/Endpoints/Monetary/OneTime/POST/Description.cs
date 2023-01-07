using FastEndpoints;
using MamisSolidarias.Infrastructure.Donations.Models;

namespace MamisSolidarias.WebAPI.Donations.Endpoints.Monetary.OneTime.POST;

internal class Description : Summary<Endpoint>
{
    public Description()
    {
        Description = "Create a new one time donation";
        ExampleRequest = new Request
        {
            DonorId = 1,
            Amount = 100,
            Currency = Currency.ARS
        };
        Response(201,"Created new one time donation");
        Response(400);
        Response(401);
        Response(403);
    }
}