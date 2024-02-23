using CompanyPortal.Core.Common;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.Data.Database.Entities;

[Index(nameof(Title))]
public class Article : EntityBase
{
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public int CoverImageResourceId { get; set; }

    public uint Views { get; set; }

    public string Tags { get; set; } = string.Empty;
}
