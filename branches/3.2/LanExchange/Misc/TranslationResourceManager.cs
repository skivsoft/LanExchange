using System.Globalization;
using System.Reflection;
using System.Resources;

namespace LanExchange.Misc
{
    public class TranslationResourceManager : ResourceManager
    {
        public TranslationResourceManager(string baseName, Assembly assembly) : base(baseName, assembly)
        {
        }

        public override string GetString(string name, CultureInfo culture)
        {
            var result = base.GetString(name, culture);
            return !string.IsNullOrEmpty(result) ? App.TR.Translate(result) : result;
        }
    }
}