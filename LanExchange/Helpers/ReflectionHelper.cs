using System;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace LanExchange.Helpers
{
    /// <summary>
    /// Several useful reflection utils.
    /// </summary>
    public static class ReflectionUtils
    {
        /// <summary>
        /// Copies the object properties.
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="sourceObject">The source object.</param>
        /// <param name="destObject">The dest object.</param>
        public static void CopyObjectProperties<TClass>(TClass sourceObject, TClass destObject) where TClass : class
        {
            var props = typeof(TClass).GetProperties();
            foreach (var prop in props)
            {
                var obj = prop.GetValue(sourceObject, null);
                prop.SetValue(destObject, obj, null);
            }
        }
    }
}