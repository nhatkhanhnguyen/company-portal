using CompanyPortal.Core.Common;

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.Data.Database.Entities;

[Index(nameof(Name))]
public class Category : EntityBase
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(5)]
    public string ExternalId { get; set; } = string.Empty;

    public virtual ICollection<Product> Products { get; set; } = [];
}
