﻿using System;
using System.Globalization;
using System.Threading;
using LanExchange.Model.Settings;

namespace LanExchange
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
                lang = Settings.Instance.Language;
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            }
            catch
            {
            }
        }
    }
}
