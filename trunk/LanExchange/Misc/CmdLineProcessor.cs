using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace LanExchange.Misc
{
    static class CmdLineProcessor
    {
        private static string[] s_Args;

        private static string GetIfPresent(string name)
        {
            for (int i = 1; i < s_Args.Length; i++)
            {
                var arg = s_Args[i].ToUpper(CultureInfo.InvariantCulture);
                if (arg.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                    return arg.Remove(0, name.Length);
            }
            return null;
        }

        [Localizable(false)]
        internal static void Processing()
        {
            s_Args = Environment.GetCommandLineArgs();
            var lang = GetIfPresent("/LANG:");
            if (lang == null)
                lang = Model.Settings.Settings.Instance.GetStringValue("Language");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
        }
    }
}
