using System;
using System.Runtime.InteropServices;
using LanExchange.Plugin.Windows.Enums;

namespace LanExchange.Plugin.Windows.Structures
{
    // Contains information about a menu item
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct MENUITEMINFO
    {
        public int cbSize;
        public MIIM fMask;
        public MFT fType;
        public MFS fState;
        public uint wID;
        public IntPtr hSubMenu;
        public IntPtr hbmpChecked;
        public IntPtr hbmpUnchecked;
        public IntPtr dwItemData;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string dwTypeData;
        public int cch;
        public IntPtr hbmpItem;
    }
}