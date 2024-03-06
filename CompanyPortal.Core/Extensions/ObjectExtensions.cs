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

    public static string? GetDescription(this Enum value)
    {
        FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
        if (fieldInfo == null)
        {
            return null;
        }
        var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
        return attribute.Description;
    }
}