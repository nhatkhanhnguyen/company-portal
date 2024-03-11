using CompanyPortal.Core.Common;
using CompanyPortal.Core.Enums;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPortal.Data.Database.Entities;

[Index(nameof(ExternalId))]
[Index(nameof(Fullname))]
[Index(nameof(Email))]
[Index(nameof(PhoneNumber))]
public class Order : EntityBase
{
    [MaxLength(20)]
    public string ExternalId { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Fullname { get; set; } = string.Empty;

    [MaxLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = [];

    [Column(TypeName = "decimal(11, 0)")]
    public decimal Total =>  OrderDetails.Sum(x => x.Quantity * x.Price);

    public OrderStatus Status { get; set; } = OrderStatus.Ordered;
}
