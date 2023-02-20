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

    [Authorize(Policy = "CanRead")]
    [UsePaging]
    [UseProjection]
    public IQueryable<MonetaryDonation> GetDonations(DonationsDbContext dbContext, DonationsFilter? filter)
    {
	    var query = dbContext.MonetaryDonations.AsQueryable();
	    
	    if (filter?.Query is not null)
	    {
		    query = query.Where(t => t.Motive.ToLower().Contains(filter.Query.ToLower()));
	    }
	    
	    if (filter?.From is not null)
	    {
		    query = query.Where(t => t.DonatedAt >= filter.From.Value.ToDateTime(TimeOnly.MinValue));
	    }

	    if (filter?.To is not null)
	    {
		    query = query.Where(t => t.DonatedAt <= filter.To.Value.ToDateTime(TimeOnly.MaxValue));
	    }
	    
	    if (filter?.DonorId is not null)
	    {
		    query = query.Where(t => t.DonorId == filter.DonorId.Value);
	    }
	    
	    return query;
    }

    public sealed record DonationsFilter(string? Query,DateOnly? From,DateOnly? To, int? DonorId);

}