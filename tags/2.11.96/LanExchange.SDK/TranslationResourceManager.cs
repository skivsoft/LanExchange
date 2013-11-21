using System.Reflection;
using System.Resources;

namespace LanExchange.SDK
{
    public class TranslationResourceManager : ResourceManager
    {
        public static ITranslationService Service;
        
        public TranslationResourceManager(string baseName, Assembly assembly) : base(baseName, assembly)
        {
        }

        public override string  GetString(string name, System.Globalization.CultureInfo culture)
        {
            return Service != null ? Service.Translate(base.GetString(name, culture)) : name;
        }
    }
}
