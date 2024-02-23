using System.ComponentModel.DataAnnotations.Schema;
using CompanyPortal.Core.Common;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.Data.Database.Entities;

[Index(nameof(Name))]
public class Product : EntityBase
{
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(11, 0)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(11, 0)")]
    public decimal? PercentDiscount { get; set; }

    [Column(TypeName = "decimal(11, 0)")]
    public decimal? DiscountedPrice { get; set; }

    public virtual ICollection<Resource> Images { get; set; } = [];

    public string Tags { get; set; } = string.Empty;
}
