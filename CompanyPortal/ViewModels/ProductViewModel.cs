using CompanyPortal.Core.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPortal.ViewModels;

public class ProductViewModel : ViewModelBase
{
    [DisplayName("Mã sản phẩm")]
    public string ExternalId { get; set; } = string.Empty;

    [DisplayName("Tên sản phẩm")]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Mô tả ngắn")]
    public string ShortDescription { get; set; } = string.Empty;

    [DisplayName("Mô tả đầy đủ")]
    public string FullDescription { get; set; } = string.Empty;

    [DisplayName("Giá")]
    [Column(TypeName = "decimal(11, 0)")]
    public decimal Price { get; set; }    
    
    public double Rate { get; set; }

    [DisplayName("Tags, cách nhau bằng khoảng trắng")]
    public string Tags { get; set; } = string.Empty;

    [DisplayName("Ảnh sản phẩm")]
    public List<ResourceViewModel> Images { get; set; } = [];

    [DisplayName("Ẩn sản phẩm, không hiện trên trang sản phẩm của người dùng")]
    public bool MarkedAsInactive { get; set; } = false;

    [DisplayName("Tự động tạo")]
    public bool AutoGenerateExternalId { get; set; } = false;

    [DisplayName("Danh mục")]
    public int CategoryId { get; set; }

    public int ReviewCount { get; set; }

    [DisplayName("Các tùy chọn")]
    public List<ProductVariantViewModel> Variants { get; set; } = [];

    // Read-only
    public string CategoryName { get; internal set; } = string.Empty;
}
