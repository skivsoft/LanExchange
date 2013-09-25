using System;
using System.Threading;
using System.Windows.Forms;

namespace WMIViewer
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            WMIArgs wmiArgs = null;
            try
            {
                wmiArgs = WMIArgs.ParseFromCmdLine(args);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "WMIViewer.exe", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            var presenter = new WMIPresenter(wmiArgs);
            if (wmiArgs != null && presenter.ConnectToComputer())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                switch (wmiArgs.StartCmd)
                {
                    case WMIStartCommand.EditProperty:
                        var propForm = new WMIEditProperty(presenter);
                        propForm.ShowDialog();
                        break;
                    case WMIStartCommand.ExecuteMethod:
                        var methodForm = new WMIMethodForm(presenter);
                        methodForm.PrepareForm();
                        methodForm.ShowDialog();
                        break;
                    default:
                        WMIClassList.Instance.EnumLocalMachineClasses();
                        Application.Run(new WMIForm(presenter));
                        break;
                }
            }
        }
    }
}