using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WMIViewer
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WMIForm(GetComputer(args)));
        }

        static IWmiComputer GetComputer(IEnumerable<string> args)
        {
            const string COMPUTER_MARKER = "/COMPUTER:";
            var compName = SystemInformation.ComputerName;
            foreach(var word in args)
                if (word.ToUpper().StartsWith(COMPUTER_MARKER))
                {
                    var comp = word.Remove(0, COMPUTER_MARKER.Length);
                    if (!string.IsNullOrEmpty(comp))
                        compName = comp;
                    break;
                }
            return new WMIComputer(compName);
        }
    }
}