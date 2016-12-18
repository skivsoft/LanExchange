using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using LanExchange.Plugin.Windows.Enums;
using LanExchange.Plugin.Windows.Interfaces;
using LanExchange.Plugin.Windows.Structures;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Windows.Utils
{
    /// <summary>
    /// Summary description for SysImageList.
    /// </summary>
    [Localizable(false)]
    public sealed class SysImageList : IDisposable
    {
        private const int FILE_ATTRIBUTE_NORMAL = 0x80;
        private IntPtr hIml = IntPtr.Zero;
        private IImageList iImageList;
        private SysImageListSize size = SysImageListSize.SmallIcons;
        private bool disposed;

        /// <summary>
        /// Creates a Small Icons SystemImageList 
        /// </summary>
        public SysImageList()
        {
            Create();
        }

        /// <summary>
        /// Creates a SystemImageList with the specified size
        /// </summary>
        /// <param name="size">Size of System ImageList</param>
        public SysImageList(SysImageListSize size)
        {
            this.size = size;
            Create();
        }

        /// <summary>
        /// Finalise for SysImageList
        /// </summary>
        ~SysImageList()
        {
            Dispose(false);
        }

        public string DisplayName { get; set; }

        /// <summary>
        /// Gets the hImageList handle
        /// </summary>
        public IntPtr Handle
        {
            get { return hIml; }
        }

        /// <summary>
        /// Gets/sets the size of System Image List to retrieve.
        /// </summary>
        public SysImageListSize ImageListSize
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
                Create();
            }
        }

        /// <summary>
        /// Returns the size of the Image List Icons.
        /// </summary>
        public Size Size
        {
            get
            {
                int cx = 0;
                int cy = 0;
                if (iImageList == null)
                {
                    ImageList_GetIconSize(
                        hIml,
                        ref cx,
                        ref cy);
                }
                else
                {
                    iImageList.GetIconSize(ref cx, ref cy);
                }

                return new Size(cx, cy);
            }
        }

        /// <summary>
        /// Clears up any resources associated with the SystemImageList
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clears up any resources associated with the SystemImageList
        /// when disposing is true.
        /// </summary>
        /// <param name="disposing">Whether the object is being disposed</param>
        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (iImageList != null)
                    {
                        Marshal.ReleaseComObject(iImageList);
                    }

                    iImageList = null;
                }
            }

            disposed = true;
        }

        /// <summary>
        /// Returns a GDI+ copy of the icon from the ImageList
        /// at the specified index.
        /// </summary>
        /// <param name="index">The index to get the icon for</param>
        /// <returns>The specified icon</returns>
        public Icon Icon(int index)
        {
            Icon icon;

            IntPtr hIcon = IntPtr.Zero;
            if (iImageList == null)
            {
                hIcon = ImageList_GetIcon(
                    hIml,
                    index,
                    (int)ImageListDrawItemConstants.ILD_TRANSPARENT);
            }
            else
            {
                iImageList.GetIcon(
                    index,
                    (int)ImageListDrawItemConstants.ILD_TRANSPARENT,
                    ref hIcon);
            }

            if (hIcon != IntPtr.Zero)
                icon = System.Drawing.Icon.FromHandle(hIcon);
            else
                icon = null;
            return icon;
        }

        /// <summary>
        /// Return the index of the icon for the specified file, always using 
        /// the cached version where possible.
        /// </summary>
        /// <param name="fileName">Filename to get icon for</param>
        /// <returns>Index of the icon</returns>
        public int IconIndex(string fileName)
        {
            return IconIndex(fileName, false);
        }

        /// <summary>
        /// Returns the index of the icon for the specified file
        /// </summary>
        /// <param name="fileName">Filename to get icon for</param>
        /// <param name="forceLoadFromDisk">If True, then hit the disk to get the icon,
        /// otherwise only hit the disk if no cached icon is available.</param>
        /// <returns>Index of the icon</returns>
        public int IconIndex(
            string fileName,
            bool forceLoadFromDisk)
        {
            return IconIndex(
                fileName,
                forceLoadFromDisk,
                ShellIconStateConstants.ShellIconStateNormal);
        }

        /// <summary>
        /// Returns the index of the icon for the specified file
        /// </summary>
        /// <param name="fileName">Filename to get icon for</param>
        /// <param name="forceLoadFromDisk">If True, then hit the disk to get the icon,
        /// otherwise only hit the disk if no cached icon is available.</param>
        /// <param name="iconState">Flags specifying the state of the icon
        /// returned.</param>
        /// <returns>Index of the icon</returns>
        public int IconIndex(
            string fileName,
            bool forceLoadFromDisk,
            ShellIconStateConstants iconState)
        {
            var dwFlags = SHGetFileInfoConstants.SHGFI_SYSICONINDEX;
            int dwAttr;
            if (size == SysImageListSize.SmallIcons)
            {
                dwFlags |= SHGetFileInfoConstants.SHGFI_SMALLICON;
            }

            // We can choose whether to access the disk or not. If you don't
            // hit the disk, you may get the wrong icon if the icon is
            // not cached. Also only works for files.
            if (!forceLoadFromDisk)
            {
                dwFlags |= SHGetFileInfoConstants.SHGFI_USEFILEATTRIBUTES;
                dwAttr = FILE_ATTRIBUTE_NORMAL;
            }
            else
            {
                dwAttr = 0;
            }

            // sFileSpec can be any file. You can specify a
            // file that does not exist and still get the
            // icon, for example sFileSpec = "C:\PANTS.DOC"
            var shfi = new SHFILEINFO();
            var shfiSize = (uint)Marshal.SizeOf(shfi.GetType());
            IntPtr retVal = SHGetFileInfo(
                fileName,
                dwAttr,
                ref shfi,
                shfiSize,
                (uint)dwFlags | (uint)iconState);

            if (retVal.Equals(IntPtr.Zero))
            {
                System.Diagnostics.Debug.Assert(!retVal.Equals(IntPtr.Zero), "Failed to get icon index");
                return 0;
            }

            return shfi.iIcon;
        }

        /// <summary>
        /// Draws an image
        /// </summary>
        /// <param name="hdc">Device context to draw to</param>
        /// <param name="index">Index of image to draw</param>
        /// <param name="x">X Position to draw at</param>
        /// <param name="y">Y Position to draw at</param>
        public void DrawImage(
            IntPtr hdc,
            int index,
            int x,
            int y)
        {
            DrawImage(hdc, index, x, y, ImageListDrawItemConstants.ILD_TRANSPARENT);
        }

        /// <summary>
        /// Draws an image using the specified flags
        /// </summary>
        /// <param name="hdc">Device context to draw to</param>
        /// <param name="index">Index of image to draw</param>
        /// <param name="x">X Position to draw at</param>
        /// <param name="y">Y Position to draw at</param>
        /// <param name="flags">Drawing flags</param>
        public void DrawImage(
            IntPtr hdc,
            int index,
            int x,
            int y,
            ImageListDrawItemConstants flags)
        {
            if (iImageList == null)
            {
                ImageList_Draw(
                    hIml,
                    index,
                    hdc,
                    x,
                    y,
                    (int)flags);
            }
            else
            {
                var pimldp = new IMAGELISTDRAWPARAMS();
                pimldp.hdcDst = hdc;
                pimldp.cbSize = Marshal.SizeOf(pimldp.GetType());
                pimldp.i = index;
                pimldp.x = x;
                pimldp.y = y;
                pimldp.rgbFg = -1;
                pimldp.fStyle = (int)flags;
                iImageList.Draw(ref pimldp);
            }
        }

        /// <summary>
        /// Draws an image using the specified flags and specifies
        /// the size to clip to(or to stretch to if ILD_SCALE
        /// is provided).
        /// </summary>
        /// <param name="hdc">Device context to draw to</param>
        /// <param name="index">Index of image to draw</param>
        /// <param name="x">X Position to draw at</param>
        /// <param name="y">Y Position to draw at</param>
        /// <param name="flags">Drawing flags</param>
        /// <param name="cx">Width to draw</param>
        /// <param name="cy">Height to draw</param>
        public void DrawImage(
            IntPtr hdc,
            int index,
            int x,
            int y,
            ImageListDrawItemConstants flags,
            int cx,
            int cy)
        {
            var pimldp = new IMAGELISTDRAWPARAMS();
            pimldp.hdcDst = hdc;
            pimldp.cbSize = Marshal.SizeOf(pimldp.GetType());
            pimldp.i = index;
            pimldp.x = x;
            pimldp.y = y;
            pimldp.cx = cx;
            pimldp.cy = cy;
            pimldp.fStyle = (int)flags;
            if (iImageList == null)
            {
                pimldp.himl = hIml;
                ImageList_DrawIndirect(ref pimldp);
            }
            else
            {
                iImageList.Draw(ref pimldp);
            }
        }

        /// <summary>
        /// Determines if the system is running Windows XP
        /// or above
        /// </summary>
        /// <returns>True if system is running XP or above, False otherwise</returns>
        private static bool IsXpOrAbove()
        {
            bool ret = false;
            if (Environment.OSVersion.Version.Major > 5)
            {
                ret = true;
            }
            else if ((Environment.OSVersion.Version.Major == 5) &&
                (Environment.OSVersion.Version.Minor >= 1))
            {
                ret = true;
            }

            return ret;
        }

        [DllImport(ExternDll.Shell32, CharSet = CharSet.Unicode)]
        private static extern IntPtr SHGetFileInfo(
            string pszPath,
            int dwFileAttributes,
            ref SHFILEINFO psfi,
            uint cbFileInfo,
            uint uFlags);

        // [DllImport("user32.dll")]
        // private static extern int DestroyIcon(IntPtr hIcon);
        // private const int FILE_ATTRIBUTE_DIRECTORY = 0x10;
        // private const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x100; 
        // private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x2000;
        // private const int FORMAT_MESSAGE_FROM_HMODULE = 0x800;
        // private const int FORMAT_MESSAGE_FROM_STRING = 0x400;
        // private const int FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;
        // private const int FORMAT_MESSAGE_IGNORE_INSERTS = 0x200;
        // private const int FORMAT_MESSAGE_MAX_WIDTH_MASK = 0xFF;
        // [DllImport("kernel32")]
        // private extern static int FormatMessage (
        // int dwFlags, 
        // IntPtr lpSource, 
        // int dwMessageId, 
        // int dwLanguageId, 
        // string lpBuffer,
        // uint nSize, 
        // int argumentsLong);
        // [DllImport("kernel32")]
        // private extern static int GetLastError();
        [DllImport(ExternDll.Comctl32)]
        private static extern int ImageList_Draw(
            IntPtr hIml,
            int i,
            IntPtr hdcDst,
            int x,
            int y,
            int fStyle);

        [DllImport(ExternDll.Comctl32)]
        private static extern int ImageList_DrawIndirect(
            ref IMAGELISTDRAWPARAMS pimldp);

        [DllImport(ExternDll.Comctl32)]
        private static extern int ImageList_GetIconSize(
            IntPtr himl,
            ref int cx,
            ref int cy);

        [DllImport(ExternDll.Comctl32)]
        private static extern IntPtr ImageList_GetIcon(
            IntPtr himl,
            int i,
            int flags);

        /// <summary>
        /// SHGetImageList is not exported correctly in XP.  See KB316931
        /// http:// support.microsoft.com/default.aspx?scid = kb;EN-US;Q316931
        /// Apparently (and hopefully) ordinal 727 isn't going to change.
        /// </summary>
        [DllImport(ExternDll.Shell32, EntryPoint = "#727")]
        private static extern int SHGetImageList(
            int iImageList,
            ref Guid riid,
            ref IImageList ppv);

        [DllImport(ExternDll.Shell32, EntryPoint = "#727")]
        private static extern int SHGetImageListHandle(
            int iImageList,
            ref Guid riid,
            ref IntPtr handle);

        /// <summary>
        /// Creates the SystemImageList
        /// </summary>
        private void Create()
        {
            // forget last image list if any:
            hIml = IntPtr.Zero;

            if (IsXpOrAbove())
            {
                // Get the System IImageList object from the Shell:
                var iidImageList = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");
                SHGetImageList(
                    (int)size,
                    ref iidImageList,
                    ref iImageList);

                // the image list handle is the IUnknown pointer, but 
                // using Marshal.GetIUnknownForObject doesn't return
                // the right value.  It really doesn't hurt to make
                // a second call to get the handle:
                SHGetImageListHandle((int)size, ref iidImageList, ref hIml);
            }
            else
            {
                // Prepare flags:
                SHGetFileInfoConstants dwFlags = SHGetFileInfoConstants.SHGFI_USEFILEATTRIBUTES | SHGetFileInfoConstants.SHGFI_SYSICONINDEX;
                if (size == SysImageListSize.SmallIcons)
                {
                    dwFlags |= SHGetFileInfoConstants.SHGFI_SMALLICON;
                }

                // Get image list
                var shfi = new SHFILEINFO();
                var shfiSize = (uint)Marshal.SizeOf(shfi.GetType());

                // Call SHGetFileInfo to get the image list handle
                // using an arbitrary file:
                hIml = SHGetFileInfo(
                    ".txt",
                    FILE_ATTRIBUTE_NORMAL,
                    ref shfi,
                    shfiSize,
                    (uint)dwFlags);
                System.Diagnostics.Debug.Assert(hIml != IntPtr.Zero, "Failed to create Image List");
            }
        }
    }
}
