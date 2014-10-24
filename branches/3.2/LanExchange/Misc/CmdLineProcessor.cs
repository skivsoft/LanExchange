using System;
using System.ComponentModel;

namespace LanExchange.Misc
{
    internal static class CmdLineProcessor
    {
        private static readonly string[] s_Args = Environment.GetCommandLineArgs();

        internal static string GetIfPresent(string name)
        {
            for (int i = 1; i < s_Args.Length; i++)
            {
                var arg = s_Args[i];
                if (arg.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                    return arg.Remove(0, name.Length);
            }
            return null;
        }

        [Localizable(false)]
        internal static void Processing()
        {
            if (GetIfPresent("/genenglish") != null)
            {
                GenerateEnglish.Generate();
                Environment.Exit(0);
            }
            //if (GetIfPresent("/new") == null)
            //    if (App.Resolve<ISingleInstanceService>().CheckExists("LanExchange"))
            //        Environment.Exit(1);
            var lang = GetIfPresent("/lang:");
            if (lang == null)
                lang = App.Config.Language;
            App.TR.CurrentLanguage = lang;
        }
    }
}