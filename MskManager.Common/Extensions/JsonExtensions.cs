using Newtonsoft.Json;
using System;

namespace MskManager.Common.Extensions
{
    public static class JsonExtensions
    {
        public static T Deserialize<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static string Serialize(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
