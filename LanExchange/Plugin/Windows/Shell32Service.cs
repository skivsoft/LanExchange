using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using LanExchange.Application.Interfaces;
using LanExchange.Plugin.Windows.Utils;

namespace LanExchange.Plugin.Windows
{
    internal sealed class Shell32Service : IShell32Service
    {
        private readonly ShellContextMenu menu;

        public Shell32Service()
        {
            menu = new ShellContextMenu();
        }

        public void ShowContextMenu(IntPtr handle, FileInfo[] files, Point position)
        {
            menu.ShowContextMenu(handle, files, position);
        }

        public void ShowMyComputerContextMenu(IntPtr handle, Point position)
        {
            menu.ShowContextMenuForCSIDL(handle, ShellAPI.CSIDL.DRIVES, position);
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