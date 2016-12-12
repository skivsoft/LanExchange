using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Implementation
{
    internal sealed class TranslationResourceManager : ResourceManager
    {
        private readonly ITranslationService translationService;

        public TranslationResourceManager(
            ITranslationService translationService,            
            string baseName, Assembly assembly) : base(baseName, assembly)
        {
            if (translationService != null) throw new ArgumentNullException(nameof(translationService));

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