using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Text;

namespace SkivSoft.LanExchange
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // load plugins
            MainForm.MainApp = new TMainApp();
            MainForm.MainApp.LoadPlugins();
            // print version info to log
            MainForm.MainApp.LogPrint("OSVersion: [{0}], Processors count: {1}", Environment.OSVersion, Environment.ProcessorCount);
            MainForm.MainApp.LogPrint(@"MachineName: {0}, UserName: {1}\{2}, Interactive: {3}", Environment.MachineName, Environment.UserDomainName, Environment.UserName, Environment.UserInteractive);
            MainForm.MainApp.LogPrint("Executable: [{0}], Version: {1}", Application.ExecutablePath, Assembly.GetExecutingAssembly().GetName().Version.ToString());

            // init app and main form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
