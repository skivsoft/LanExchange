using System;

namespace LanExchange.OS.Windows.Utils
{
    public class HookEventArgs : EventArgs
    {
        public int HookCode;	// Hook code
        public IntPtr wParam;	// WPARAM argument
        public IntPtr lParam;	// LPARAM argument
    }
}