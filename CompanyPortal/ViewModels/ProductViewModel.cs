using CompanyPortal.Core.Common;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPortal.ViewModels;

public class ProductViewModel : ViewModelBase
{
    [DisplayName("Tên sản phẩm")]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Mô tả ngắn")]
    public string Description { get; set; } = string.Empty;

    [DisplayName("Giá")]
    [Column(TypeName = "decimal(11, 0)")]
    public decimal Price { get; set; }

    [DisplayName("Tags, cách nhau bằng dấu phẩy")]
    public string Tags { get; set; } = string.Empty;

    [DisplayName("Ảnh sản phẩm")]
    public List<ResourceViewModel> Images { get; set; } = [];

    [DisplayName("Ẩn sản phẩm, không hiện trên trang sản phẩm của người dùng")]
    public bool MarkedAsInactive { get; set; } = false;
}
