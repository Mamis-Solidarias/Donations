using MamisSolidarias.HttpClient.Donations.Models;
using MamisSolidarias.HttpClient.Donations.DonationsClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace MamisSolidarias.HttpClient.Donations;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// It registers the DonationsHttpClient using dependency injection
    /// </summary>
    /// <param name="builder"></param>
    public static void AddDonationsHttpClient(this WebApplicationBuilder builder)
    {
        var configuration = new DonationsConfiguration();
        builder.Configuration.GetSection("DonationsHttpClient").Bind(configuration);
        ArgumentNullException.ThrowIfNull(configuration.BaseUrl);
        ArgumentNullException.ThrowIfNull(configuration.Timeout);
        ArgumentNullException.ThrowIfNull(configuration.Retries);

        builder.Services.AddSingleton<IDonationsClient, DonationsClient.DonationsClient>();
        builder.Services.AddHttpClient("Donations", client =>
        {
            client.BaseAddress = new Uri(configuration.BaseUrl);
            client.Timeout = TimeSpan.FromMilliseconds(configuration.Timeout);
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");
        })
            .AddTransientHttpErrorPolicy(t =>
            t.WaitAndRetryAsync(configuration.Retries,
                retryAttempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, retryAttempt)))
        );
    }
}