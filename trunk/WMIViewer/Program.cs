using System;
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
                switch (wmiArgs.StartCmd)
                {
                    case WMIStartCommand.EditProperty:
                        mainForm = new WMIEditProperty(presenter);
                        mainForm.ShowDialog();
                        break;
                    case WMIStartCommand.ExecuteMethod:
                        mainForm = new WMIMethodForm(presenter);
                        Application.Run(mainForm);
                        break;
                    default:
                        WMIClassList.Instance.EnumLocalMachineClasses();
                        mainForm = new WMIForm(presenter);
                        Application.Run(mainForm);
                        break;
                }
            }
        }
    }
}