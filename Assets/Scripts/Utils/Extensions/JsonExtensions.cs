using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace FabricWars.Utils.Extensions
{
    public static class JsonExtensions
    {
        public static T Get <T>(this JObject obj, string key) where T : IConvertible
        {
            if (obj.TryGetValue(key, out var value))
                return ((JValue)value).ToObject<T>();

            throw new KeyNotFoundException($"key {key} is not found in JObject({obj})");
        }
    }
}