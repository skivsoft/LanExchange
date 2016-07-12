using System;
using System.Linq;

namespace LanExchange.Application.Extensions
{
    internal static class TypeExtension
    {
        public static bool HasAttribute<T>(this Type type) where T : Attribute
        {
            if (type == null) return false;

            var attribute = type
                .GetCustomAttributes(typeof(T), true)
                .FirstOrDefault();

            return attribute != null;
        }
    }
}