using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LanExchange.OSLayer
{
    public class User32
    {
        public const int LVM_FIRST = 0x1000;
        public const int LVM_SETITEMSTATE = LVM_FIRST + 43;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct LVITEM
        {
            public int mask;
            public int iItem;
            public int iSubItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pszText;
            public int cchTextMax;
            public int iImage;
            public IntPtr lParam;
            public int iIndent;
            public int iGroupId;
            public int cColumns;
            public IntPtr puColumns;
        };

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageLVItem(IntPtr hWnd, int msg, int wParam, ref LVITEM lvi);

        [DllImport("User32.dll")]
        public static extern bool ShowWindow(int hWnd, int cmdShow);

        //SetForeground, чтоб активировать окно по хендлу
        [DllImportAttribute("User32.dll")]
        public static extern IntPtr SetForegroundWindow(int hWnd);
    }
}
