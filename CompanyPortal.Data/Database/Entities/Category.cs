using CompanyPortal.Core.Common;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.Data.Database.Entities;

[Index(nameof(Name))]
[Index(nameof(ExternalId))]
[Index(nameof(Description))]
public class Category : EntityBase
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(20)]
    public string ExternalId { get; set; } = string.Empty;

    public virtual ICollection<Resource> CoverImages { get; set; } = [];

    public virtual ICollection<Product> Products { get; set; } = [];
}
