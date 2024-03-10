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

    public decimal Price { get; set; }

    public decimal Total => Quantity * Price;

    public string ProductImageUrl { get; set; } = string.Empty;

    public OrderDetailStatus Status { get; set; }

    public OrderDetailViewModel(int id, string productName, string productImageUrl, int quantity, decimal price)
    {
        Id = id;
        ProductName = productName;
        ProductImageUrl = productImageUrl;
        Quantity = quantity;
        Price = price;
    }
}

public enum OrderDetailStatus
{
    Old,
    New,
    Modified,
    Removed
}