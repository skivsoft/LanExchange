using System;
using System.Diagnostics;
using System.Windows.Forms;
using LanExchange.OS.Windows.Utils;
using LanExchange.SDK.OS;

namespace LanExchange.OS.Windows
{
    internal class Shell32Service : IShell32Service
    {
        public void ShowMyComputerContextMenu(IntPtr handle)
        {
            var scm = new ShellContextMenu();
            scm.ShowContextMenuForCSIDL(handle, ShellAPI.CSIDL.DRIVES, Cursor.Position);
        }

        public void OpenMyComputer()
        {
            Process.Start("explorer.exe", "/n,::{20D04FE0-3AEA-1069-A2D8-08002B30309D}");
        }

        public void FileIconInit(bool isFullInit)
        {
            ShellAPI.FileIconInit(isFullInit);
        }
    }
}