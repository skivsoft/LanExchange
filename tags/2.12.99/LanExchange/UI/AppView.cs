﻿using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using LanExchange.Intf;
using System.Diagnostics;
using LanExchange.SDK;

namespace LanExchange.UI
{
    public class AppView : IAppView
    {
        public void ApplicationRun()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Application.ThreadException += ApplicationOnThreadException;
            Application.ThreadExit += ApplicationOnThreadExit;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); // must be called before first form created
            var form = (Form) App.Resolve<IMainView>();
            Application.Run(form);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            App.MainView.ApplicationExit();
        }

        [Localizable(false)]
        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            #if DEBUG
            Debug.Fail(e.Exception.Message + Environment.NewLine + e.Exception.StackTrace);
            #else
            App.MainView.ApplicationExit();
            #endif
        }

        private static void ApplicationOnThreadExit(object sender, EventArgs e)
        {
            App.MainPages.SaveInstant();
            App.Config.Save();
            // dispose instances registered in plugins
            App.Resolve<IDisposableManager>().Dispose();
        }
    }
}