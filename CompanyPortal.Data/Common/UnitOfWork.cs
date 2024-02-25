using CompanyPortal.Data.Database;

using Microsoft.Extensions.Logging;

namespace CompanyPortal.Data.Common;

public class UnitOfWork(ApplicationDbContext dbContext, ILogger<UnitOfWork> logger) : IUnitOfWork
{
    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}