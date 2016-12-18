using System;

namespace LanExchange.Plugin.Windows.Utils
{
    public class HookEventArgs : EventArgs
    {
        public HookEventArgs(int hookCode, IntPtr wparam, IntPtr lparam)
        {
            HookCode = hookCode;
            WParam = wparam;
            LParam = lparam;
        }

        public int HookCode { get; }

        public IntPtr WParam { get; }

        public IntPtr LParam { get; }
    }
}