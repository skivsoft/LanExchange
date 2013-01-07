using System;
using System.Windows.Forms;
using SkivSoft.LanExchange.Core;

namespace SkivSoft.LanExchange
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // init core
            TMainApp.App = new TMainAppUI();
            // init app and main form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
