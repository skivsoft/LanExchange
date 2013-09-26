using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace WMIViewer
{
    static class Program
    {
        [STAThread]
        [Localizable(true)]
        static void Main(string[] args)
        {
            WMIArgs wmiArgs = null;
            try
            {
                wmiArgs = WMIArgs.ParseFromCmdLine(args);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, GetProgramTitle(), MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, RtlUtils.Options);
            }
            using (var presenter = new WMIPresenter(wmiArgs))
                if (wmiArgs != null && presenter.ConnectToComputer())
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    switch (wmiArgs.StartCmd)
                    {
                        case WMIStartCommand.EditProperty:
                            using (var propForm = new WMIEditProperty(presenter))
                                propForm.ShowDialog();
                            break;
                        case WMIStartCommand.ExecuteMethod:
                            using (var methodForm = new WMIMethodForm(presenter))
                            {
                                methodForm.PrepareForm();
                                methodForm.ShowDialog();
                            }
                            break;
                        default:
                            WMIClassList.Instance.EnumLocalMachineClasses();
                            Application.Run(new WMIForm(presenter));
                            break;
                    }
                }
        }

        public static string GetProgramTitle()
        {
            var assembly = Assembly.GetEntryAssembly();
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if (!string.IsNullOrEmpty(titleAttribute.Title))
                    return titleAttribute.Title;
            }
            return Path.GetFileNameWithoutExtension(assembly.CodeBase);
        }

    }
}