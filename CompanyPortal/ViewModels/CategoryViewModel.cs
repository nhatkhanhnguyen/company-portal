using CompanyPortal.Core.Common;

using System.ComponentModel;

namespace CompanyPortal.ViewModels;

public class CategoryViewModel : ViewModelBase
{
    [DisplayName("Tên danh mục")]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Mô tả ngắn")]
    public string Description { get; set; } = string.Empty;

    [DisplayName("Ẩn danh mục, không hiện trên trang sản phẩm của người dùng")]
    public bool MarkedAsInactive { get; set; } = false;
}
