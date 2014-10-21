using System;
using System.Globalization;
using System.Windows.Forms;
using LanExchange.Properties;

namespace LanExchange.Plugin.WinForms.Utils
{
    public static class MessageBoxHelper
    {
        public static void ShowRunCmdError(string cmdLine)
        {
            MessageBox.Show(String.Format(CultureInfo.CurrentCulture, Resources.PanelView_RunCmdErrorMsg, cmdLine),
                Resources.PanelView_RunCmdErrorCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
        }
    }
}
