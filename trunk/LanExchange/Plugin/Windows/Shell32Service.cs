using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using LanExchange.Plugin.Windows.Utils;
using LanExchange.SDK;

namespace LanExchange.Plugin.Windows
{
    internal class Shell32Service : IShell32Service
    {
        private readonly ShellContextMenu m_Menu;

        public Shell32Service()
        {
            m_Menu = new ShellContextMenu();
        }

        public void ShowContextMenu(IntPtr handle, FileInfo[] files, Point position)
        {
            m_Menu.ShowContextMenu(handle, files, position);
        }

        public void ShowMyComputerContextMenu(IntPtr handle, Point position)
        {
            m_Menu.ShowContextMenuForCSIDL(handle, ShellAPI.CSIDL.DRIVES, position);
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