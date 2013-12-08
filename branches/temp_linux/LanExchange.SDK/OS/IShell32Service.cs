using System;

namespace LanExchange.SDK.OS
{
    public interface IShell32Service
    {

        void ShowMyComputerContextMenu(IntPtr handle);

        void OpenMyComputer();

        void FileIconInit(bool isFullInit);
    }
}