using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WMIViewer.Extensions;
using WMIViewer.Model;
using WMIViewer.Presenter;
using WMIViewer.UI;

namespace WMIViewer
{
    internal static class Program
    {
        [STAThread]
        [Localizable(true)]
        private static void Main(string[] args)
        {
            CmdLineArgs wmiArgs = null;
            try
            {
                wmiArgs = CmdLineArgs.ParseFromCmdLine(args);
            }
            catch (Exception ex)
            {
                var title = Assembly.GetEntryAssembly().GetProgramTitle();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }

            using (var presenter = new WmiPresenter(wmiArgs))
                if (wmiArgs != null && presenter.ConnectToComputer())
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    switch (wmiArgs.StartCmd)
                    {
                        case CmdLineCommand.EditProperty:
                            using (var propForm = new EditPropertyForm(presenter))
                                propForm.ShowDialog();
                            break;

                        case CmdLineCommand.ExecuteMethod:
                            using (var methodForm = new MethodForm(presenter))
                            {
                                methodForm.PrepareForm();
                                methodForm.ShowDialog();
                            }

                            break;

                        default:
                            WmiClassList.Instance.EnumLocalMachineClasses();
                            Application.Run(new MainForm(presenter));
                            break;
                    }
                }
        }
    }
}