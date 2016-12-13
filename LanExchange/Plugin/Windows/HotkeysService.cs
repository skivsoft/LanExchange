using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using LanExchange.Application.Interfaces;

namespace LanExchange.Plugin.Windows
{
    /// <summary> This class allows you to manage a hotkey </summary>
    [Localizable(false)]
    // [CLSCompliant(false)]

    internal class HotkeysService : IHotkeyService
    {
        [DllImport(ExternDll.User32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, uint vk);

        [DllImport(ExternDll.User32, SetLastError = true)]
        public static extern int UnregisterHotKey(IntPtr hwnd, int id);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern short GlobalAddAtom([MarshalAs(UnmanagedType.LPWStr)]string lpString);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern short GlobalDeleteAtom(short nAtom);

        public const int MOD_ALT = 1;
        public const int MOD_CONTROL = 2;
        public const int MOD_SHIFT = 4;
        public const int MOD_WIN = 8;
        public const int KeyX = 88;

        public HotkeysService()
        {
        }

        /// <summary>Handle of the current process</summary>
        private IntPtr handle;

        /// <summary>The ID for the hotkey</summary>
        private short hotkeyId;

        /// <summary>Register the hotkey</summary>
        private bool RegisterGlobalHotKey(int hotkey, int modifiers, IntPtr handle)
        {
            UnregisterGlobalHotKey();
            this.handle = handle;
            // use the GlobalAddAtom API to get a unique ID(as suggested by MSDN)
            var atomName = this.handle.ToInt32().ToString("X8", CultureInfo.InvariantCulture) + GetType().FullName;
            hotkeyId = GlobalAddAtom(atomName);
            return hotkeyId != 0 && RegisterHotKey(this.handle, hotkeyId, (uint)modifiers, (uint)hotkey);
        }

        /// <summary>Unregister the hotkey</summary>
        private void UnregisterGlobalHotKey()
        {
            if (hotkeyId != 0)
            {
                UnregisterHotKey(handle, hotkeyId);
                // clean up the atom list
                GlobalDeleteAtom(hotkeyId);
                hotkeyId = 0;
            }
        }

        public void Dispose()
        {
            UnregisterGlobalHotKey();
        }

        public string ShowWindowKey
        {
            get { return "Ctrl+Win+X"; }
        }

        public bool RegisterShowWindowKey(IntPtr handle)
        {
            return RegisterGlobalHotKey(KeyX, MOD_CONTROL + MOD_WIN, handle);
        }

        public bool IsHotKey(short id)
        {
            return id == hotkeyId;
        }
    }
}