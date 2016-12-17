using System;
using System.Globalization;
using System.Windows.Forms;
using LanExchange.Presentation.WinForms.Properties;

namespace LanExchange.Presentation.WinForms.Helpers
{
    public static class MessageBoxHelper
    {
        public static void ShowRunCmdError(string cmdLine)
        {
            MessageBox.Show(string.Format(CultureInfo.CurrentCulture, Resources.PanelView_RunCmdErrorMsg, cmdLine),
                Resources.PanelView_RunCmdErrorCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
        }
    }
}
