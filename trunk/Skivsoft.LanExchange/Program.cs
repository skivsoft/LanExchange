using System;
using System.Windows.Forms;

namespace LanExchange
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
