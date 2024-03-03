using System.ComponentModel.DataAnnotations;
using CompanyPortal.Core.Common;

using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPortal.Data.Database.Entities;

public class ProductVariant : EntityBase
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(11, 0)")]
    public decimal Price { get; set; }

    public int ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = default!;
}
