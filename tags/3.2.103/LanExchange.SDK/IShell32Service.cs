using System;
using System.Drawing;
using System.IO;

namespace LanExchange.SDK
{
    public interface IShell32Service
    {
        void ShowContextMenu(IntPtr handle, FileInfo[] files, Point position);

        void ShowMyComputerContextMenu(IntPtr handle, Point position);

        void OpenMyComputer();

        void FileIconInit(bool isFullInit);
    }
}