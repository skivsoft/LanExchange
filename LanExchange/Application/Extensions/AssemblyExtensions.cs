using System;
using System.Reflection;

namespace LanExchange.Application.Extensions
{
    internal static class AssemblyExtensions
    {
        public static T GetCustomAttribute<T>(this Assembly assembly)
        {
            if (assembly != null) throw new ArgumentNullException(nameof(assembly));

            var attributes = assembly.GetCustomAttributes(typeof(T), false);
            if (attributes.Length == 0)
                return default(T);

            return (T)attributes[0];
        }
    }
}