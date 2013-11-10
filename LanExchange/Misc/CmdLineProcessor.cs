using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using LanExchange.Core;
using LanExchange.Intf;

namespace LanExchange.Misc
{
    static class CmdLineProcessor
    {
        private static string[] s_Args;

        private static string GetIfPresent(string name)
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
            s_Args = Environment.GetCommandLineArgs();
            if (GetIfPresent("/genenglish") != null)
            {
                GenerateEnglish.Generate();
                Environment.Exit(0);
            }
            var lang = GetIfPresent("/LANG:");
            if (lang == null)
                lang = Model.Settings.Settings.Instance.GetStringValue("Language");
            App.TR.CurrentLanguage = lang;
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
        }
    }
}
