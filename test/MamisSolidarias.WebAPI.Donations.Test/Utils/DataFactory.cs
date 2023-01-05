using System.Collections.Generic;
using System.Linq;
using Bogus;
using MamisSolidarias.Infrastructure.Donations;
using MamisSolidarias.Infrastructure.Donations.Models;

namespace MamisSolidarias.WebAPI.Donations.Utils;

internal class DataFactory
{
    private readonly DonationsDbContext? _dbContext;

    public DataFactory(DonationsDbContext? dbContext)
    {
        _dbContext = dbContext;
    }
    
    public static MonetaryDonationBuilder GetMonetaryDonation()
    {
        return new MonetaryDonationBuilder();
    }
    
    public MonetaryDonationBuilder GenerateMonetaryDonation()
    {
        return new MonetaryDonationBuilder(_dbContext);
    }
}