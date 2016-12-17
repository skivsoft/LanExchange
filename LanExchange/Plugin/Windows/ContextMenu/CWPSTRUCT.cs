using System;
using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Windows.ContextMenu
{
    // Defines the values used with the IShellFolder::GetDisplayNameOf and IShellFolder::SetNameOf 
    // methods to specify the type of file or folder names used by those methods
    [StructLayout(LayoutKind.Sequential)]
    internal struct CWPSTRUCT
    {
        public IntPtr lparam;
        public IntPtr wparam;
        public int message;
        public IntPtr hwnd;
    }
}