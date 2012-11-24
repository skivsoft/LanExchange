using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OSTools
{
    class NativeMethods
    {
        private const int LVM_FIRST = 0x1000;
        private const int LVM_SETITEMSTATE = LVM_FIRST + 43;

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

       
        /// <summary>
        /// Select all rows on the given listview
        /// </summary>
        /// <param name="list">The listview whose items are to be selected</param>
        public static void SelectAllItems(ListView list)
        {
            NativeMethods.SetItemState(list, -1, 2, 2);
        }

        /// <summary>
        /// Deselect all rows on the given listview
        /// </summary>
        /// <param name="list">The listview whose items are to be deselected</param>
        public static void DeselectAllItems(ListView list)
        {
            NativeMethods.SetItemState(list, -1, 2, 0);
        }

        /// <summary>
        /// Set the item state on the given item
        /// </summary>
        /// <param name="list">The listview whose item's state is to be changed</param>
        /// <param name="itemIndex">The index of the item to be changed</param>
        /// <param name="mask">Which bits of the value are to be set?</param>
        /// <param name="value">The value to be set</param>
        public static void SetItemState(ListView list, int itemIndex, int mask, int value)
        {
            LVITEM lvItem = new LVITEM();
            lvItem.stateMask = mask;
            lvItem.state = value;
            SendMessageLVItem(list.Handle, LVM_SETITEMSTATE, itemIndex, ref lvItem);
        }

        [DllImport("User32.dll")]
        public static extern bool ShowWindow(int hWnd, int cmdShow);

        //SetForeground, чтоб активировать окно по хендлу
        [DllImportAttribute("User32.dll")]
        public static extern IntPtr SetForegroundWindow(int hWnd);
    }

    public static class OsVersionHelper
    {

        // Will return result of 64but OS on all versions of windows that support .net
        public static bool Is64BitOperatingSystem()
        {
            if (IntPtr.Size == 8) //64bit will only run on 64bit
            {
                return true;
            }

            bool flag;
            return (DoesWin32MethodExist("kernel32.dll", "IsWow64Process") && IsWow64Process(GetCurrentProcess(), out flag)) && flag;
        }

        private static bool DoesWin32MethodExist(string moduleName, string methodName)
        {
            IntPtr moduleHandle = GetModuleHandle(moduleName);
            if (moduleHandle == IntPtr.Zero)
            {
                return false;
            }

            return GetProcAddress(moduleHandle, methodName) != IntPtr.Zero;
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr module, [MarshalAs(UnmanagedType.LPStr)]string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process(IntPtr process, out bool wow64Process);
    }

    public class PathUtils
    {
        public static void ExplodeCmd(string CmdLine, out string FName, out string Params)
        {
            FName = "";
            Params = "";
            bool bQuote = false;
            bool bParam = false;
            for (int i = 0; i < CmdLine.Length; i++)
            {
                if (!bParam && CmdLine[i] == '"')
                {
                    bQuote = !bQuote;
                    continue;
                }
                if (!bParam && !bQuote && CmdLine[i] == ' ')
                    bParam = true;
                else
                    if (bParam)
                        Params += CmdLine[i].ToString();
                    else
                        FName += CmdLine[i].ToString();
            }
        }
    }
}
