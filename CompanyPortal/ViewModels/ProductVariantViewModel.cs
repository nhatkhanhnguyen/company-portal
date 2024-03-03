using System.ComponentModel;
using CompanyPortal.Core.Common;

namespace CompanyPortal.ViewModels;

public class ProductVariantViewModel : ViewModelBase
{
    [DisplayName("Tên tùy chọn")]
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }
}
