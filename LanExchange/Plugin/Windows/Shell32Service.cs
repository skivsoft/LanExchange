using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using LanExchange.Plugin.Windows.Enums;
using LanExchange.Plugin.Windows.Utils;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Windows
{
    internal sealed class Shell32Service : IShell32Service
    {
        private readonly ShellContextMenu menu;

        public Shell32Service()
        {
            menu = new ShellContextMenu();
        }

        public void ShowContextMenu(IntPtr handle, FileInfo[] files, Point position, bool control, bool shift)
        {
            menu.ShowContextMenu(handle, files, position, control, shift);
        }

        public void ShowMyComputerContextMenu(IntPtr handle, Point position, bool control, bool shift)
        {
            menu.ShowContextMenuForCSIDL(handle, CSIDL.DRIVES, position, control, shift);
        }

        public void OpenMyComputer()
        {
            Process.Start("explorer.exe", "/n,::{20D04FE0 - 3AEA - 1069-A2D8 - 08002B30309D}");
        }

        public void FileIconInit(bool isFullInit)
        {
            ShellAPI.FileIconInit(isFullInit);
        }
    }
}