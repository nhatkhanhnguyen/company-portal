using System.ComponentModel.DataAnnotations.Schema;
using CompanyPortal.Core.Common;
using CompanyPortal.Core.Enum;

namespace CompanyPortal.Data.Database.Entities;

public class Resource : EntityBase
{
    public string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public int? ProductId { get; set; }

    public int? ArticleId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; }

    [ForeignKey(nameof(ArticleId))]
    public Article? Article { get; set; }

    public ResourceType ResourceType { get; set; }
}
