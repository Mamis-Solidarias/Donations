using MamisSolidarias.Infrastructure.Donations.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.Infrastructure.Donations;

public class DonationsDbContext: DbContext
{
    public DbSet<MonetaryDonation> MonetaryDonations { get; set; }
    
    public DonationsDbContext(DbContextOptions<DonationsDbContext> options) : base(options)
    {
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new MonetaryDonationConfigurator().Configure(modelBuilder.Entity<MonetaryDonation>());
    }
    
}