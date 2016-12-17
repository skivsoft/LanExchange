using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using LanExchange.Plugin.Windows.Utils;

namespace LanExchange.Plugin.Windows.ContextMenu
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
        private const int MAX_PATH = 260;
        private const uint CMD_FIRST = 1;
        private const uint CMD_LAST = 30000;
        private const int S_OK = 0;

        private static readonly int cbInvokeCommand = Marshal.SizeOf(typeof(CMINVOKECOMMANDINFOEX));
        private static Guid IID_IShellFolder = new Guid("{000214E6 - 0000-0000-C000 - 000000000046}");
        private static Guid IID_IContextMenu = new Guid("{000214e4 - 0000-0000-c000 - 000000000046}");
        private static Guid IID_IContextMenu2 = new Guid("{000214f4 - 0000-0000-c000 - 000000000046}");
        private static Guid IID_IContextMenu3 = new Guid("{bcfce0a0-ec17 - 11d0 - 8d10 - 00a0c90f2719}");

        private IContextMenu _oContextMenu;
        private IContextMenu2 _oContextMenu2;
        private IContextMenu3 _oContextMenu3;
        private IShellFolder _oDesktopFolder;
        private IShellFolder _oParentFolder;
        private IntPtr[] _arrPIDLs;
        private string _strParentFolder;

        #region Destructor
        /// <summary>Ensure all resources get released</summary>
        ~ShellContextMenu()
        {
            ReleaseAll();
        }
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
                    CMF.EXPLORE | CMF.NORMAL | (shift ? CMF.EXTENDEDVERBS : 0));

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
        public void ShowContextMenuForCSIDL(
            IntPtr handleOwner,
            ShellAPI.CSIDL csidl,
            Point pointScreen,
            bool control,
            bool shift)
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
                    CMF.EXPLORE | CMF.NORMAL | (shift ? CMF.EXTENDEDVERBS : 0));

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

        #region InvokeCommand
        private static void InvokeCommand(
            IContextMenu oContextMenu,
            uint nCmd,
            string strFolder,
            Point pointInvoke,
            bool control,
            bool shift)
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

        #region DLL Import

        // Retrieves the IShellFolder interface for the desktop folder, which is the root of the Shell's namespace.
        [DllImport(ExternDll.Shell32)]
        private static extern int SHGetDesktopFolder(out IntPtr ppshf);

        // Takes a STRRET structure returned by IShellFolder::GetDisplayNameOf, converts it to a string, and places the result in a buffer. 
        [DllImport(ExternDll.SHLWAPI, EntryPoint = "StrRetToBuf", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int StrRetToBuf(IntPtr pstr, IntPtr pidl, StringBuilder pszBuf, int cchBuf);

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
        private IntPtr[] GetPIDL(FileInfo[] arrayOfFileInfo)
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
        private void FreePIDL(IntPtr[] arrPIDL)
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

        #region WindowsHookInvoked()
        /// <summary>
        /// Handle messages for context menu
        /// </summary>
        private void WindowsHookInvoked(object sender, HookEventArgs e)
        {
            var cwp = (CWPSTRUCT)Marshal.PtrToStructure(e.LParam, typeof(CWPSTRUCT));
            
            if (_oContextMenu2 != null &&
                (cwp.message == (int)WM.INITMENUPOPUP ||
                 cwp.message == (int)WM.MEASUREITEM ||
                 cwp.message == (int)WM.DRAWITEM))
            {
                if (_oContextMenu2.HandleMenuMsg((uint)cwp.message, cwp.wparam, cwp.lparam) == S_OK)
                {
                    return;
                }
            }

            if (_oContextMenu3 != null && cwp.message == (int)WM.MENUCHAR)
            {
                if (_oContextMenu3.HandleMenuMsg2((uint)cwp.message, cwp.wparam, cwp.lparam, IntPtr.Zero) == S_OK)
                {
                }
            }
        }
        #endregion
    }
}
