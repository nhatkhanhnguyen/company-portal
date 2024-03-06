using CompanyPortal.Core.Common;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPortal.Data.Database.Entities;

[Index(nameof(Title))]
public class Article : EntityBase
{
    [MaxLength(128)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(300)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Content { get; set; } = string.Empty;

    public int? CoverImageId { get; set; }

    [ForeignKey(nameof(CoverImageId))]
    public Resource? CoverImage { get; set; }

    public uint Views { get; set; }

    [MaxLength(100)]
    public string Tags { get; set; } = string.Empty;
}
