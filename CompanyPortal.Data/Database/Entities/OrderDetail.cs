using CompanyPortal.Core.Common;

namespace CompanyPortal.Data.Database.Entities;
public class OrderDetail : EntityBase
{
    public int ProductId { get; set; }

    public int OrderId { get; set; }

    public int Quantity { get; set; }
}
