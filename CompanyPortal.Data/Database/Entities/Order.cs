using CompanyPortal.Core.Common;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPortal.Data.Database.Entities;

public class Order : EntityBase
{
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
    public decimal Total { get; set; }
}
