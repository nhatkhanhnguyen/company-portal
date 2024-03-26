using CompanyPortal.Core.Common;
using CompanyPortal.Core.Enums;

namespace CompanyPortal.ViewModels;

public class ResourceViewModel : ViewModelBase
{
    public string Name { get; set; } = string.Empty;

    public string BlobName { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public string Base64Content { get; set; } = string.Empty;

    public double Size { get; set; }

    public int? ProductId { get; set; }

    public int? ArticleId { get; set; }

    public int? CategoryId { get; set; }

    public ResourceType ResourceType { get; set; }

    public UploadFileStatus Status { get; set; } = UploadFileStatus.Old;
}

public enum UploadFileStatus
{
    Old,
    New,
    Removed
}