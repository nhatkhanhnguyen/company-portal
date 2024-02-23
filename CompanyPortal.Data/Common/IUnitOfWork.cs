namespace CompanyPortal.Data.Common;

public interface IUnitOfWork
{
    bool SaveChanges();

    Task<bool> SaveChangesAsync();
}