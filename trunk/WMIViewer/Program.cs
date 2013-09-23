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

            var wmiArgs = WMIArgs.ParseFromCmdLine(args);

            var presenter = new WMIPresenter(wmiArgs, null);

            if (presenter.ConnectToComputer())
            {
                Form mainForm;
                if (wmiArgs.EditPropertyMode)
                    mainForm = new WMIEditProperty(presenter);
                else
                {
                    WMIClassList.Instance.EnumLocalMachineClasses();
                    mainForm = new WMIForm(presenter);
                }
                Application.Run(mainForm);
            }
        }
    }
}