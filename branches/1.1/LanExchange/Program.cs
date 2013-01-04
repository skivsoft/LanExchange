// *****************************************************************************
// RELEASE 1.1 MAJOR TODO LIST
//   TODO * Autoupdate computer lists
//   TODO * Sending items to another tab
//   TODO * Updating menu lists after changing tab list (add, remove)
//   DONE * Update counts in status list
//   TODO * Filtering computer items
//   TODO * ContextMenuStrip for top panel
//   TODO * MSI installer
//   TODO * Publishing and autoupdate from v1.0
//
// RELEASE 1.1 MINOR TODO LIST
//   TODO  Manual creation of computer items
//   TODO  Enum shares after ItemActivate
//   TODO  Multi-langual support (Russian, Engligh)
//   TODO  Internal language editor
//   TODO  Recently used items must appears when Tray.onMouseOver event fired
//   TODO  WMI-commands execution with parameters
//   TODO  Refactoring for strict Model-View-Presenter pattern
//   
// 
// *****************************************************************************
namespace LanExchange
{
    using System;
    using System.Windows.Forms;
    using System.Reflection;
    using NLog;
    using LanExchange.UI;
    using LanExchange.Utils;

    internal static class Program
    {
        private readonly static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        static void LogHeader()
        {
            logger.Info("----------------------------------------");
            logger.Info("Executable: [{0}], Version: {1}", Application.ExecutablePath, Assembly.GetExecutingAssembly().GetName().Version.ToString());
            logger.Info(@"MachineName: {0}, UserName: {1}\{2}, Interactive: {3}", Environment.MachineName, Environment.UserDomainName, Environment.UserName, Environment.UserInteractive);
            logger.Info("OSVersion: [{0}], Processors count: {1}", Environment.OSVersion, Environment.ProcessorCount);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            logger.Error("Unhandled: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace);
        }

        [STAThread]
        static void Main()
        {
            SingleInstanceCheck.Check();
            LogHeader();
            Application.ApplicationExit += MainForm.OnApplicationExit;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); // must be called before first form created
            Application.Run(new MainForm());
            // workaround for NLog's bug under Mono (hanging after app exit) 
            LogManager.Configuration = null;
        }
    }
}
