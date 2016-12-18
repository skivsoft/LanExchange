using System;
using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Windows.Structures
{
    // Contains and receives information for change notifications
    [StructLayout(LayoutKind.Sequential)]
    public struct SHChangeNotifyEntry
    {
        public IntPtr pIdl;
        public bool Recursively;
    }
}