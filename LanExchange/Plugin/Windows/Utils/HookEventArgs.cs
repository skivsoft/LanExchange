using System;

namespace LanExchange.Plugin.Windows.Utils
{
    public class HookEventArgs : EventArgs
    {
        public int HookCode;    // Hook code
        public IntPtr WParam;    // WPARAM argument
        public IntPtr LParam;    // LPARAM argument
    }
}