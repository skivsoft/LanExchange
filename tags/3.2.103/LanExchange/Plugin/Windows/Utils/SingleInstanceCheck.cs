using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Security.Permissions;

namespace LanExchange.Plugin.Windows.Utils
{

    /// <summary>
    /// This checks for another instance of an app.
    /// Use inside Main() by calling SingleInstanceCheck.Check();
    /// Include this class as a sibling to Main's class.
    /// If another instance of the app is detected, 
    /// - A message box appears.
    /// - 'this' instance calls Exit.
    /// </summary>
    /// <example>
    ///    static void Main()
    ///    {
    ///       MyApp.SingleInstanceCheck.Check();
    ///       Application.EnableVisualStyles();
    ///       Application.SetCompatibleTextRenderingDefault(false);
    ///       Application.Run(new Form1());
    ///    }
    /// </example>
    public static class SingleInstanceCheck
    {

        static bool FindInvisibleWindow(IntPtr hWnd, IntPtr lParam)
        {
            var wndRef = new HandleRef(lParam, hWnd);
            // skip visible windows
            if (SafeNativeMethods.IsWindowVisible(wndRef))
                return true;
            // skip windows without caption
            if (SafeNativeMethods.GetWindowTextLength(wndRef) == 0)
                return true;
            // retrieve process id for a window
            int procId;
            SafeNativeMethods.GetWindowThreadProcessId(wndRef, out procId);
            // we found or not
            if (procId == lParam.ToInt32())
            {
                UnsafeNativeMethods.ShowWindowAsync(wndRef, NativeMethods.SW_NORMAL);
                UnsafeNativeMethods.SetForegroundWindow(wndRef);
                return false;
            }
            return true;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        internal static bool CheckExists(string unicalName)
        {
            // http://snipplr.com/view/19272/ - C#, single-instance-check using mutex
            // http://iridescence.no/post/CreatingaSingleInstanceApplicationinC.aspx
            bool isOwnedHere;
            s_AppStartMutex = new Mutex(true, unicalName, out isOwnedHere);
            if (isOwnedHere) return false; 

            Process me = Process.GetCurrentProcess();
            Process[] procList = Process.GetProcessesByName(me.ProcessName);
            foreach (Process process in procList) // Debug note: Set Enable the Visual Studio Hosting Process = false.
            {
                if (process.Id != me.Id) // If the ProcessName matches but the Id doesn't, it's another instance of mine.
                {
                    if (process.MainWindowHandle != IntPtr.Zero)
                    {
                        var wndRef = new HandleRef(process, process.MainWindowHandle);
                        UnsafeNativeMethods.ShowWindowAsync(wndRef, NativeMethods.SW_NORMAL);
                        UnsafeNativeMethods.SetForegroundWindow(wndRef);
                    }
                    else
                    {
                        var callback = new SafeNativeMethods.EnumThreadWindowsCallback(FindInvisibleWindow);
                        SafeNativeMethods.EnumWindows(callback, new IntPtr(process.Id));
                        GC.KeepAlive(callback);
                    }
                    break;
                }
            }
            return true;
        }

        // volatile is intended to prevent GC. Otherwise, GC.KeepAlive(appStartMutex) might be needed.
        // This appears to be a concern in In release builds but not debug builds.

        // ReSharper disable NotAccessedField.Local
        static volatile Mutex s_AppStartMutex;
        // ReSharper restore NotAccessedField.Local

    }
}
