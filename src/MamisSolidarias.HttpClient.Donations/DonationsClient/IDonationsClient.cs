using MamisSolidarias.WebAPI.Donations.Endpoints.Test;

namespace MamisSolidarias.HttpClient.Donations.DonationsClient;

public interface IDonationsClient
{
    Task<Response?> GetTestAsync(Request requestParameters, CancellationToken token = default);
}