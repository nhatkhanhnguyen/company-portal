using CompanyPortal.Core.Common;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPortal.Data.Database.Entities;

[Index(nameof(Name))]
public class Product : EntityBase
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(11, 0)")]
    public decimal Price { get; set; }

    public virtual ICollection<Resource> Images { get; set; } = [];

    [MaxLength(100)]
    public string Tags { get; set; } = string.Empty;
}
