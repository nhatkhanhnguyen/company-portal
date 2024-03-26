using CompanyPortal.Core.Common;
using CompanyPortal.Core.Enums;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPortal.Data.Database.Entities;

[Index(nameof(Name))]
[Index(nameof(BlobName))]
[Index(nameof(ProductId))]
[Index(nameof(ArticleId))]
[Index(nameof(CategoryId))]
public class Resource : EntityBase
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string BlobName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Url { get; set; } = string.Empty;

    public double Size { get; set; }

    public int? ProductId { get; set; }

    public int? ArticleId { get; set; }

    public int? CategoryId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; }
    
    [ForeignKey(nameof(CategoryId))]
    public Category? Category { get; set; }

    [ForeignKey(nameof(ArticleId))]
    public Article? Article { get; set; }

    public ResourceType ResourceType { get; set; }
}
