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
            CmdLineArgs wmiArgs = null;
            try
            {
                wmiArgs = CmdLineArgs.ParseFromCmdLine(args);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, GetProgramTitle(), MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, RightToLeft.Options);
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