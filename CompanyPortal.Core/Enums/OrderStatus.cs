using System.ComponentModel;

namespace CompanyPortal.Core.Enums;

public enum OrderStatus
{
    [Description("Đã đặt hàng")]
    Ordered = 100/6,

    [Description("Đã xác nhận")]
    Confirmed = 200/6,

    [Description("Đang xử lý")]
    Processing = 300/6,

    [Description("Đang giao")]
    Delivering = 400/6,

    [Description("Đã giao")]
    Delivered = 500/6,
    
    [Description("Hoàn thành")]    
    Completed = 600/6
}
