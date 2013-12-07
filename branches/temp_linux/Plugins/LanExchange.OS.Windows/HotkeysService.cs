using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using LanExchange.SDK.OS;

namespace LanExchange.OS.Windows
{
    /// <summary> This class allows you to manage a hotkey </summary>
    [Localizable(false)]
    //[CLSCompliant(false)]
    internal class HotkeysService : IHotkeysService
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
            Handle = Process.GetCurrentProcess().Handle;
        }

        /// <summary>Handle of the current process</summary>
        public IntPtr Handle;

        /// <summary>The ID for the hotkey</summary>
        public short HotkeyID { get; private set; }

        /// <summary>Register the hotkey</summary>
        public bool RegisterGlobalHotKey(int hotkey, int modifiers, IntPtr handle)
        {
            UnregisterGlobalHotKey();
            Handle = handle;
            return RegisterGlobalHotKey(hotkey, modifiers);
        }

        /// <summary>Register the hotkey</summary>
        public bool RegisterGlobalHotKey(int hotkey, int modifiers)
        {
            UnregisterGlobalHotKey();
            // use the GlobalAddAtom API to get a unique ID (as suggested by MSDN)
            string atomName = Handle.ToInt32().ToString("X8", CultureInfo.InvariantCulture) + GetType().FullName;
            HotkeyID = GlobalAddAtom(atomName);
            if (HotkeyID == 0)
                return false;
            // register the hotkey, throw if any error
            return RegisterHotKey(Handle, HotkeyID, (uint) modifiers, (uint) hotkey);
        }

        /// <summary>Unregister the hotkey</summary>
        public void UnregisterGlobalHotKey()
        {
            if (HotkeyID != 0)
            {
                UnregisterHotKey(Handle, HotkeyID);
                // clean up the atom list
                GlobalDeleteAtom(HotkeyID);
                HotkeyID = 0;
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
            return RegisterGlobalHotKey(KeyX, MOD_CONTROL + MOD_WIN, Handle);
        }
    }
}