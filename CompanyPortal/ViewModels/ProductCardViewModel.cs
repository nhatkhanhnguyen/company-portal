namespace CompanyPortal.ViewModels;

public class ProductCardViewModel
{
    public string Name { get; set; } = string.Empty;

    public string ShortDescription { get; set; } = string.Empty;

    public string CategoryName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public ResourceViewModel Image { get; set; } = default!;

    public int Rating { get; set; }
}
