using System;
using Bogus;
using MamisSolidarias.Infrastructure.Donations;
using MamisSolidarias.Infrastructure.Donations.Models;
using MamisSolidarias.Messages;

namespace MamisSolidarias.WebAPI.Donations.Utils;

public class MonetaryDonationBuilder
{
    private static readonly Faker<MonetaryDonation> MonetaryDonation = new Faker<MonetaryDonation>()
        .RuleFor(t => t.Id, f => f.Random.Uuid())
        .RuleFor(t => t.Currency, f => f.PickRandom<Currency>())
        .RuleFor(t => t.DonatedAt, f => f.Date.Past())
        .RuleFor(t => t.Amount, f => f.Random.Decimal(1, 1000))
        .RuleFor(t => t.DonorId, f => f.Random.Int(1, 1000).OrNull(f))
        .RuleFor(t => t.Motive, f => f.Lorem.Sentence())
        .RuleFor(t => t.Type, f => f.PickRandom<DonationType>());

    private readonly DonationsDbContext? _dbContext;
    private readonly MonetaryDonation _monetaryDonation = MonetaryDonation.Generate();
    public MonetaryDonationBuilder(DonationsDbContext? dbContext)
     => _dbContext = dbContext;

    public MonetaryDonationBuilder(MonetaryDonation? obj = null)
        => _monetaryDonation = obj ?? MonetaryDonation.Generate();
    public MonetaryDonation Build()
    {
        _dbContext?.MonetaryDonations.Add(_monetaryDonation);
        _dbContext?.SaveChanges();
        _dbContext?.ChangeTracker.Clear();
        return _monetaryDonation;
    }
    
    public static implicit operator MonetaryDonation(MonetaryDonationBuilder builder) => builder.Build();
    public static implicit operator MonetaryDonationBuilder(MonetaryDonation donation) => new (donation);
    
    public MonetaryDonationBuilder WithId(Guid id)
    {
        _monetaryDonation.Id = id;
        return this;
    }
    
    public MonetaryDonationBuilder WithCurrency(Currency currency)
    {
        _monetaryDonation.Currency = currency;
        return this;
    }
    
    public MonetaryDonationBuilder WithDonatedAt(DateTime donatedAt)
    {
        _monetaryDonation.DonatedAt = donatedAt;
        return this;
    }
    
    public MonetaryDonationBuilder WithAmount(decimal amount)
    {
        _monetaryDonation.Amount = amount;
        return this;
    }
    
    public MonetaryDonationBuilder WithDonorId(int? donorId)
    {
        _monetaryDonation.DonorId = donorId;
        return this;
    }
    
    public MonetaryDonationBuilder WithMotive(string motive)
    {
        _monetaryDonation.Motive = motive;
        return this;
    }

    public MonetaryDonationBuilder WithType(DonationType type)
    {
        _monetaryDonation.Type = type;
        return this;
    }
    
}