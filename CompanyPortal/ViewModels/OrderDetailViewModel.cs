using CompanyPortal.Core.Common;

using System.ComponentModel;

namespace CompanyPortal.ViewModels;

public class OrderDetailViewModel : ViewModelBase
{
    [DisplayName("Mã sản phẩm")]
    public int ProductId { get; set; }

    public int OrderId { get; set; }

    [DisplayName("Số lượng")]
    public int Quantity { get; set; }

    [DisplayName("Tên sản phẩm")]
    public string ProductName { get; set; } = string.Empty;
}
