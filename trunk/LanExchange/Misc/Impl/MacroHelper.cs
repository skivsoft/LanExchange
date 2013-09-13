using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace LanExchange.Misc.Impl
{
    public static class MacroHelper
    {
        [Localizable(false)]
        public static IDictionary<string, string> GetPublicReadProperties(object obj)
        {
            var props = new Dictionary<string, string>();
            foreach (var prop in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly))
                if (prop.CanRead)
                {
                    var propValue = prop.GetValue(obj, null).ToString();
                    props.Add(string.Format("$({0})", prop.Name), propValue);
                }
            return props;
        }

        public static string ExpandPublicProperties(string value, object obj)
        {
            foreach (var pair in GetPublicReadProperties(obj))
                value = value.Replace(pair.Key, pair.Value);
            return value;
        }
    }
}