using CompanyPortal.Data.Database;

using Microsoft.Extensions.Logging;

namespace CompanyPortal.Data.Common;

public class UnitOfWork(ApplicationDbContext dbContext, ILogger<UnitOfWork> logger) : IUnitOfWork
{
    public bool SaveChanges()
    {
        try
        {
            return dbContext.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.InnerException ?? ex,
                ex.InnerException != null ? ex.InnerException.Message : ex.Message);
        }

        return false;
    }

    public async Task<bool> SaveChangesAsync()
    {
        try
        {
            return await dbContext.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.InnerException ?? ex,
                ex.InnerException != null ? ex.InnerException.Message : ex.Message);
        }

        return false;
    }
}