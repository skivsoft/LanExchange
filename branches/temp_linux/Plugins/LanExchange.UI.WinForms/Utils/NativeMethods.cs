using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LanExchange.UI.WinForms.Utils
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
        private const int LVM_GETCOLUMN = LVM_FIRST + 95;
        private const int LVM_GETCOUNTPERPAGE = LVM_FIRST + 40;
        private const int LVM_GETGROUPINFO = LVM_FIRST + 149;
        private const int LVM_GETGROUPSTATE = LVM_FIRST + 92;
        private const int LVM_GETHEADER = LVM_FIRST + 31;
        private const int LVM_GETTOOLTIPS = LVM_FIRST + 78;
        private const int LVM_GETTOPINDEX = LVM_FIRST + 39;
        private const int LVM_HITTEST = LVM_FIRST + 18;
        private const int LVM_INSERTGROUP = LVM_FIRST + 145;
        private const int LVM_REMOVEALLGROUPS = LVM_FIRST + 160;
        private const int LVM_SCROLL = LVM_FIRST + 20;
        private const int LVM_SETBKIMAGE = LVM_FIRST + 0x8A;
        private const int LVM_SETCOLUMN = LVM_FIRST + 96;
        private const int LVM_SETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 54;
        private const int LVM_SETGROUPINFO = LVM_FIRST + 147;
        private const int LVM_SETGROUPMETRICS = LVM_FIRST + 155;
        private const int LVM_SETIMAGELIST = LVM_FIRST + 3;
        private const int LVM_SETITEM = LVM_FIRST + 76;
        private const int LVM_SETITEMCOUNT = LVM_FIRST + 47;
        private const int LVM_SETITEMSTATE = LVM_FIRST + 43;
        private const int LVM_SETSELECTEDCOLUMN = LVM_FIRST + 140;
        private const int LVM_SETTOOLTIPS = LVM_FIRST + 74;
        private const int LVM_SUBITEMHITTEST = LVM_FIRST + 57;
        private const int LVS_EX_SUBITEMIMAGES = 0x0002;

        private const int LVIF_TEXT = 0x0001;
        private const int LVIF_IMAGE = 0x0002;
        private const int LVIF_PARAM = 0x0004;
        private const int LVIF_STATE = 0x0008;
        private const int LVIF_INDENT = 0x0010;
        private const int LVIF_NORECOMPUTE = 0x0800;

        private const int LVCF_FMT = 0x0001;
        private const int LVCF_WIDTH = 0x0002;
        private const int LVCF_TEXT = 0x0004;
        private const int LVCF_SUBITEM = 0x0008;
        private const int LVCF_IMAGE = 0x0010;
        private const int LVCF_ORDER = 0x0020;
        private const int LVCFMT_LEFT = 0x0000;
        private const int LVCFMT_RIGHT = 0x0001;
        private const int LVCFMT_CENTER = 0x0002;
        private const int LVCFMT_JUSTIFYMASK = 0x0003;

        private const int LVCFMT_IMAGE = 0x0800;
        private const int LVCFMT_BITMAP_ON_RIGHT = 0x1000;
        private const int LVCFMT_COL_HAS_IMAGES = 0x8000;

        private const int LVBKIF_SOURCE_NONE = 0x0;
        private const int LVBKIF_SOURCE_HBITMAP = 0x1;
        private const int LVBKIF_SOURCE_URL = 0x2;
        private const int LVBKIF_SOURCE_MASK = 0x3;
        private const int LVBKIF_STYLE_NORMAL = 0x0;
        private const int LVBKIF_STYLE_TILE = 0x10;
        private const int LVBKIF_STYLE_MASK = 0x10;
        private const int LVBKIF_FLAG_TILEOFFSET = 0x100;
        private const int LVBKIF_TYPE_WATERMARK = 0x10000000;
        private const int LVBKIF_FLAG_ALPHABLEND = 0x20000000;

        private const int LVSICF_NOINVALIDATEALL = 1;
        private const int LVSICF_NOSCROLL = 2;

        private const int HDM_FIRST = 0x1200;
        private const int HDM_HITTEST = HDM_FIRST + 6;
        private const int HDM_GETITEMRECT = HDM_FIRST + 7;
        private const int HDM_GETITEM = HDM_FIRST + 11;
        private const int HDM_SETITEM = HDM_FIRST + 12;

        private const int HDI_WIDTH = 0x0001;
        private const int HDI_TEXT = 0x0002;
        private const int HDI_FORMAT = 0x0004;
        private const int HDI_BITMAP = 0x0010;
        private const int HDI_IMAGE = 0x0020;

        private const int HDF_LEFT = 0x0000;
        private const int HDF_RIGHT = 0x0001;
        private const int HDF_CENTER = 0x0002;
        private const int HDF_JUSTIFYMASK = 0x0003;
        private const int HDF_RTLREADING = 0x0004;
        private const int HDF_STRING = 0x4000;
        private const int HDF_BITMAP = 0x2000;
        private const int HDF_BITMAP_ON_RIGHT = 0x1000;
        private const int HDF_IMAGE = 0x0800;
        private const int HDF_SORTUP = 0x0400;
        private const int HDF_SORTDOWN = 0x0200;

        private const int SB_HORZ = 0;
        private const int SB_VERT = 1;
        private const int SB_CTL = 2;
        private const int SB_BOTH = 3;

        private const int SIF_RANGE = 0x0001;
        private const int SIF_PAGE = 0x0002;
        private const int SIF_POS = 0x0004;
        private const int SIF_DISABLENOSCROLL = 0x0008;
        private const int SIF_TRACKPOS = 0x0010;
        private const int SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS);
        
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
            //if (_WIN32_IE >= 0x0500)
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
            public int cbSize = Marshal.SizeOf(typeof(NativeMethods.SCROLLINFO));
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
        public static extern IntPtr SendMessageHDHITTESTINFO(IntPtr hWnd, int Msg, IntPtr wParam, [In, Out] HDHITTESTINFO lParam);
        [DllImport(ExternDll.User32, CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetScrollInfo(IntPtr hWnd, int fnBar, SCROLLINFO scrollInfo);

        #endregion

        /// <summary>
        /// Return the handle to the header control on the given list
        /// </summary>
        /// <param name="list">The listview whose header control is to be returned</param>
        /// <returns>The handle to the header control</returns>
        public static IntPtr GetHeaderControl(ListView list)
        {
            return SendMessage(list.Handle, LVM_GETHEADER, 0, 0);
        }

        /// <summary>
        /// Setup the given column of the listview to show the given image to the right of the text.
        /// If the image index is -1, any previous image is cleared
        /// </summary>
        /// <param name="list">The listview to send a m to</param>
        /// <param name="columnIndex">Index of the column to modifiy</param>
        /// <param name="order"></param>
        /// <param name="imageIndex">Index into the small image list</param>
        public static void SetColumnImage(ListView list, int columnIndex, SortOrder order, int imageIndex)
        {
            IntPtr hdrCntl = NativeMethods.GetHeaderControl(list);
            if (hdrCntl.ToInt32() == 0)
                return;

            HDITEM item = new HDITEM();
            item.mask = HDI_FORMAT;
            IntPtr result = SendMessageHDItem(hdrCntl, HDM_GETITEM, columnIndex, ref item);

            item.fmt &= ~(HDF_SORTUP | HDF_SORTDOWN | HDF_IMAGE | HDF_BITMAP_ON_RIGHT);

            //if (NativeMethods.HasBuiltinSortIndicators())
            {
                if (order == SortOrder.Ascending)
                    item.fmt |= HDF_SORTUP;
                if (order == SortOrder.Descending)
                    item.fmt |= HDF_SORTDOWN;
            }
            //else
            //{
            //    item.mask |= HDI_IMAGE;
            //    item.fmt |= (HDF_IMAGE | HDF_BITMAP_ON_RIGHT);
            //    item.iImage = imageIndex;
            //}

            result = SendMessageHDItem(hdrCntl, HDM_SETITEM, columnIndex, ref item);
        }

        /// <summary>
        /// Get the scroll position of the given scroll bar
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="horizontalBar"></param>
        /// <returns></returns>
        public static int GetScrollPosition(ListView lv, bool horizontalBar)
        {
            int fnBar = (horizontalBar ? SB_HORZ : SB_VERT);

            SCROLLINFO scrollInfo = new SCROLLINFO();
            scrollInfo.fMask = SIF_POS;
            if (GetScrollInfo(lv.Handle, fnBar, scrollInfo))
                return scrollInfo.nPos;
            else
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
            return NativeMethods.HeaderControlHitTest(handle, pt, HHT_ONHEADER | HHT_ONDIVIDER);
        }

        private static int HeaderControlHitTest(IntPtr handle, Point pt, int flag)
        {
            HDHITTESTINFO testInfo = new HDHITTESTINFO();
            testInfo.pt_x = pt.X;
            testInfo.pt_y = pt.Y;
            IntPtr result = NativeMethods.SendMessageHDHITTESTINFO(handle, HDM_HITTEST, IntPtr.Zero, testInfo);
            if ((testInfo.flags & flag) != 0)
                return testInfo.iItem;
            else
                return -1;
        }

    }
}