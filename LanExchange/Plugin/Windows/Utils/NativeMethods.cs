using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Windows.Utils
{
    [Localizable(false)]
    internal static class NativeMethods
    {
        public static HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);

        public const int
            WM_QUERYENDSESSION = 0x0011,
            WM_ENDSESSION = 0x0016,
            SW_HIDE = 0,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_MAX = 10,
            SWP_NOSIZE = 0x0001,
            SWP_NOMOVE = 0x0002,
            SWP_NOZORDER = 0x0004,
            SWP_NOACTIVATE = 0x0010,
            SWP_SHOWWINDOW = 0x0040,
            SWP_HIDEWINDOW = 0x0080,
            SWP_DRAWFRAME = 0x0020,
            SWP_NOOWNERZORDER = 0x0200;

        public const uint
            LP_ENDSESSION_CLOSEAPP = 0x00000001,
            LP_ENDSESSION_CRITICAL = 0x40000000,
            LP_ENDSESSION_LOGOFF   = 0x80000000;

        #region Constants
        private const int LVM_FIRST = 0x1000;
        private const int LVM_GETHEADER = LVM_FIRST + 31;


        private const int HDM_FIRST = 0x1200;
        private const int HDM_HITTEST = HDM_FIRST + 6;
        private const int HDM_GETITEM = HDM_FIRST + 11;
        private const int HDM_SETITEM = HDM_FIRST + 12;

        private const int HDI_FORMAT = 0x0004;

        private const int HDF_BITMAP_ON_RIGHT = 0x1000;
        private const int HDF_IMAGE = 0x0800;
        private const int HDF_SORTUP = 0x0400;
        private const int HDF_SORTDOWN = 0x0200;

        private const int SB_HORZ = 0;
        private const int SB_VERT = 1;

        private const int SIF_POS = 0x0004;
        
        #endregion

        #region Structures

        [StructLayout(LayoutKind.Sequential)]
        public struct HDITEM
        {
            public int mask;
            public int cxy;
            public IntPtr pszText;
            public IntPtr hbm;
            public int cchTextMax;
            public int fmt;
            public IntPtr lParam;
            public int iImage;
            public int iOrder;
            // if (_WIN32_IE >= 0x0500)

            public int type;
            public IntPtr pvFilter;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class HDHITTESTINFO
        {
            public int pt_x;
            public int pt_y;
            public int flags;
            public int iItem;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class SCROLLINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(SCROLLINFO));
            public int fMask;
            public int nMin;
            public int nMax;
            public int nPage;
            public int nPos;
            public int nTrackPos;
        }
        #endregion


        #region Entry points

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport(ExternDll.User32, EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessageHDItem(IntPtr hWnd, int msg, int wParam, ref HDITEM hdi);

        [DllImport(ExternDll.User32, EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageHDHITTESTINFO(IntPtr hWnd, int msg, IntPtr wParam, [In, Out] HDHITTESTINFO lParam);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetScrollInfo(IntPtr hWnd, int fnBar, SCROLLINFO scrollInfo);

        #endregion

        /// <summary>
        /// Return the handle to the header control on the given list
        /// </summary>
        /// <param name="listViewHandle">The listview whose header control is to be returned</param>
        /// <returns>The handle to the header control</returns>
        public static IntPtr GetHeaderControl(IntPtr listViewHandle)
        {
            return SendMessage(listViewHandle, LVM_GETHEADER, 0, 0);
        }

        /// <summary>
        /// Setup the given column of the listview to show the given image to the right of the text.
        /// If the image index is -1, any previous image is cleared
        /// </summary>
        /// <param name="listViewHandle">The listview to send a m to</param>
        /// <param name="columnIndex">Index of the column to modifiy</param>
        /// <param name="order"></param>
        /// <param name="imageIndex">Index into the small image list</param>
        public static void SetColumnImage(IntPtr listViewHandle, int columnIndex, int order, int imageIndex)
        {
            IntPtr hdrCntl = GetHeaderControl(listViewHandle);
            if (hdrCntl.ToInt32() == 0)
                return;

            var item = new HDITEM();
            item.mask = HDI_FORMAT;
            SendMessageHDItem(hdrCntl, HDM_GETITEM, columnIndex, ref item);

            item.fmt &= ~(HDF_SORTUP | HDF_SORTDOWN | HDF_IMAGE | HDF_BITMAP_ON_RIGHT);

            // if (NativeMethods.HasBuiltinSortIndicators())

            {
                if (order == 1) // ascending
                    item.fmt |= HDF_SORTUP;
                if (order == 2) // descending
                    item.fmt |= HDF_SORTDOWN;
            }
            // else

            // {

            // item.mask |= HDI_IMAGE;

            // item.fmt |= (HDF_IMAGE | HDF_BITMAP_ON_RIGHT);

            // item.iImage = imageIndex;

            // }


            SendMessageHDItem(hdrCntl, HDM_SETITEM, columnIndex, ref item);
        }

        /// <summary>
        /// Get the scroll position of the given scroll bar
        /// </summary>
        /// <param name="listViewHandle"></param>
        /// <param name="horizontalBar"></param>
        /// <returns></returns>
        public static int GetScrollPosition(IntPtr listViewHandle, bool horizontalBar)
        {
            int fnBar = horizontalBar ? SB_HORZ : SB_VERT;

            var scrollInfo = new SCROLLINFO();
            scrollInfo.fMask = SIF_POS;
            if (GetScrollInfo(listViewHandle, fnBar, scrollInfo))
                return scrollInfo.nPos;
            return -1;
        }

        /// <summary>
        /// Return the index of the column of the header that is under the given point.
        /// Return -1 if no column is under the pt
        /// </summary>
        /// <param name="handle">The list we are interested in</param>
        /// <param name="pt">The client co-ords</param>
        /// <returns>The index of the column under the point, or -1 if no column header is under that point</returns>
        public static int GetColumnUnderPoint(IntPtr handle, Point pt)
        {
            const int HHT_ONHEADER = 2;
            const int HHT_ONDIVIDER = 4;
            return HeaderControlHitTest(handle, pt, HHT_ONHEADER | HHT_ONDIVIDER);
        }

        private static int HeaderControlHitTest(IntPtr handle, Point pt, int flag)
        {
            var testInfo = new HDHITTESTINFO();
            testInfo.pt_x = pt.X;
            testInfo.pt_y = pt.Y;
            SendMessageHDHITTESTINFO(handle, HDM_HITTEST, IntPtr.Zero, testInfo);
            if ((testInfo.flags & flag) != 0)
                return testInfo.iItem;
            return -1;
        }

    }
}