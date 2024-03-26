using CompanyPortal.Core.Common;
using CompanyPortal.Core.Enums;

namespace CompanyPortal.ViewModels;

public class SettingViewModel : ViewModelBase
{
    public string Name { get; set; } = string.Empty; 

    public SettingType Type { get; set; }

    public string Value { get; set; } = string.Empty;

    public uint Index { get; set; }
}
