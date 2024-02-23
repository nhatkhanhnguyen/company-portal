namespace CompanyPortal.Core.Common;

public abstract class ViewModelBase
{
    public int Id { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTimeOffset DateCreated { get; set; }

    public DateTimeOffset? DateModified { get; set; }

    public DateTimeOffset? DateDeleted { get; set; }
}