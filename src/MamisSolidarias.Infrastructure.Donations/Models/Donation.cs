using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MamisSolidarias.Infrastructure.Donations.Models;

public abstract class Donation
{
    private static readonly TimeZoneInfo _timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("America/Buenos_Aires");
        
    /// <summary>
    ///     Unique identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Exact date and time of the donation in GMT+3
    /// </summary>
    public DateTime DonatedAt { get; set; } = TimeZoneInfo.ConvertTime(DateTime.Now, _timeZoneInfo);

    /// <summary>
    ///     Id of the donor, if it is not anonymous
    /// </summary>
    public int? DonorId { get; set; }

    /// <summary>
    ///     Motive of the donation. It includes extra data like which program
    ///     it came from, if it was a campaign, etc.
    /// </summary>
    public string Motive { get; set; } = string.Empty;

    /// <summary>
    ///     Type of the donation
    /// </summary>
    public DonationType Type { get; set; }
}

public abstract class DonationConfigurator<T> : IEntityTypeConfiguration<T>
    where T : Donation
{
    private static readonly TimeZoneInfo _gmt3 = TimeZoneInfo.FindSystemTimeZoneById("America/Buenos_Aires");

    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).ValueGeneratedOnAdd();
        builder.Property(d => d.DonatedAt)
            .IsRequired()
            .HasConversion(
                t => t.ToUniversalTime(),
                t => TimeZoneInfo.ConvertTimeFromUtc(t, _gmt3)
            )
            .HasColumnName("DonatedAtUTC");
        builder.Property(d => d.DonorId);
        builder.Property(d => d.Motive).IsRequired();
        builder.Property(d => d.Type)
            .HasConversion(
                t => t.ToString(),
                t => Enum.Parse<DonationType>(t, true)
            )
            .IsRequired();
    }
}