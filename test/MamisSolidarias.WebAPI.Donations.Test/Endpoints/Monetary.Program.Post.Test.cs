using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Donations;
using MamisSolidarias.Infrastructure.Donations.Models;
using MamisSolidarias.Messages;
using MamisSolidarias.Utils.Test;
using MamisSolidarias.WebAPI.Donations.Endpoints.Monetary.Program.POST;
using MamisSolidarias.WebAPI.Donations.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Donations.Endpoints;

public class Monetary_Program_Post_Test
{
    private DonationsDbContext _dbContext = null!;
    private Endpoint _endpoint = null!;
    
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

        _endpoint = EndpointFactory.CreateEndpoint<Endpoint>(_dbContext);
    }

    [TearDown]
    public void Dispose()
    {
        _dbContext.Dispose();
    }

    [Test]
    public async Task CreateDonation_WithDonor_Succeeds()
    {
        // Arrange
        MonetaryDonation donation = DataFactory.GetMonetaryDonation()
            .WithDonorId(123)
            .WithCurrency(Currency.EUR);
        const string program = "15 meriendas";

        var request = Map(donation,program);

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
        const string program = "15 meriendas";
        var request = Map(donation,program);

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

    private static Request Map(MonetaryDonation donation, string program)
    {
        return new()
        {
            Amount = donation.Amount,
            Currency = donation.Currency,
            DonorId = donation.DonorId,
            Program = program
        };
    }
}