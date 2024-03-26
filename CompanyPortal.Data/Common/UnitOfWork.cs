using CompanyPortal.Data.Database;

using Microsoft.Extensions.Logging;

namespace CompanyPortal.Data.Common;

public class UnitOfWork(ApplicationDbContext dbContext, ILogger<UnitOfWork> logger) : IUnitOfWork
{
    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
		try
		{
			return await dbContext.SaveChangesAsync(cancellationToken) > 0;
		}
		catch (Exception ex)
		{
			logger.LogError(ex.Message);
			return false;
		}
    }
}