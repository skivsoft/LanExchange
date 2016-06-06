using System;
using System.Drawing;
using System.IO;

namespace LanExchange.Presentation.Interfaces
{
    public interface IShell32Service
    {
        void ShowContextMenu(IntPtr handle, FileInfo[] files, Point position, bool control, bool shift);

        void ShowMyComputerContextMenu(IntPtr handle, Point position, bool control, bool shift);

        void OpenMyComputer();

        void FileIconInit(bool isFullInit);
    }
}