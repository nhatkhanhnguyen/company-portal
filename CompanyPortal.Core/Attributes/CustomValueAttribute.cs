namespace CompanyPortal.Core.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class CustomValueAttribute(double value) : Attribute
{
    public double Value { get; set; } = value;
}
