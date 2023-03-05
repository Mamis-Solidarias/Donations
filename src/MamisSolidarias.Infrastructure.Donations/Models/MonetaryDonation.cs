using MamisSolidarias.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MamisSolidarias.Infrastructure.Donations.Models;

public class MonetaryDonation : Donation
{
    /// <summary>
    /// Amount of the donation
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Currency of the donation. It is always in ARS, USD or EUR.
    /// By default, it is ARS.
    /// </summary>
    public Currency Currency { get; set; } = Currency.ARS;
}

public class MonetaryDonationConfigurator : DonationConfigurator<MonetaryDonation>
{
    public new void Configure(EntityTypeBuilder<MonetaryDonation> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Amount).IsRequired();
        builder.Property(x => x.Currency)
            .HasConversion(
                t=> t.ToString(),
                t => Enum.Parse<Currency>(t, true)
                )
            .IsRequired();
    }
}