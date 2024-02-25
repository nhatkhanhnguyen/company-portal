using CompanyPortal.Core.Common;

using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.Data.Database.Entities;
public class Category : EntityBase
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Product> Products { get; set; } = [];
}
