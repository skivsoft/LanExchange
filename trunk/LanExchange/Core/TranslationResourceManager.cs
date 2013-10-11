using System.Reflection;
using System.Resources;
using LanExchange.Intf;

namespace LanExchange.Core
{
    public class TranslationResourceManager : ResourceManager
    {
        public TranslationResourceManager(string baseName, Assembly assembly) : base(baseName, assembly)
        {
        }

        public override string  GetString(string name, System.Globalization.CultureInfo culture)
        {
            return App.TR.Translate(base.GetString(name, culture));
        }
    }
}
