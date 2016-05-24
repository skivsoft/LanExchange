using LanExchange.SDK;
using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace LanExchange.Misc
{
    public class TranslationResourceManager : ResourceManager
    {
        private readonly ITranslationService translationService;

        public TranslationResourceManager(
            ITranslationService translationService,            
            string baseName, Assembly assembly) : base(baseName, assembly)
        {
            Contract.Requires<ArgumentNullException>(translationService != null);

            this.translationService = translationService;
        }

        public override string GetString(string name, CultureInfo culture)
        {
            var result = base.GetString(name, culture);
            if (string.IsNullOrEmpty(result))
                return string.Empty;
            return translationService.Translate(result);
        }
    }
}