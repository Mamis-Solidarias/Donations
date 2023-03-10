using MamisSolidarias.HttpClient.Donations.Services;
using MamisSolidarias.Utils.Http;
using Microsoft.AspNetCore.Http;

namespace MamisSolidarias.HttpClient.Donations.DonationsClient;

public partial class DonationsClient : IDonationsClient
{
    private readonly HeaderService _headerService;
    private readonly IHttpClientFactory _httpClientFactory;
    
    public DonationsClient(IHttpContextAccessor? contextAccessor,IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _headerService = new HeaderService(contextAccessor);
    }
    
    private ReadyRequest CreateRequest(HttpMethod httpMethod,params string[] urlParams)
    {
        var client = _httpClientFactory.CreateClient("Donations");
        var request = new HttpRequestMessage(httpMethod, string.Join('/', urlParams));
        
        var authHeader = _headerService.GetAuthorization();
        if (authHeader is not null)
            request.Headers.Add("Authorization",authHeader);
        
        return new ReadyRequest(client,request);
    }
}