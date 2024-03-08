namespace CompanyPortal.ViewModels;

public class ArticleCardViewModel
{
    public string Title { get; set; } = string.Empty;

    public string ShortDescription { get; set; } = string.Empty;

    public ResourceViewModel Image { get; set; } = default!;

    public DateTimeOffset DateCreated { get; set; }
}
