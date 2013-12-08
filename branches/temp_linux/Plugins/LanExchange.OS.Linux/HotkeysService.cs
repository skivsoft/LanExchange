using System;
using System.ComponentModel;
using LanExchange.SDK.OS;

namespace LanExchange.OS.Linux
{
    /// <summary> This class allows you to manage a hotkey </summary>
    [Localizable(false)]
    internal class HotkeysService : IHotkeysService
    {
        public short HotkeyID
        {
            get { return 0; }
        }

        public string ShowWindowKey
        {
            get { return "Ctrl+Win+X"; }
        }

        public bool RegisterShowWindowKey(IntPtr handle)
        {
            return false;
        }

        public void Dispose()
        {
        }
    }
}