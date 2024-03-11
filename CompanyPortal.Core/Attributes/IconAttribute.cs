namespace CompanyPortal.Core.Attributes;

public class IconAttribute : Attribute
{
    public string IconClass { get; set; } = string.Empty;

    public IconAttribute(string iconClass)
    {
        IconClass = iconClass;
    }
}
