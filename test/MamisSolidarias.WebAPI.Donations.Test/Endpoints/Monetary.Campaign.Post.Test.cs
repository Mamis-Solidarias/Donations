using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Donations;
using MamisSolidarias.Infrastructure.Donations.Models;
using MamisSolidarias.Messages;
using MamisSolidarias.Utils.Test;
using MamisSolidarias.WebAPI.Donations.Endpoints.Monetary.Campaign.POST;
using MamisSolidarias.WebAPI.Donations.Utils;
using MassTransit;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Donations.Endpoints;

public class Monetary_Campaign_Post_Test
{
    private DonationsDbContext _dbContext = null!;
    private Endpoint _endpoint = null!;
    private readonly Mock<IBus> _mockBus = new();

    [SetUp]
    public void SetUp()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder<DonationsDbContext>()
            .UseSqlite(connection)
            .Options;

        _dbContext = new DonationsDbContext(options);
        _dbContext.Database.EnsureCreated();

        _endpoint = EndpointFactory.CreateEndpoint<Endpoint>(_dbContext,_mockBus.Object);
    }

    [TearDown]
    public void Dispose()
    {
        _dbContext.Dispose();
        _mockBus.Reset();
    }

    [Test]
    public async Task CreateDonation_WithDonor_Succeeds()
    {
        // Arrange
        MonetaryDonation donation = DataFactory.GetMonetaryDonation()
            .WithDonorId(123)
            .WithCurrency(Currency.EUR);
        const Campaigns campaign = Campaigns.Abrigaditos;
        const int campaignId = 1;

        var request = Map(donation,campaign, campaignId, null);

        // Act
        await _endpoint.HandleAsync(request, CancellationToken.None);

        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(201);
        var id = _endpoint.Response.Id;

        var createdDonation = await _dbContext.MonetaryDonations.FindAsync(id);
        createdDonation.Should().NotBeNull();
        createdDonation!.Amount.Should().Be(donation.Amount);
        createdDonation.Currency.Should().Be(donation.Currency);
        createdDonation.DonorId.Should().Be(donation.DonorId);
    }

    [Test]
    public async Task CreateDonation_WithoutDonor_Succeeds()
    {
        // Arrange
        MonetaryDonation donation = DataFactory.GetMonetaryDonation()
            .WithDonorId(null);
        const Campaigns campaign = Campaigns.Abrigaditos;
        const int campaignId = 1;

        var request = Map(donation,campaign, campaignId, null);

        // Act
        await _endpoint.HandleAsync(request, CancellationToken.None);

        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(201);
        var id = _endpoint.Response.Id;

        var createdDonation = await _dbContext.MonetaryDonations.FindAsync(id);
        createdDonation.Should().NotBeNull();
        createdDonation!.Amount.Should().Be(donation.Amount);
        createdDonation.Currency.Should().Be(donation.Currency);
        createdDonation.DonorId.Should().BeNull();
    }

    private static Request Map(MonetaryDonation donation, Campaigns campaign, int campaignId, int? participantId)
    {
        return new()
        {
            Amount = donation.Amount,
            Currency = donation.Currency,
            DonorId = donation.DonorId,
            Campaign = campaign,
            CampaignId = campaignId,
            ParticipantId = participantId
        };
    }
}