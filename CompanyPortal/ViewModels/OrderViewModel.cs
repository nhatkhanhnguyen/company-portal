using CompanyPortal.Core.Common;
using CompanyPortal.Core.Enums;

using System.ComponentModel;

namespace CompanyPortal.ViewModels;

public class OrderViewModel : ViewModelBase
{
    [DisplayName("Họ và tên")]
    public string Fullname { get; set; } = string.Empty;

    [DisplayName("Mã đơn hàng")]
    public string ExternalId { get; set; } = string.Empty;

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

    [DisplayName("Đánh dấu đơn hàng đã hủy")]
    public bool MarkedAsInactive { get; set; } = false;

    public OrderStatus Status { get; set; } = OrderStatus.Ordered;

    public string Progress => $"{(float)Status}%";
}
