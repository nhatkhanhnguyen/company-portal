using CompanyPortal.Core.Common;

using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPortal.ViewModels;

public class ProductViewModel : ViewModelBase
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Column(TypeName = "decimal(11, 0)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(11, 0)")]
    public decimal? PercentDiscount { get; set; }

    [Column(TypeName = "decimal(11, 0)")]
    public decimal? DiscountedPrice { get; set; }

    public string Tags { get; set; } = string.Empty;

    public IEnumerable<ResourceViewModel> Images { get; set; } = [];

    public bool MarkedAsInactive { get; set; } = false;
}
