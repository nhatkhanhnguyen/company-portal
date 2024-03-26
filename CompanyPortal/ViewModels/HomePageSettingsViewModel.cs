namespace CompanyPortal.ViewModels;

public class HomePageSettingsViewModel
{
    public List<SettingViewModel> CarouselSettings { get; set; } = [];

    public List<SettingViewModel> StarredProductSettings { get; set; } = [];

    public List<SettingViewModel> WhyUsSettings { get; set; } = [];
}
