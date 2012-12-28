using System;
using System.Collections.Generic;
using System.Text;
using GongSolutions.Shell.Interop;
using System.Drawing;
using System.Runtime.InteropServices;

namespace LanExchange.Utils
{
    class Shell32Utils
    {
        public const int MyComputerIndex = 49;
        public const int WorkgroupIndex = 18;
        public const int UsersIndex = 206;

        public static int GetIconIndex(CSIDL csidl)
        {
            IntPtr ppidl;
            HResult res;
            res = Shell32.SHGetSpecialFolderLocation(IntPtr.Zero, csidl, out ppidl);
            SHFILEINFO shfi = new SHFILEINFO();
            uint shfiSize = (uint)Marshal.SizeOf(shfi.GetType());
            IntPtr retVal = Shell32.SHGetFileInfo(
                ppidl, 0, out shfi, shfiSize,
                (SHGFI.SYSICONINDEX | SHGFI.SMALLICON | SHGFI.PIDL));
            if (!retVal.Equals(IntPtr.Zero))
                return shfi.iIcon;
            else
                return -1;
        }
    }
}
