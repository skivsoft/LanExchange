using LanExchange.Interfaces.Processes;
using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace LanExchange.Implementations.Processes
{
    internal sealed class CmdLineProcessor : ICmdLineProcessor
    {
        private readonly string[] args = Environment.GetCommandLineArgs();
        private readonly IGenerateEnglishProcess generateEnglish;

        public CmdLineProcessor(IGenerateEnglishProcess generateEnglish)
        {
            Contract.Requires<ArgumentNullException>(generateEnglish != null);

            this.generateEnglish = generateEnglish;
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
            App.TR.CurrentLanguage = lang;
        }
    }
}
