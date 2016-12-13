using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace LanExchange.Plugin.Windows.Utils
{
    /// <summary>
    /// "Stand-alone" shell context menu
    /// /// It isn't really debugged but is mostly working.

    /// Create an instance and call ShowContextMenu with a list of FileInfo for the files.
    /// Limitation is that it only handles files in the same directory but it can be fixed
    /// by changing the way files are translated into PIDLs.
    /// /// Based on FileBrowser in C# from CodeProject

    /// http:// www.codeproject.com/useritems/FileBrowser.asp

    /// /// Hooking class taken from MSDN Magazine Cutting Edge column

    /// http:// msdn.microsoft.com/msdnmag/issues/02/10/CuttingEdge/

    /// /// Andreas Johansson

    /// afjohansson@hotmail.com
    /// http:// afjohansson.spaces.live.com

    /// </summary>
    /// <example>
    /// ShellContextMenu scm = new ShellContextMenu();

    /// FileInfo[] files = new FileInfo[1];

    /// files[0] = new FileInfo(@"c:\windows\notepad.exe");

    /// scm.ShowContextMenu(this.Handle, files, Cursor.Position);

    /// </example>
    [Localizable(false)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ShellContextMenu
    {
        #region Destructor
        /// <summary>Ensure all resources get released</summary>
        ~ShellContextMenu()
        {
            ReleaseAll();
        }
        #endregion

        #region GetContextMenuInterfaces()
        /// <summary>Gets the interfaces to the context menu</summary>
        /// <param name="oParentFolder">Parent folder</param>
        /// <param name="arrayOfPIDL">PIDLs</param>
        /// <returns>true if it got the interfaces, otherwise false</returns>
        private bool GetContextMenuInterfaces(IShellFolder oParentFolder, IntPtr[] arrayOfPIDL)
        {
            IntPtr pUnknownContextMenu;

            int nResult = oParentFolder.GetUIObjectOf(
                IntPtr.Zero,
                (uint)arrayOfPIDL.Length,
                arrayOfPIDL,
                ref IID_IContextMenu,
                IntPtr.Zero,
                out pUnknownContextMenu);

            if (S_OK == nResult)
            {
                _oContextMenu = (IContextMenu)Marshal.GetTypedObjectForIUnknown(pUnknownContextMenu, typeof(IContextMenu));

                IntPtr pUnknownContextMenu2;
                if (S_OK == Marshal.QueryInterface(pUnknownContextMenu, ref IID_IContextMenu2, out pUnknownContextMenu2))
                {
                    _oContextMenu2 = (IContextMenu2)Marshal.GetTypedObjectForIUnknown(pUnknownContextMenu2, typeof(IContextMenu2));
                }
                IntPtr pUnknownContextMenu3;
                if (S_OK == Marshal.QueryInterface(pUnknownContextMenu, ref IID_IContextMenu3, out pUnknownContextMenu3))
                {
                    _oContextMenu3 = (IContextMenu3)Marshal.GetTypedObjectForIUnknown(pUnknownContextMenu3, typeof(IContextMenu3));
                }

                return true;
            }
            return false;
        }
        #endregion

        #region InvokeCommand
        private static void InvokeCommand(IContextMenu oContextMenu, uint nCmd, string strFolder, Point pointInvoke,
            bool control, bool shift)
        {
            var invoke = new CMINVOKECOMMANDINFOEX();
            invoke.cbSize = cbInvokeCommand;
            invoke.lpVerb = (IntPtr)(nCmd - CMD_FIRST);
            invoke.lpDirectory = strFolder;
            invoke.lpVerbW = (IntPtr)(nCmd - CMD_FIRST);
            invoke.lpDirectoryW = strFolder;
            invoke.fMask = CMIC.UNICODE | CMIC.PTINVOKE |
                (control ? CMIC.CONTROL_DOWN : 0) |
                (shift ? CMIC.SHIFT_DOWN : 0);
            invoke.ptInvoke = new ShellAPI.POINT(pointInvoke.X, pointInvoke.Y);
            invoke.nShow = SW.SHOWNORMAL;
            
            oContextMenu.InvokeCommand(ref invoke);
        }
        #endregion

        #region ReleaseAll()
        /// <summary>
        /// Release all allocated interfaces, PIDLs 
        /// </summary>
        private void ReleaseAll()
        {
            if (null != _oContextMenu)
            {
                Marshal.ReleaseComObject(_oContextMenu);
                _oContextMenu = null;
            }
            if (null != _oContextMenu2)
            {
                Marshal.ReleaseComObject(_oContextMenu2);
                _oContextMenu2 = null;
            }
            if (null != _oContextMenu3)
            {
                Marshal.ReleaseComObject(_oContextMenu3);
                _oContextMenu3 = null;
            }
            if (null != _oDesktopFolder)
            {
                Marshal.ReleaseComObject(_oDesktopFolder);
                _oDesktopFolder = null;
            }
            if (null != _oParentFolder)
            {
                Marshal.ReleaseComObject(_oParentFolder);
                _oParentFolder = null;
            }
            if (null != _arrPIDLs)
            {
                FreePIDL(_arrPIDLs);
                _arrPIDLs = null;
            }
        }
        #endregion

        #region GetDesktopFolder()
        /// <summary>
        /// Gets the desktop folder
        /// </summary>
        /// <returns>IShellFolder for desktop folder</returns>
        private IShellFolder GetDesktopFolder()
        {
            if (null != _oDesktopFolder) return _oDesktopFolder;
            // Get desktop IShellFolder
            IntPtr pUnkownDesktopFolder;
            int nResult = SHGetDesktopFolder(out pUnkownDesktopFolder);
            if (S_OK != nResult)
            {
                throw new ShellContextMenuException("Failed to get the desktop shell folder");
            }
            _oDesktopFolder = (IShellFolder)Marshal.GetTypedObjectForIUnknown(pUnkownDesktopFolder, typeof(IShellFolder));

            return _oDesktopFolder;
        }
        #endregion

        #region GetParentFolder()
        /// <summary>
        /// Gets the parent folder
        /// </summary>
        /// <param name="folderName">Folder path</param>
        /// <returns>IShellFolder for the folder(relative from the desktop)</returns>
        private IShellFolder GetParentFolder(string folderName)
        {
            if (null == _oParentFolder)
            {
                IShellFolder oDesktopFolder = GetDesktopFolder();
                if (null == oDesktopFolder)
                {
                    return null;
                }

                // Get the PIDL for the folder file is in
                IntPtr pPIDL;
                uint pchEaten = 0;
                ShellAPI.SFGAO pdwAttributes = 0;
                int nResult = oDesktopFolder.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, folderName, ref pchEaten, out pPIDL, ref pdwAttributes);
                if (S_OK != nResult)
                {
                    return null;
                }

                IntPtr pStrRet = Marshal.AllocCoTaskMem(MAX_PATH * 2 + 4);
                Marshal.WriteInt32(pStrRet, 0, 0);
                _oDesktopFolder.GetDisplayNameOf(pPIDL, SHGNO.FORPARSING, pStrRet);
                var strFolder = new StringBuilder(MAX_PATH);
                StrRetToBuf(pStrRet, pPIDL, strFolder, MAX_PATH);
                Marshal.FreeCoTaskMem(pStrRet);
                _strParentFolder = strFolder.ToString();

                // Get the IShellFolder for folder
                IntPtr pUnknownParentFolder;
                nResult = oDesktopFolder.BindToObject(pPIDL, IntPtr.Zero, ref IID_IShellFolder, out pUnknownParentFolder);
                // Free the PIDL first
                Marshal.FreeCoTaskMem(pPIDL);
                if (S_OK != nResult)
                {
                    return null;
                }
                _oParentFolder = (IShellFolder)Marshal.GetTypedObjectForIUnknown(pUnknownParentFolder, typeof(IShellFolder));
            }

            return _oParentFolder;
        }
        #endregion

        #region GetPIDL()
        /// <summary>
        /// Get the PIDLs
        /// </summary>
        /// <param name="arrayOfFileInfo">Array of FileInfo</param>
        /// <returns>Array of PIDLs</returns>
        protected IntPtr[] GetPIDL(FileInfo[] arrayOfFileInfo)
        {
            if (null == arrayOfFileInfo || 0 == arrayOfFileInfo.Length)
            {
                return null;
            }

            IShellFolder oParentFolder = GetParentFolder(arrayOfFileInfo[0].DirectoryName);
            if (null == oParentFolder)
            {
                return null;
            }

            var arrPIDL = new IntPtr[arrayOfFileInfo.Length];
            int n = 0;
            foreach (FileInfo fi in arrayOfFileInfo)
            {
                // Get the file relative to folder
                uint pchEaten = 0;
                ShellAPI.SFGAO pdwAttributes = 0;
                IntPtr pPIDL;
                int nResult = oParentFolder.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, fi.Name, ref pchEaten, out pPIDL, ref pdwAttributes);
                if (S_OK != nResult)
                {
                    FreePIDL(arrPIDL);
                    return null;
                }
                arrPIDL[n] = pPIDL;
                n++;
            }

            return arrPIDL;
        }
        #endregion

        #region FreePIDL()
        /// <summary>
        /// Free the PIDLs
        /// </summary>
        /// <param name="arrPIDL">Array of PIDLs(IntPtr)</param>
        protected void FreePIDL(IntPtr[] arrPIDL)
        {
            if (null != arrPIDL)
            {
                for (int n = 0; n < arrPIDL.Length; n++)
                {
                    if (arrPIDL[n] != IntPtr.Zero)
                    {
                        Marshal.FreeCoTaskMem(arrPIDL[n]);
                        arrPIDL[n] = IntPtr.Zero;
                    }
                }
            }
        }
        #endregion

        #region InvokeContextMenuDefault
        // private void InvokeContextMenuDefault(FileInfo[] arrayOfFileInfo)

        // {

        // // Release all resources first.

        // ReleaseAll();


        // IntPtr pMenu = IntPtr.Zero;


        // try

        // {

        // _arrPIDLs = GetPIDL(arrayOfFileInfo);

        // if (null == _arrPIDLs)

        // {

        // ReleaseAll();

        // return;

        // }


        // if (false == GetContextMenuInterfaces(_oParentFolder, _arrPIDLs))

        // {

        // ReleaseAll();

        // return;

        // }


        // pMenu = CreatePopupMenu();


        // int nResult = _oContextMenu.QueryContextMenu(

        // pMenu,

        // 0,

        // CMD_FIRST,

        // CMD_LAST,

        // CMF.DEFAULTONLY |

        // ((Control.ModifierKeys & Keys.Shift) != 0 ? CMF.EXTENDEDVERBS : 0));


        // uint nDefaultCmd = (uint)GetMenuDefaultItem(pMenu, false, 0);

        // if (nDefaultCmd >= CMD_FIRST)

        // {

        // InvokeCommand(_oContextMenu, nDefaultCmd, arrayOfFileInfo[0].DirectoryName, Control.MousePosition);

        // }


        // DestroyMenu(pMenu);

        // pMenu = IntPtr.Zero;

        // }

        // catch

        // {

        // throw;

        // }

        // finally

        // {

        // if (pMenu != IntPtr.Zero)

        // {

        // DestroyMenu(pMenu);

        // }

        // ReleaseAll();

        // }

        // }

        #endregion

        #region ShowContextMenu()
        /// <summary>
        /// Shows the context menu
        /// </summary>
        /// <param name="handleOwner">Window that will get messages</param>
        /// <param name="arrFileInfo">FileInfos (should all be in same directory)</param>
        /// <param name="pointScreen">Where to show the menu</param>
        public void ShowContextMenu(IntPtr handleOwner, FileInfo[] arrFileInfo, Point pointScreen, bool control, bool shift)
        {
            // Release all resources first.
            ReleaseAll();

            IntPtr pMenu = IntPtr.Zero;
            var hook = new LocalWindowsHook(HookType.WH_CALLWNDPROC);
            hook.HookInvoked += WindowsHookInvoked;

            try
            {
                // Application.AddMessageFilter(this);


                _arrPIDLs = GetPIDL(arrFileInfo);
                if (null == _arrPIDLs)
                {
                    ReleaseAll();
                    return;
                }

                if (false == GetContextMenuInterfaces(_oParentFolder, _arrPIDLs))
                {
                    ReleaseAll();
                    return;
                }

                pMenu = CreatePopupMenu();

                _oContextMenu.QueryContextMenu(
                    pMenu,
                    0,
                    CMD_FIRST,
                    CMD_LAST,
                    CMF.EXPLORE |
                    CMF.NORMAL |
                    (shift ? CMF.EXTENDEDVERBS : 0));

                hook.Install();

                uint nSelected = TrackPopupMenuEx(
                    pMenu,
                    TPM.RETURNCMD,
                    pointScreen.X,
                    pointScreen.Y,
                    handleOwner,
                    IntPtr.Zero);

                DestroyMenu(pMenu);
                pMenu = IntPtr.Zero;

                if (nSelected != 0)
                {
                    InvokeCommand(_oContextMenu, nSelected, _strParentFolder, pointScreen, control, shift);
                }
            }
            finally
            {
                hook.Uninstall();
                if (pMenu != IntPtr.Zero)
                {
                    DestroyMenu(pMenu);
                }
                ReleaseAll();
            }
        }
        #endregion

        #region ShowContextMenuForCSIDL
        /// <summary>
        /// Shows the context menu
        /// </summary>
        /// <param name="handleOwner">Window that will get messages</param>
        /// <param name="csidl">CSIDL value for special folder.</param>
        /// <param name="pointScreen">Where to show the menu</param>
        [CLSCompliant(false)]
        public void ShowContextMenuForCSIDL(IntPtr handleOwner, ShellAPI.CSIDL csidl, Point pointScreen, 
            bool control, bool shift)
        {
            // Release all resources first.
            ReleaseAll();

            IntPtr pMenu = IntPtr.Zero;
            var hook = new LocalWindowsHook(HookType.WH_CALLWNDPROC);
            hook.HookInvoked += WindowsHookInvoked;

            try
            {
                IntPtr tempPidl;
                ShellAPI.SHGetSpecialFolderLocation(IntPtr.Zero, csidl, out tempPidl);
                _arrPIDLs = new[] { tempPidl };

               _oParentFolder = GetDesktopFolder();

                if (false == GetContextMenuInterfaces(_oParentFolder, _arrPIDLs))
                {
                    ReleaseAll();
                    return;
                }

                pMenu = CreatePopupMenu();

                _oContextMenu.QueryContextMenu(
                    pMenu,
                    0,
                    CMD_FIRST,
                    CMD_LAST,
                    CMF.EXPLORE |
                    CMF.NORMAL |
                    (shift ? CMF.EXTENDEDVERBS : 0));

                hook.Install();

                uint nSelected = TrackPopupMenuEx(
                    pMenu,
                    TPM.RETURNCMD,
                    pointScreen.X,
                    pointScreen.Y,
                    handleOwner,
                    IntPtr.Zero);

                DestroyMenu(pMenu);
                pMenu = IntPtr.Zero;

                if (nSelected != 0)
                {
                    InvokeCommand(_oContextMenu, nSelected, null, pointScreen, control, shift);
                }
            }
            finally
            {
                hook.Uninstall();
                if (pMenu != IntPtr.Zero)
                {
                    DestroyMenu(pMenu);
                }
                ReleaseAll();
            }
        }
        #endregion


        #region WindowsHookInvoked()
        /// <summary>
        /// Handle messages for context menu
        /// </summary>
        private void WindowsHookInvoked(object sender, HookEventArgs e)
        {
            var cwp = (CWPSTRUCT)Marshal.PtrToStructure(e.LParam, typeof(CWPSTRUCT));
            
            if (_oContextMenu2 != null &&
                (cwp.message == (int) WM.INITMENUPOPUP ||
                 cwp.message == (int)WM.MEASUREITEM ||
                 cwp.message == (int)WM.DRAWITEM))
            {
                if (_oContextMenu2.HandleMenuMsg((uint)cwp.message, cwp.wparam, cwp.lparam) == S_OK)
                {
                    return;
                }
            }

            if (_oContextMenu3 != null && cwp.message == (int) WM.MENUCHAR)
            {
                if (_oContextMenu3.HandleMenuMsg2((uint)cwp.message, cwp.wparam, cwp.lparam, IntPtr.Zero) == S_OK)
                {
                }
            }
        }
        #endregion

        #region Local variabled
        private IContextMenu _oContextMenu;
        private IContextMenu2 _oContextMenu2;
        private IContextMenu3 _oContextMenu3;
        private IShellFolder _oDesktopFolder;
        private IShellFolder _oParentFolder;
        private IntPtr[] _arrPIDLs;
        private string _strParentFolder;
        #endregion

        #region Variables and Constants

        private const int MAX_PATH = 260;
        private const uint CMD_FIRST = 1;
        private const uint CMD_LAST = 30000;

        private const int S_OK = 0;

        private static readonly int cbInvokeCommand = Marshal.SizeOf(typeof(CMINVOKECOMMANDINFOEX));

        #endregion

        #region DLL Import

        // Retrieves the IShellFolder interface for the desktop folder, which is the root of the Shell's namespace.
        [DllImport(ExternDll.Shell32)]
        private static extern Int32 SHGetDesktopFolder(out IntPtr ppshf);

        // Takes a STRRET structure returned by IShellFolder::GetDisplayNameOf, converts it to a string, and places the result in a buffer. 
        [DllImport(ExternDll.SHLWAPI, EntryPoint = "StrRetToBuf", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Int32 StrRetToBuf(IntPtr pstr, IntPtr pidl, StringBuilder pszBuf, int cchBuf);

        // The TrackPopupMenuEx function displays a shortcut menu at the specified location and tracks the selection of items on the shortcut menu. The shortcut menu can appear anywhere on the screen.
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern uint TrackPopupMenuEx(IntPtr hmenu, TPM flags, int x, int y, IntPtr hwnd, IntPtr lptpm);

        // The CreatePopupMenu function creates a drop-down menu, submenu, or shortcut menu. The menu is initially empty. You can insert or append menu items by using the InsertMenuItem function. You can also use the InsertMenu function to insert menu items and the AppendMenu function to append menu items.
        [DllImport(ExternDll.User32, SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr CreatePopupMenu();

        // The DestroyMenu function destroys the specified menu and frees any memory that the menu occupies.
        [DllImport(ExternDll.User32, SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool DestroyMenu(IntPtr hMenu);

        // Determines the default menu item on the specified menu
        // [DllImport(ExternDll.User32, SetLastError = true, CharSet = CharSet.Auto)]

        // private static extern int GetMenuDefaultItem(IntPtr hMenu, bool fByPos, uint gmdiFlags);


        #endregion

        #region Shell GUIDs

        private static Guid IID_IShellFolder = new Guid("{000214E6 - 0000-0000-C000 - 000000000046}");
        private static Guid IID_IContextMenu = new Guid("{000214e4 - 0000-0000-c000 - 000000000046}");
        private static Guid IID_IContextMenu2 = new Guid("{000214f4 - 0000-0000-c000 - 000000000046}");
        private static Guid IID_IContextMenu3 = new Guid("{bcfce0a0-ec17 - 11d0 - 8d10 - 00a0c90f2719}");

        #endregion

        #region Structs

        [StructLayout(LayoutKind.Sequential)]
        private struct CWPSTRUCT
        {
            public readonly IntPtr lparam;
            public readonly IntPtr wparam;
            public readonly int message;
            private readonly IntPtr hwnd;
        }

        // Contains extended information about a shortcut menu command
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct CMINVOKECOMMANDINFOEX
        {
            public int cbSize;
            public CMIC fMask;
            private readonly IntPtr hwnd;
            public IntPtr lpVerb;
            [MarshalAs(UnmanagedType.LPStr)]
            private readonly string lpParameters;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpDirectory;
            public SW nShow;
            private readonly int dwHotKey;
            private readonly IntPtr hIcon;
            [MarshalAs(UnmanagedType.LPStr)]
            private readonly string lpTitle;
            public IntPtr lpVerbW;
            [MarshalAs(UnmanagedType.LPWStr)]
            private readonly string lpParametersW;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpDirectoryW;
            [MarshalAs(UnmanagedType.LPWStr)]
            private readonly string lpTitleW;
            public ShellAPI.POINT ptInvoke;
        }

        // A generalized global memory handle used for data transfer operations by the 
        // IAdviseSink, IDataObject, and IOleCache interfaces

        #endregion

        #region Enums

        // Defines the values used with the IShellFolder::GetDisplayNameOf and IShellFolder::SetNameOf 
        // methods to specify the type of file or folder names used by those methods
        [Flags]
        private enum SHGNO
        {
            FORPARSING = 0x8000
        }

        // Specifies how the shortcut menu can be changed when calling IContextMenu::QueryContextMenu
        [Flags]
        private enum CMF : uint
        {
            NORMAL = 0x00000000,
            // DEFAULTONLY = 0x00000001,

            // VERBSONLY = 0x00000002,

            EXPLORE = 0x00000004,
            // NOVERBS = 0x00000008,

            // CANRENAME = 0x00000010,

            // NODEFAULT = 0x00000020,

            // INCLUDESTATIC = 0x00000040,

            EXTENDEDVERBS = 0x00000100,
            // RESERVED = 0xffff0000

        }

        // Specifies how TrackPopupMenuEx positions the shortcut menu horizontally
        [Flags]
        private enum TPM : uint
        {
            // LEFTBUTTON = 0x0000,

            // RIGHTBUTTON = 0x0002,

            // LEFTALIGN = 0x0000,

            // CENTERALIGN = 0x0004,

            // RIGHTALIGN = 0x0008,

            // TOPALIGN = 0x0000,

            // VCENTERALIGN = 0x0010,

            // BOTTOMALIGN = 0x0020,

            // HORIZONTAL = 0x0000,

            // VERTICAL = 0x0040,

            // NONOTIFY = 0x0080,

            RETURNCMD = 0x0100,
            // RECURSE = 0x0001,

            // HORPOSANIMATION = 0x0400,

            // HORNEGANIMATION = 0x0800,

            // VERPOSANIMATION = 0x1000,

            // VERNEGANIMATION = 0x2000,

            // NOANIMATION = 0x4000,

            // LAYOUTRTL = 0x8000

        }

        // Flags used with the CMINVOKECOMMANDINFOEX structure
        [Flags]
        private enum CMIC : uint
        {
            // HOTKEY = 0x00000020,

            // ICON = 0x00000010,

            // FLAG_NO_UI = 0x00000400,

            UNICODE = 0x00004000,
            // NO_CONSOLE = 0x00008000,

            // ASYNCOK = 0x00100000,

            // NOZONECHECKS = 0x00800000,

            SHIFT_DOWN = 0x10000000,
            CONTROL_DOWN = 0x40000000,
            // FLAG_LOG_USAGE = 0x04000000,

            PTINVOKE = 0x20000000
        }

        // Specifies how the window is to be shown
        [Flags]
        private enum SW
        {
            // HIDE = 0,

            SHOWNORMAL = 1,
            // NORMAL = 1,

            // SHOWMINIMIZED = 2,

            // SHOWMAXIMIZED = 3,

            // MAXIMIZE = 3,

            // SHOWNOACTIVATE = 4,

            // SHOW = 5,

            // MINIMIZE = 6,

            // SHOWMINNOACTIVE = 7,

            // SHOWNA = 8,

            // RESTORE = 9,

            // SHOWDEFAULT = 10,

        }

        // Window message flags
        [Flags]
        private enum WM : uint
        {
            // ACTIVATE = 0x6,

            // ACTIVATEAPP = 0x1C,

            // AFXFIRST = 0x360,

            // AFXLAST = 0x37F,

            // APP = 0x8000,

            // ASKCBFORMATNAME = 0x30C,

            // CANCELJOURNAL = 0x4B,

            // CANCELMODE = 0x1F,

            // CAPTURECHANGED = 0x215,

            // CHANGECBCHAIN = 0x30D,

            // CHAR = 0x102,

            // CHARTOITEM = 0x2F,

            // CHILDACTIVATE = 0x22,

            // CLEAR = 0x303,

            // CLOSE = 0x10,

            // COMMAND = 0x111,

            // COMPACTING = 0x41,

            // COMPAREITEM = 0x39,

            // CONTEXTMENU = 0x7B,

            // COPY = 0x301,

            // COPYDATA = 0x4A,

            // CREATE = 0x1,

            // CTLCOLORBTN = 0x135,

            // CTLCOLORDLG = 0x136,

            // CTLCOLOREDIT = 0x133,

            // CTLCOLORLISTBOX = 0x134,

            // CTLCOLORMSGBOX = 0x132,

            // CTLCOLORSCROLLBAR = 0x137,

            // CTLCOLORSTATIC = 0x138,

            // CUT = 0x300,

            // DEADCHAR = 0x103,

            // DELETEITEM = 0x2D,

            // DESTROY = 0x2,

            // DESTROYCLIPBOARD = 0x307,

            // DEVICECHANGE = 0x219,

            // DEVMODECHANGE = 0x1B,

            // DISPLAYCHANGE = 0x7E,

            // DRAWCLIPBOARD = 0x308,

            DRAWITEM = 0x2B,
            // DROPFILES = 0x233,

            // ENABLE = 0xA,

            // ENDSESSION = 0x16,

            // ENTERIDLE = 0x121,

            // ENTERMENULOOP = 0x211,

            // ENTERSIZEMOVE = 0x231,

            // ERASEBKGND = 0x14,

            // EXITMENULOOP = 0x212,

            // EXITSIZEMOVE = 0x232,

            // FONTCHANGE = 0x1D,

            // GETDLGCODE = 0x87,

            // GETFONT = 0x31,

            // GETHOTKEY = 0x33,

            // GETICON = 0x7F,

            // GETMINMAXINFO = 0x24,

            // GETOBJECT = 0x3D,

            // GETSYSMENU = 0x313,

            // GETTEXT = 0xD,

            // GETTEXTLENGTH = 0xE,

            // HANDHELDFIRST = 0x358,

            // HANDHELDLAST = 0x35F,

            // HELP = 0x53,

            // HOTKEY = 0x312,

            // HSCROLL = 0x114,

            // HSCROLLCLIPBOARD = 0x30E,

            // ICONERASEBKGND = 0x27,

            // IME_CHAR = 0x286,

            // IME_COMPOSITION = 0x10F,

            // IME_COMPOSITIONFULL = 0x284,

            // IME_CONTROL = 0x283,

            // IME_ENDCOMPOSITION = 0x10E,

            // IME_KEYDOWN = 0x290,

            // IME_KEYLAST = 0x10F,

            // IME_KEYUP = 0x291,

            // IME_NOTIFY = 0x282,

            // IME_REQUEST = 0x288,

            // IME_SELECT = 0x285,

            // IME_SETCONTEXT = 0x281,

            // IME_STARTCOMPOSITION = 0x10D,

            // INITDIALOG = 0x110,

            // INITMENU = 0x116,

            INITMENUPOPUP = 0x117,
            // INPUTLANGCHANGE = 0x51,

            // INPUTLANGCHANGEREQUEST = 0x50,

            // KEYDOWN = 0x100,

            // KEYFIRST = 0x100,

            // KEYLAST = 0x108,

            // KEYUP = 0x101,

            // KILLFOCUS = 0x8,

            // LBUTTONDBLCLK = 0x203,

            // LBUTTONDOWN = 0x201,

            // LBUTTONUP = 0x202,

            // LVM_GETEDITCONTROL = 0x1018,

            // LVM_SETIMAGELIST = 0x1003,

            // MBUTTONDBLCLK = 0x209,

            // MBUTTONDOWN = 0x207,

            // MBUTTONUP = 0x208,

            // MDIACTIVATE = 0x222,

            // MDICASCADE = 0x227,

            // MDICREATE = 0x220,

            // MDIDESTROY = 0x221,

            // MDIGETACTIVE = 0x229,

            // MDIICONARRANGE = 0x228,

            // MDIMAXIMIZE = 0x225,

            // MDINEXT = 0x224,

            // MDIREFRESHMENU = 0x234,

            // MDIRESTORE = 0x223,

            // MDISETMENU = 0x230,

            // MDITILE = 0x226,

            MEASUREITEM = 0x2C,
            MENUCHAR = 0x120,
            // MENUCOMMAND = 0x126,

            // MENUDRAG = 0x123,

            // MENUGETOBJECT = 0x124,

            // MENURBUTTONUP = 0x122,

            // MENUSELECT = 0x11F,

            // MOUSEACTIVATE = 0x21,

            // MOUSEFIRST = 0x200,

            // MOUSEHOVER = 0x2A1,

            // MOUSELAST = 0x20A,

            // MOUSELEAVE = 0x2A3,

            // MOUSEMOVE = 0x200,

            // MOUSEWHEEL = 0x20A,

            // MOVE = 0x3,

            // MOVING = 0x216,

            // NCACTIVATE = 0x86,

            // NCCALCSIZE = 0x83,

            // NCCREATE = 0x81,

            // NCDESTROY = 0x82,

            // NCHITTEST = 0x84,

            // NCLBUTTONDBLCLK = 0xA3,

            // NCLBUTTONDOWN = 0xA1,

            // NCLBUTTONUP = 0xA2,

            // NCMBUTTONDBLCLK = 0xA9,

            // NCMBUTTONDOWN = 0xA7,

            // NCMBUTTONUP = 0xA8,

            // NCMOUSEHOVER = 0x2A0,

            // NCMOUSELEAVE = 0x2A2,

            // NCMOUSEMOVE = 0xA0,

            // NCPAINT = 0x85,

            // NCRBUTTONDBLCLK = 0xA6,

            // NCRBUTTONDOWN = 0xA4,

            // NCRBUTTONUP = 0xA5,

            // NEXTDLGCTL = 0x28,

            // NEXTMENU = 0x213,

            // NOTIFY = 0x4E,

            // NOTIFYFORMAT = 0x55,

            // NULL = 0x0,

            // PAINT = 0xF,

            // PAINTCLIPBOARD = 0x309,

            // PAINTICON = 0x26,

            // PALETTECHANGED = 0x311,

            // PALETTEISCHANGING = 0x310,

            // PARENTNOTIFY = 0x210,

            // PASTE = 0x302,

            // PENWINFIRST = 0x380,

            // PENWINLAST = 0x38F,

            // POWER = 0x48,

            // PRINT = 0x317,

            // PRINTCLIENT = 0x318,

            // QUERYDRAGICON = 0x37,

            // QUERYENDSESSION = 0x11,

            // QUERYNEWPALETTE = 0x30F,

            // QUERYOPEN = 0x13,

            // QUEUESYNC = 0x23,

            // QUIT = 0x12,

            // RBUTTONDBLCLK = 0x206,

            // RBUTTONDOWN = 0x204,

            // RBUTTONUP = 0x205,

            // RENDERALLFORMATS = 0x306,

            // RENDERFORMAT = 0x305,

            // SETCURSOR = 0x20,

            // SETFOCUS = 0x7,

            // SETFONT = 0x30,

            // SETHOTKEY = 0x32,

            // SETICON = 0x80,

            // SETMARGINS = 0xD3,

            // SETREDRAW = 0xB,

            // SETTEXT = 0xC,

            // SETTINGCHANGE = 0x1A,

            // SHOWWINDOW = 0x18,

            // SIZE = 0x5,

            // SIZECLIPBOARD = 0x30B,

            // SIZING = 0x214,

            // SPOOLERSTATUS = 0x2A,

            // STYLECHANGED = 0x7D,

            // STYLECHANGING = 0x7C,

            // SYNCPAINT = 0x88,

            // SYSCHAR = 0x106,

            // SYSCOLORCHANGE = 0x15,

            // SYSCOMMAND = 0x112,

            // SYSDEADCHAR = 0x107,

            // SYSKEYDOWN = 0x104,

            // SYSKEYUP = 0x105,

            // TCARD = 0x52,

            // TIMECHANGE = 0x1E,

            // TIMER = 0x113,

            // TVM_GETEDITCONTROL = 0x110F,

            // TVM_SETIMAGELIST = 0x1109,

            // UNDO = 0x304,

            // UNINITMENUPOPUP = 0x125,

            // USER = 0x400,

            // USERCHANGED = 0x54,

            // VKEYTOITEM = 0x2E,

            // VSCROLL = 0x115,

            // VSCROLLCLIPBOARD = 0x30A,

            // WINDOWPOSCHANGED = 0x47,

            // WINDOWPOSCHANGING = 0x46,

            // WININICHANGE = 0x1A,

            // SH_NOTIFY = 0x0401

        }

        #endregion

        #region IShellFolder
        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214E6-0000-0000-C000-000000000046")]
        private interface IShellFolder
        {
            // Translates a file object's or folder's display name into an item identifier list.
            // Return value: error code, if any
            [PreserveSig]
            Int32 ParseDisplayName(
                IntPtr hwnd,
                IntPtr pbc,
                [MarshalAs(UnmanagedType.LPWStr)] 
            string pszDisplayName,
                ref uint pchEaten,
                out IntPtr ppidl,
                ref ShellAPI.SFGAO pdwAttributes);

            // Allows a client to determine the contents of a folder by creating an item
            // identifier enumeration object and returning its IEnumIDList interface.
            // Return value: error code, if any
            [PreserveSig]
            Int32 EnumObjects(
                IntPtr hwnd,
                ShellAPI.SHCONTF grfFlags,
                out IntPtr enumIDList);

            // Retrieves an IShellFolder object for a subfolder.
            // Return value: error code, if any
            [PreserveSig]
            Int32 BindToObject(
                IntPtr pidl,
                IntPtr pbc,
                ref Guid riid,
                out IntPtr ppv);

            // Requests a pointer to an object's storage interface. 
            // Return value: error code, if any
            [PreserveSig]
            Int32 BindToStorage(
                IntPtr pidl,
                IntPtr pbc,
                ref Guid riid,
                out IntPtr ppv);

            // Determines the relative order of two file objects or folders, given their
            // item identifier lists. Return value: If this method is successful, the
            // CODE field of the HRESULT contains one of the following values(the code
            // can be retrived using the helper function GetHResultCode): Negative A
            // negative return value indicates that the first item should precede
            // the second(pidl1 < pidl2). 

            // Positive A positive return value indicates that the first item should
            // follow the second(pidl1 > pidl2).  Zero A return value of zero
            // indicates that the two items are the same(pidl1 = pidl2). 
            [PreserveSig]
            Int32 CompareIDs(
                IntPtr lParam,
                IntPtr pidl1,
                IntPtr pidl2);

            // Requests an object that can be used to obtain information from or interact
            // with a folder object.
            // Return value: error code, if any
            [PreserveSig]
            Int32 CreateViewObject(
                IntPtr hwndOwner,
                Guid riid,
                out IntPtr ppv);

            // Retrieves the attributes of one or more file objects or subfolders. 
            // Return value: error code, if any
            [PreserveSig]
            Int32 GetAttributesOf(
                uint cidl,
                [MarshalAs(UnmanagedType.LPArray)]
            IntPtr[] apidl,
                ref ShellAPI.SFGAO rgfInOut);

            // Retrieves an OLE interface that can be used to carry out actions on the
            // specified file objects or folders.
            // Return value: error code, if any
            [PreserveSig]
            Int32 GetUIObjectOf(
                IntPtr hwndOwner,
                uint cidl,
                [MarshalAs(UnmanagedType.LPArray)]
            IntPtr[] apidl,
                ref Guid riid,
                IntPtr rgfReserved,
                out IntPtr ppv);

            // Retrieves the display name for the specified file object or subfolder. 
            // Return value: error code, if any
            [PreserveSig]
            Int32 GetDisplayNameOf(
                IntPtr pidl,
                SHGNO uFlags,
                IntPtr lpName);

            // Sets the display name of a file object or subfolder, changing the item
            // identifier in the process.
            // Return value: error code, if any
            [PreserveSig]
            Int32 SetNameOf(
                IntPtr hwnd,
                IntPtr pidl,
                [MarshalAs(UnmanagedType.LPWStr)] 
            string pszName,
                SHGNO uFlags,
                out IntPtr ppidlOut);
        }
        #endregion

        #region IContextMenu
        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214e4-0000-0000-c000-000000000046")]
        private interface IContextMenu
        {
            // Adds commands to a shortcut menu
            [PreserveSig]
            Int32 QueryContextMenu(
                IntPtr hmenu,
                uint iMenu,
                uint idCmdFirst,
                uint idCmdLast,
                CMF uFlags);

            // Carries out the command associated with a shortcut menu item
            [PreserveSig]
            Int32 InvokeCommand(
                ref CMINVOKECOMMANDINFOEX info);

            // Retrieves information about a shortcut menu command, 
            // including the help string and the language-independent, 
            // or canonical, name for the command
            [PreserveSig]
            Int32 GetCommandString(
                uint idcmd,
                ShellAPI.GCS uflags,
                uint reserved,
                [MarshalAs(UnmanagedType.LPArray)]
            byte[] commandstring,
                int cch);
        }

        [ComImport, Guid("000214f4-0000-0000-c000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IContextMenu2
        {
            // Adds commands to a shortcut menu
            [PreserveSig]
            Int32 QueryContextMenu(
                IntPtr hmenu,
                uint iMenu,
                uint idCmdFirst,
                uint idCmdLast,
                CMF uFlags);

            // Carries out the command associated with a shortcut menu item
            [PreserveSig]
            Int32 InvokeCommand(
                ref CMINVOKECOMMANDINFOEX info);

            // Retrieves information about a shortcut menu command, 
            // including the help string and the language-independent, 
            // or canonical, name for the command
            [PreserveSig]
            Int32 GetCommandString(
                uint idcmd,
                ShellAPI.GCS uflags,
                uint reserved,
                [MarshalAs(UnmanagedType.LPWStr)]
            StringBuilder commandstring,
                int cch);

            // Allows client objects of the IContextMenu interface to 
            // handle messages associated with owner-drawn menu items
            [PreserveSig]
            Int32 HandleMenuMsg(
                uint uMsg,
                IntPtr wParam,
                IntPtr lParam);
        }

        [ComImport, Guid("bcfce0a0-ec17-11d0-8d10-00a0c90f2719")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IContextMenu3
        {
            // Adds commands to a shortcut menu
            [PreserveSig]
            Int32 QueryContextMenu(
                IntPtr hmenu,
                uint iMenu,
                uint idCmdFirst,
                uint idCmdLast,
                CMF uFlags);

            // Carries out the command associated with a shortcut menu item
            [PreserveSig]
            Int32 InvokeCommand(
                ref CMINVOKECOMMANDINFOEX info);

            // Retrieves information about a shortcut menu command, 
            // including the help string and the language-independent, 
            // or canonical, name for the command
            [PreserveSig]
            Int32 GetCommandString(
                uint idcmd,
                ShellAPI.GCS uflags,
                uint reserved,
                [MarshalAs(UnmanagedType.LPWStr)]
            StringBuilder commandstring,
                int cch);

            // Allows client objects of the IContextMenu interface to 
            // handle messages associated with owner-drawn menu items
            [PreserveSig]
            Int32 HandleMenuMsg(
                uint uMsg,
                IntPtr wParam,
                IntPtr lParam);

            // Allows client objects of the IContextMenu3 interface to 
            // handle messages associated with owner-drawn menu items
            [PreserveSig]
            Int32 HandleMenuMsg2(
                uint uMsg,
                IntPtr wParam,
                IntPtr lParam,
                IntPtr plResult);
        }
        #endregion

    }

    #region ShellContextMenuException

    #endregion


}
