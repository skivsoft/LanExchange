using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using LanExchange.Plugin.Windows.Enums;
using LanExchange.Plugin.Windows.Structures;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

namespace LanExchange.Plugin.Windows.Utils
{
    /// <summary>
    /// This class contains every method, enumeration, struct and constants from the Windows API, which are
    /// required by the FileBrowser
    /// </summary>
    [Localizable(false)]
    [CLSCompliant(false)]
    public static class ShellAPI
    {
        public const int MAX_PATH = 260;
        public const uint CMD_FIRST = 1;
        public const uint CMD_LAST = 30000;

        public const int S_OK = 0, S_FALSE = 1;
        public const int DRAGDROP_S_DROP = 0x00040100;
        public const int DRAGDROP_S_CANCEL = 0x00040101;
        public const int DRAGDROP_S_USEDEFAULTCURSORS = 0x00040102;

        private static Guid IID_DesktopGUID = new Guid("{00021400-0000-0000-C000-000000000046}");
        private static Guid IID_IShellFolder = new Guid("{000214E6-0000-0000-C000-000000000046}");
        private static Guid IID_IContextMenu = new Guid("{000214e4-0000-0000-c000-000000000046}");
        private static Guid IID_IContextMenu2 = new Guid("{000214f4-0000-0000-c000-000000000046}");
        private static Guid IID_IContextMenu3 = new Guid("{bcfce0a0-ec17-11d0-8d10-00a0c90f2719}");
        private static Guid IID_IDropTarget = new Guid("{00000122-0000-0000-C000-000000000046}");
        private static Guid IID_IDataObject = new Guid("{0000010e-0000-0000-C000-000000000046}");
        private static Guid IID_IQueryInfo = new Guid("{00021500-0000-0000-C000-000000000046}");
        private static Guid IID_IPersistFile = new Guid("{0000010b-0000-0000-C000-000000000046}");
        private static Guid CLSID_DragDropHelper = new Guid("{4657278A-411B-11d2-839A-00C04FD918D0}");
        private static Guid CLSID_NewMenu = new Guid("{D969A300-E7FF-11d0-A93B-00A0C90F2719}");
        private static Guid IID_IDragSourceHelper = new Guid("{DE5BF786-477A-11d2-839D-00C04FD918D0}");
        private static Guid IID_IDropTargetHelper = new Guid("{4657278B-411B-11d2-839A-00C04FD918D0}");
        private static Guid IID_IShellExtInit = new Guid("{000214e8-0000-0000-c000-000000000046}");
        private static Guid IID_IStream = new Guid("{0000000c-0000-0000-c000-000000000046}");
        private static Guid IID_IStorage = new Guid("{0000000B-0000-0000-C000-000000000046}");

        private static int cbFileInfo = Marshal.SizeOf(typeof(SHFILEINFO));
        private static int cbMenuItemInfo = Marshal.SizeOf(typeof(MENUITEMINFO));
        private static int cbInvokeCommand = Marshal.SizeOf(typeof(CMINVOKECOMMANDINFOEX));

        [DllImport(ExternDll.Shell32, EntryPoint = "#660")]
        public static extern bool FileIconInit(bool bFullInit);

        // Retrieves information about an object in the file system,
        // such as a file, a folder, a directory, or a drive root.
        // [DllImport("shell32", 
        // EntryPoint = "SHGetFileInfo", 
        // ExactSpelling = false, 
        // CharSet = CharSet.Auto,
        // SetLastError = true)]
        // public static extern IntPtr SHGetFileInfo(
        // string pszPath, 
        // FILE_ATTRIBUTE dwFileAttributes, 
        // ref SHFILEINFO sfi,
        // int cbFileInfo, 
        // SHGFI uFlags);

        // Retrieves information about an object in the file system,
        // such as a file, a folder, a directory, or a drive root.
        [DllImport(ExternDll.Shell32, 
            EntryPoint = "SHGetFileInfo", 
            ExactSpelling = false, 
            CharSet = CharSet.Auto, 
            SetLastError = true)]
        public static extern IntPtr SHGetFileInfo(
            IntPtr ppidl,
            FILE_ATTRIBUTE dwFileAttributes, 
            ref SHFILEINFO sfi,
            int cbFileInfo,
            SHGFI uFlags);

        // Takes the CSIDL of a folder and returns the pathname.
        // [DllImport("shell32.dll")]
        // public static extern int SHGetFolderPath(
        // IntPtr hwndOwner,
        // CSIDL nFolder,
        // IntPtr hToken,
        // SHGFP dwFlags,
        // StringBuilder pszPath);
        // Retrieves the IShellFolder interface for the desktop folder,
        // which is the root of the Shell's namespace. 
        [DllImport(ExternDll.Shell32)]
        public static extern int SHGetDesktopFolder(
            out IntPtr ppshf);

        // Retrieves ppidl of special folder
        [DllImport(ExternDll.Shell32, 
            EntryPoint = "SHGetSpecialFolderLocation", 
            ExactSpelling = true, 
            CharSet = CharSet.Ansi, 
            SetLastError = true)]
        public static extern int SHGetSpecialFolderLocation(
            IntPtr hwndOwner, 
            CSIDL nFolder, 
            out IntPtr ppidl);

        // This function takes the fully-qualified pointer to an item
        // identifier list(PIDL) of a namespace object, and returns a specified
        // interface pointer on the parent object.
        [DllImport(ExternDll.Shell32)]
        public static extern int SHBindToParent(
            IntPtr pidl,            
            ref Guid riid,
            out IntPtr ppv,
            out IntPtr ppidlLast);

        // Registers a window that receives notifications from the file system or shell
        [DllImport(ExternDll.Shell32, EntryPoint = "#2", CharSet = CharSet.Auto)]
        public static extern uint SHChangeNotifyRegister(
            IntPtr hwnd,
            SHCNRF fSources,
            SHCNE fEvents,
            WM wMsg,
            int cEntries,
            [MarshalAs(UnmanagedType.LPArray)]
            SHChangeNotifyEntry[] pfsne);

        // Unregisters the client's window process from receiving SHChangeNotify
        [DllImport(ExternDll.Shell32, EntryPoint = "#4", CharSet = CharSet.Auto)]
        public static extern bool SHChangeNotifyDeregister(
            uint hNotify);

        // Converts an item identifier list to a file system path
        // [DllImport("shell32.dll")]
        // public static extern bool SHGetPathFromIDList(
        // IntPtr pidl,
        // StringBuilder pszPath);
        // SHGetRealIDL converts a simple PIDL to a full PIDL
        // [DllImport("shell32.dll")]
        // public static extern int SHGetRealIDL(
        // IShellFolder psf,
        // IntPtr pidlSimple,
        // out IntPtr ppidlReal);
        // Tests whether two ITEMIDLIST structures are equal in a binary comparison
        [DllImport(ExternDll.Shell32,
            EntryPoint = "ILIsEqual", 
            ExactSpelling = true, 
            CharSet = CharSet.Ansi, 
            SetLastError = true)]
        public static extern bool ILIsEqual(
            IntPtr pidl1,
            IntPtr pidl2);

        // Takes a STRRET structure returned by IShellFolder::GetDisplayNameOf,
        // converts it to a string, and places the result in a buffer. 
        // [DllImport("shlwapi.dll", 
        // EntryPoint = "StrRetToBuf", 
        // ExactSpelling = false, 
        // CharSet = CharSet.Auto, 
        // SetLastError = true)]
        // public static extern int StrRetToBuf(
        // IntPtr pstr, 
        // IntPtr pidl,
        // StringBuilder pszBuf, 
        // int cchBuf);

        // Sends the specified message to a window or windows
        [DllImport(ExternDll.User32, 
            EntryPoint = "SendMessage", 
            ExactSpelling = false, 
            CharSet = CharSet.Auto, 
            SetLastError = true)]
        public static extern IntPtr SendMessage(
            IntPtr hWnd, 
            WM wMsg, 
            int wParam, 
            IntPtr lParam);

        // Destroys an icon and frees any memory the icon occupied
        [DllImport(ExternDll.User32,
            EntryPoint = "DestroyIcon",
            ExactSpelling = true,
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern bool DestroyIcon(
            IntPtr hIcon);

        // Displays a shortcut menu at the specified location and 
        // tracks the selection of items on the shortcut menu
        [DllImport(ExternDll.User32, 
            ExactSpelling = true,
            CharSet = CharSet.Auto)]
        public static extern uint TrackPopupMenuEx(
            IntPtr hmenu, 
            TPM flags,
            int x, 
            int y, 
            IntPtr hwnd,
            IntPtr lptpm);

        // Creates a popup-menu. The menu is initially empty, but it can be filled with 
        // menu items by using the InsertMenuItem, AppendMenu, and InsertMenu functions
        [DllImport(ExternDll.User32,
            SetLastError = true, 
            CharSet = CharSet.Auto)]
        public static extern IntPtr CreatePopupMenu();

        // Destroys the specified menu and frees any memory that the menu occupies
        [DllImport(ExternDll.User32,
            SetLastError = true,
            CharSet = CharSet.Auto)]
        public static extern bool DestroyMenu(
            IntPtr hMenu);

        // appends a new item to the end of the specified menu bar, drop-down menu, submenu, 
        // or shortcut menu. You can use this function to specify the content, appearance, and 
        // behavior of the menu item
        // [DllImport("user32",
        // SetLastError = true,
        // CharSet = CharSet.Auto)]
        // public static extern bool AppendMenu(
        // IntPtr hMenu,
        // MFT uFlags,
        // uint uIDNewItem,
        // [MarshalAs(UnmanagedType.LPTStr)]
        // string lpNewItem);
        // Inserts a new menu item into a menu, moving other items down the menu
        // [DllImport("user32", 
        // SetLastError = true, 
        // CharSet = CharSet.Auto)]
        // public static extern bool InsertMenu(
        // IntPtr hmenu, 
        // uint uPosition, 
        // MFT uflags,
        // uint uIDNewItem, 
        // [MarshalAs(UnmanagedType.LPTStr)]
        // string lpNewItem);
        // Inserts a new menu item at the specified position in a menu
        [DllImport(ExternDll.User32,
            SetLastError = true,
            CharSet = CharSet.Auto)]
        public static extern bool InsertMenuItem(
            IntPtr hMenu,
            uint uItem,
            bool fByPosition,
            ref MENUITEMINFO lpmii);

        // Deletes a menu item or detaches a submenu from the specified menu
        [DllImport(ExternDll.User32,
            SetLastError = true,
            CharSet = CharSet.Auto)]
        public static extern bool RemoveMenu(
            IntPtr hMenu,
            uint uPosition,
            MFT uFlags);

        // Retrieves information about a menu item
        [DllImport(ExternDll.User32,
            SetLastError = true,
            CharSet = CharSet.Auto)]
        public static extern bool GetMenuItemInfo(
            IntPtr hMenu,
            uint uItem,
            bool fByPos,
            ref MENUITEMINFO lpmii);

        // Changes information about a menu item.
        [DllImport(ExternDll.User32,
            SetLastError = true,
            CharSet = CharSet.Auto)]
        public static extern bool SetMenuItemInfo(
            IntPtr hMenu,
            uint uItem,
            bool fByPos,
            ref MENUITEMINFO lpmii);

        // Determines the default menu item on the specified menu
        [DllImport(ExternDll.User32,
            SetLastError = true,
            CharSet = CharSet.Auto)]
        public static extern int GetMenuDefaultItem(
            IntPtr hMenu,
            bool fByPos,
            uint gmdiFlags);

        // Sets the default menu item for the specified menu
        [DllImport(ExternDll.User32,
            SetLastError = true,
            CharSet = CharSet.Auto)]
        public static extern bool SetMenuDefaultItem(
            IntPtr hMenu,
            uint uItem,
            bool fByPos);

        // Retrieves a handle to the drop-down menu or submenu activated by the specified menu item
        [DllImport(ExternDll.User32,
            SetLastError = true,
            CharSet = CharSet.Auto)]
        public static extern IntPtr GetSubMenu(
            IntPtr hMenu,
            int nPos);

        // Replaces an image with an icon or cursor
        [DllImport(ExternDll.Comctl32, 
            EntryPoint = "ImageList_ReplaceIcon", 
            ExactSpelling = false, 
            CharSet = CharSet.Auto, 
            SetLastError = true)]
        public static extern int ImageList_ReplaceIcon(
            IntPtr himl, 
            int index, 
            IntPtr hicon);

        // Adds an image or images to an image list
        [DllImport(ExternDll.Comctl32,
            EntryPoint = "ImageList_Add",
            ExactSpelling = false,
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern int ImageList_Add(
            IntPtr himl,
            IntPtr hbmImage,
            IntPtr hbmMask);

        // Creates an icon from an image and mask in an image list
        [DllImport(ExternDll.Comctl32, 
            EntryPoint = "ImageList_GetIcon", 
            ExactSpelling = true, 
            CharSet = CharSet.Ansi, 
            SetLastError = true)]
        public static extern IntPtr ImageList_GetIcon(
            IntPtr himl, 
            int index, 
            ILD flags);

        // Registers the specified window as one that can be the target of an OLE drag-and-drop 
        // operation and specifies the IDropTarget instance to use for drop operations
        // [DllImport("ole32.dll", 

        // CharSet = CharSet.Auto, 

        // SetLastError = true)]

        // public static extern int RegisterDragDrop(

        // IntPtr hWnd, 

        // ShellDll.IDropTarget IdropTgt);

        // Revokes the registration of the specified application window as a potential target for 
        // OLE drag-and-drop operations
        [DllImport(ExternDll.Ole32, 
            CharSet = CharSet.Auto, 
            SetLastError = true)]
        public static extern int RevokeDragDrop(
            IntPtr hWnd);

        // This function frees the specified storage medium
        [DllImport(ExternDll.Ole32, 
            CharSet = CharSet.Auto, 
            SetLastError = true)]
        public static extern void ReleaseStgMedium(
            ref STGMEDIUM pmedium);

        // Carries out an OLE drag and drop operation
        // [DllImport("ole32.dll",
        // CharSet = CharSet.Auto,
        // SetLastError = true)]
        // public static extern int DoDragDrop(
        // IntPtr pDataObject,
        // [MarshalAs(UnmanagedType.Interface)]
        // IDropSource pDropSource,
        // DragDropEffects dwOKEffect,
        // out DragDropEffects pdwEffect);
        // Retrieves a drag/drop helper interface for drawing the drag/drop images
        [DllImport(ExternDll.Ole32,
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern int CoCreateInstance(
            ref Guid rclsid,
            IntPtr pUnkOuter,
            CLSCTX dwClsContext,
            ref Guid riid,
            out IntPtr ppv);

        // Retrieves a data object that you can use to access the contents of the clipboard
        [DllImport(ExternDll.Ole32,
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern int OleGetClipboard(
            out IntPtr ppDataObj);

        public static DateTime FileTimeToDateTime(FILETIME fileTime)
        {
            var ticks = (((long)fileTime.dwHighDateTime) << 32) + fileTime.dwLowDateTime;
            return DateTime.FromFileTimeUtc(ticks);
        }
    }
}