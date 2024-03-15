using CompanyPortal.Core.Attributes;
using CompanyPortal.Core.Common;

using System.ComponentModel;
using System.Reflection;

namespace CompanyPortal.Core.Extensions;

public static class EnumExtensions
{
    public static List<EnumToRecord<T>> GetEnumToRecords<T>() where T : struct, Enum
    {
        return Enum.GetValues<T>().Select(e => new EnumToRecord<T>(GetIndex<T>(e), e.GetEnumDescription(), e, e.GetCustomValue(), e.GetEnumIcon())).ToList();
    }

    public static string? GetEnumDescription(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        if (fieldInfo == null)
        {
            return null;
        }
        var attribute = fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
        return attribute?.Description;
    }

    public static int? GetIndex<T>(T currentValue) where T : struct, Enum
    {
        var enumValues = Enum.GetValues<T>().ToList();
        return enumValues.IndexOf(currentValue);
    }

    public static double? GetCustomValue(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        if (fieldInfo == null)
        {
            return null;
        }
        var attribute = fieldInfo.GetCustomAttribute(typeof(CustomValueAttribute)) as CustomValueAttribute;
        return attribute?.Value;
    }

    public static string? GetEnumIcon(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        if (fieldInfo == null)
        {
            return null;
        }
        var attribute = fieldInfo.GetCustomAttribute(typeof(IconAttribute)) as IconAttribute;
        return attribute?.IconClass;
    }

    public static T GetNextEnumValueInOrder<T>(T currentValue) where T : struct, Enum
    {
        var enumValues = Enum.GetValues<T>().ToList();
        var currentValueIndex = enumValues.IndexOf(currentValue);
        return enumValues.ElementAt(currentValueIndex + 1 >= enumValues.Count ? enumValues.Count - 1 : currentValueIndex + 1);
    }

    public static T GetPreviousEnumValueInOrder<T>(T currentValue) where T : struct, Enum
    {
        var enumValues = Enum.GetValues<T>().ToList();
        var currentValueIndex = enumValues.IndexOf(currentValue);
        return enumValues.ElementAt(currentValueIndex - 1 >= 0 ? currentValueIndex - 1 : 0);
    }
}
