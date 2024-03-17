using CompanyPortal.Core.Common;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public int ImageId { get; set; }

    [ForeignKey(nameof(ImageId))]
    public Resource? Image { get; set; }

    public virtual ICollection<Product> Products { get; set; } = [];
}
