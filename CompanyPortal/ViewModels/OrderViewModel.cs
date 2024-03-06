using CompanyPortal.Core.Common;

using System.ComponentModel;

namespace CompanyPortal.ViewModels;

public class OrderViewModel : ViewModelBase
{
    [DisplayName("Họ và tên")]
    public string Fullname { get; set; } = string.Empty;

    [DisplayName("Số điện thoại")]
    public string PhoneNumber { get; set; } = string.Empty;

    [DisplayName("Địa chỉ")]
    public string Address { get; set; } = string.Empty;

    [DisplayName("Email")]
    public string Email { get; set; } = string.Empty;

    [DisplayName("Tổng tiền")]
    public decimal Total { get; set; }

    [DisplayName("Chi tiết")]
    public List<OrderDetailViewModel> OrderDetails { get; set; } = [];
}
