using LanExchange.Interfaces.Processes;
using LanExchange.SDK;
using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace LanExchange.Implementations.Processes
{
    internal sealed class CmdLineProcessor : ICmdLineProcessor
    {
        private readonly string[] args = Environment.GetCommandLineArgs();
        private readonly IGenerateEnglishProcess generateEnglish;
        private readonly ITranslationService translationService;

        public CmdLineProcessor(
            IGenerateEnglishProcess generateEnglish,
            ITranslationService translationService)
        {
            Contract.Requires<ArgumentNullException>(generateEnglish != null);
            Contract.Requires<ArgumentNullException>(translationService != null);

            this.generateEnglish = generateEnglish;
            this.translationService = translationService;
        }

        internal string GetIfPresent(string name)
        {
            for (int i = 1; i < args.Length; i++)
            {
                var arg = args[i];
                if (arg.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                    return arg.Remove(0, name.Length);
            }
            return null;
        }

        [Localizable(false)]
        public void Processing()
        {
            if (GetIfPresent("/genenglish") != null)
            {
                generateEnglish.Generate();
                Environment.Exit(0);
            }
            var lang = GetIfPresent("/lang:");
            if (lang == null)
                lang = App.Config.Language;

            translationService.CurrentLanguage = lang;
        }
    }
}
