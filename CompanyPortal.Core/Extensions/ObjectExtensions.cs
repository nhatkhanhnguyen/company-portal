using Newtonsoft.Json;

using System.ComponentModel;
using System.Reflection;

namespace CompanyPortal.Core.Extensions;

public static class ObjectExtensions
{
    public static T? Clone<T>(this T obj)
    {
        return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
    }

    public static string GetDescription(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        if (fieldInfo == null)
        {
            return string.Empty;
        }
        var attribute = fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
        return attribute?.Description ?? string.Empty;
    }
}