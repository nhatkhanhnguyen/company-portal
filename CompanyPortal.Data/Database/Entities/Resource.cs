using CompanyPortal.Core.Common;
using CompanyPortal.Core.Enum;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPortal.Data.Database.Entities;

public class Resource : EntityBase
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string BlobName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Url { get; set; } = string.Empty;

    public double Size { get; set; }

    public int? ProductId { get; set; }

    public int? ArticleId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; }

    [ForeignKey(nameof(ArticleId))]
    public Article? Article { get; set; }

    public ResourceType ResourceType { get; set; }
}
