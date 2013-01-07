using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using LanExchange.Windows;
using System.ComponentModel;
using System.Collections;
using System.Text;

namespace LanExchange.Utils
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
            int ProcID;
            SafeNativeMethods.GetWindowThreadProcessId(wndRef, out ProcID);
            // we found or not
            if (ProcID == lParam.ToInt32())
            {
                UnsafeNativeMethods.ShowWindowAsync(wndRef, NativeMethods.SW_NORMAL);
                UnsafeNativeMethods.SetForegroundWindow(wndRef);
                return false;
            }
            return true;
        }

        static public void Check()
        {
            // http://snipplr.com/view/19272/ - C#, single-instance-check using mutex
            // http://iridescence.no/post/CreatingaSingleInstanceApplicationinC.aspx
            bool isOwnedHere = false;
            appStartMutex = new Mutex(
                true,
                Application.ProductName,
                out isOwnedHere
            );
            if (isOwnedHere) return; 

            Process me = Process.GetCurrentProcess();
            foreach (Process process in Process.GetProcessesByName(me.ProcessName)) // Debug note: Set Enable the Visual Studio Hosting Process = false.
            {
                if (process.Id != me.Id) // If the ProcessName matches but the Id doesn't, it's another instance of mine.
                {
                    if (process.MainWindowHandle != IntPtr.Zero)
                    {
                        HandleRef wndRef = new HandleRef(process, process.MainWindowHandle);
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
            Environment.Exit(0); // This will kill my instance.
        }

        // volatile is intended to prevent GC. Otherwise, GC.KeepAlive(appStartMutex) might be needed.
        // This appears to be a concern in In release builds but not debug builds.
        static volatile Mutex appStartMutex;

    }
}
