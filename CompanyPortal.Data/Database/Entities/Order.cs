using System.ComponentModel.DataAnnotations.Schema;
using CompanyPortal.Core.Common;

namespace CompanyPortal.Data.Database.Entities;

public class Order : EntityBase
{
    public string Fullname { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = [];

    [Column(TypeName = "decimal(11, 0)")]
    public decimal Total { get; set; }
}
