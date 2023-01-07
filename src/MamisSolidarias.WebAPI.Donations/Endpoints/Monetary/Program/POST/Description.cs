using FastEndpoints;
using MamisSolidarias.Infrastructure.Donations.Models;

namespace MamisSolidarias.WebAPI.Donations.Endpoints.Monetary.Program.POST;

internal class Description: Summary<Endpoint>
{
    public Description()
    {
        Description = "Create a new recurrent donation";
        ExampleRequest = new Request
        {
            DonorId = 1,
            Amount = 100,
            Currency = Currency.ARS,
            Program = "15 meriendas"
        };
        Response(201,"Created new recurrent donation");
        Response(400);
        Response(401);
        Response(403);
    }
}