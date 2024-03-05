using CompanyPortal.Core.Common;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPortal.Data.Database.Entities;

[Index(nameof(Name), IsUnique = true)]
[Index(nameof(ExternalId), IsUnique = true)]
public class Product : EntityBase
{
    [MaxLength(20)]
    public string ExternalId { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(11, 0)")]
    public decimal Price { get; set; }

    [MaxLength(700)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Tags { get; set; } = string.Empty;

    public int? CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))] 
    public Category? Category { get; set; }

    public virtual ICollection<Resource> Images { get; set; } = [];

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = [];
}
