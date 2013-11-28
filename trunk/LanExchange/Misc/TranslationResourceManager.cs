using System.Globalization;
using System.Reflection;
using System.Resources;
using LanExchange.Intf;

namespace LanExchange.Misc
{
    public class TranslationResourceManager : ResourceManager
    {
        public TranslationResourceManager(string baseName, Assembly assembly) : base(baseName, assembly)
        {
        }

        public override string  GetString(string name, CultureInfo culture)
        {
            return App.TR != null ? App.TR.Translate(base.GetString(name, culture)) : name;
        }
    }
}
