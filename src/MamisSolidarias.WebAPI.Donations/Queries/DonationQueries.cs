using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using MamisSolidarias.Infrastructure.Donations;
using MamisSolidarias.Infrastructure.Donations.Models;

namespace MamisSolidarias.WebAPI.Donations.Queries;

[ExtendObjectType("Query")]
public class DonationQueries
{

    [Authorize(Policy = "CanRead")]    
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<MonetaryDonation> GetMonetaryDonation(DonationsDbContext dbContext, Guid id)
        => dbContext.MonetaryDonations.Where(t => t.Id == id);

}