using CompanyPortal.Core.Attributes;

using System.ComponentModel;

namespace CompanyPortal.Core.Enums;

public enum OrderStatus
{
    [Description("Đã đặt hàng")]
    [CustomValue(100 / 6)]
    [Icon("fa-solid fa-check-to-slot")]
    Ordered = 0,

    [Description("Đã xác nhận")]
    [CustomValue(200 / 6)]
    [Icon("fa-solid fa-thumbs-up")]
    Confirmed = 1,

    [Description("Đang xử lý")]
    [CustomValue(300 / 6)]
    [Icon("fa-solid fa-person-running")]
    Processing = 2,

    [Description("Đang giao")]
    [CustomValue(400 / 6)]
    [Icon("fa-solid fa-truck-fast")]
    Delivering = 3,

    [Description("Đã giao")]
    [CustomValue(500 / 6)]
    [Icon("fa-solid fa-truck-ramp-box")]
    Delivered = 4,

    [Description("Hoàn thành")]
    [CustomValue(600 / 6)]
    [Icon("fa-solid fa-check-double")]
    Completed = 5
}
