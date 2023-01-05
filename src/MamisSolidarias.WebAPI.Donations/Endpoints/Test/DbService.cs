using MamisSolidarias.Infrastructure.Donations;

namespace MamisSolidarias.WebAPI.Donations.Endpoints.Test;

internal class DbService
{
    private readonly DonationsDbContext? _dbContext;

    public DbService() { }
    public DbService(DonationsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
}