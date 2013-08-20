using System;
using System.Windows.Forms;

namespace WMIViewer
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WMIForm(new WMIComputer(SystemInformation.ComputerName)));
        }
    }
}