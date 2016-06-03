using System;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace LanExchange.Application.Extensions
{
    internal static class AssemblyExtensions
    {
        public static T GetCustomAttribute<T>(this Assembly assembly)
        {
            Contract.Requires<ArgumentNullException>(assembly != null);

            var attributes = assembly.GetCustomAttributes(typeof(T), false);
            if (attributes.Length == 0)
                return default(T);

            return (T)attributes[0];
        }
    }
}