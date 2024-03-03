using CompanyPortal.Core.Common;

namespace CompanyPortal.ViewModels;

public class ArticleViewModel : ViewModelBase
{
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string Tags { get; set; } = string.Empty;

    public bool MarkedAsInactive { get; set; }

    public List<ResourceViewModel> Images { get; set; } = [];
}
