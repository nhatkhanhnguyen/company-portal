using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.Core.Common;

public abstract class EntityBase
{
    [Key]
    public int Id { get; set; }

    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? DateModified { get; set; }

    public DateTimeOffset? DateDeleted { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public string? DeletedBy { get; set; }

    public string? ModidifedBy { get; set; }

    public bool IsActive { get; set; } = true;

    public virtual bool IsTransient()
    {
        return Id <= 0;
    }
}
