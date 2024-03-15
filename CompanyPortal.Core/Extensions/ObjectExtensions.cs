using Newtonsoft.Json;

namespace CompanyPortal.Core.Extensions;

public static class ObjectExtensions
{
    public static T? Clone<T>(this T obj)
    {
        return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
    }
}