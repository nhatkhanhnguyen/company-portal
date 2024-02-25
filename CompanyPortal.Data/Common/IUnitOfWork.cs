namespace CompanyPortal.Data.Common;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}