using CompanyPortal.Core.Common;

using System.ComponentModel;

namespace CompanyPortal.ViewModels;

public class CategoryViewModel : ViewModelBase
{
    [DisplayName("Tên danh mục")]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Ảnh mô tả")]
    public ResourceViewModel Image { get; set; } = new();

    [DisplayName("Mô tả ngắn")]
    public string Description { get; set; } = string.Empty;

    [DisplayName("Mã danh mục")]
    public string ExternalId { get; set; } = string.Empty;

    [DisplayName("Ẩn danh mục, không hiện trên trang sản phẩm của người dùng")]
    public bool MarkedAsInactive { get; set; } = false;

    [DisplayName("Danh sách sản phẩm thuộc danh mục")]
    public List<ProductViewModel> Products { get; set; } = [];
}
