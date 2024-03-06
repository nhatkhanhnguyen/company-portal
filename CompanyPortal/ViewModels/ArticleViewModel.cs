using CompanyPortal.Core.Common;
using CompanyPortal.Core.Enums;

using System.ComponentModel;

namespace CompanyPortal.ViewModels;

public class ArticleViewModel : ViewModelBase
{
    [DisplayName("Tiêu đề")]
    public string Title { get; set; } = string.Empty;

    [DisplayName("Nội dung")]
    public string Content { get; set; } = string.Empty;

    [DisplayName("Tags")]
    public string Tags { get; set; } = string.Empty;

    [DisplayName("Ẩn bài viết, không hiện trên trang xem bài viết của người dùng")]
    public bool MarkedAsInactive { get; set; }

    [DisplayName("Loại bài viết")]
    public ArticleType Type { get; set; }

    [DisplayName("Ảnh bìa")]
    public ResourceViewModel CoverImage { get; set; } = default!;

    [DisplayName("Mô tả ngắn")]
    public string Description { get; set; } = string.Empty;

    public uint Views { get; set; } = default!;
}
