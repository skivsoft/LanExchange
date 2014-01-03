using System;
using System.Drawing;
using System.IO;
using LanExchange.SDK;

namespace LanExchange.OS.Linux
{
    internal class Shell32Service : IShell32Service
    {
        public void ShowContextMenu(IntPtr handle, FileInfo[] files, Point position)
        {
        }

        public void ShowMyComputerContextMenu(IntPtr handle, Point position)
        {
        }

        public void OpenMyComputer()
        {
        }

        public void FileIconInit(bool isFullInit)
        {
        }
    }
}