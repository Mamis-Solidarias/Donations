using MamisSolidarias.WebAPI.Donations.Endpoints.Test;

namespace MamisSolidarias.HttpClient.Donations.DonationsClient;

public partial class DonationsClient
{
    public Task<Response?> GetTestAsync(Request requestParameters, CancellationToken token = default)
    {
        return CreateRequest(HttpMethod.Get, "user", requestParameters.Name)
            .ExecuteAsync<Response>(token);

    }
}