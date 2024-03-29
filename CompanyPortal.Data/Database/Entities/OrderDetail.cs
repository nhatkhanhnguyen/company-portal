﻿using CompanyPortal.Core.Common;

using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPortal.Data.Database.Entities;

public class OrderDetail : EntityBase
{
    public int ProductId { get; set; }

    [ForeignKey(nameof(ProductId))] 
    public Product Product { get; set; } = default!;

    public decimal Price { get; set; } // Price at the time ordered

    public int OrderId { get; set; }

    public int Quantity { get; set; }

    public decimal Total => Price * Quantity;  // Total at the time ordered
}
