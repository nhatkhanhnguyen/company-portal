namespace CompanyPortal.Services;

public class PreloadService
{
    public bool IsLoading { get; set; }

    public void StartLoading()
    {
        IsLoading = true;
    }

    public void StopLoading()
    {
        IsLoading = false;
    }
}
