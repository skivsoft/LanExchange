using System;
using System.Globalization;
using System.Threading;

namespace LanExchange.Model
{
    class CmdLineProcessor
    {
        private static string[] s_Args;

        private static string GetIfPresent(string name)
        {
            for (int i = 1; i < s_Args.Length; i++)
            {
                var arg = s_Args[i].ToUpper();
                if (arg.StartsWith(name))
                    return arg.Remove(0, name.Length);
            }
            return null;
        }

        internal static void Processing()
        {
            s_Args = Environment.GetCommandLineArgs();
            var lang = GetIfPresent("/LANG:");
            if (lang == null)
                lang = Settings.Settings.Instance.Language;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
        }
    }
}
