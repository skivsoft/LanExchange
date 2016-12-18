using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace LanExchange.Presentation.WinForms.Helpers
{
    public static class MacroHelper
    {
        private const string MACRO_VAR_FMT = "$({0})";

        [Localizable(false)]
        public static IDictionary<string, string> GetPublicReadProperties(object obj)
        {
            var props = new Dictionary<string, string>();
            foreach (var prop in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                if (prop.CanRead)
                {
                    var indexParams = prop.GetIndexParameters();
                    if (indexParams.Length > 0) continue;
                    var propValue = prop.GetValue(obj, null);
                    props.Add(string.Format(CultureInfo.InvariantCulture, MACRO_VAR_FMT, prop.Name), propValue == null ? string.Empty : propValue.ToString());
                }

            return props;
        }

        public static string ExpandPublicProperties(string value, object obj)
        {
            return GetPublicReadProperties(obj).Aggregate(value, (current, pair) => current.Replace(pair.Key, pair.Value));
        }
    }
}