using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace LanExchange.Plugin.Windows.Utils
{
    [Localizable(false)]
    public class LocalWindowsHook
    {
        // ************************************************************************
        // Filter function delegate
        public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);
        // ************************************************************************

        // ************************************************************************
        // Internal properties
        protected IntPtr hhook = IntPtr.Zero;
        protected HookProc filterFunc = null;
        protected HookType hookType;
        // ************************************************************************

        // ************************************************************************
        // Event delegate
        public delegate void HookEventHandler(object sender, HookEventArgs e);
        // ************************************************************************

        // ************************************************************************
        // Event: HookInvoked 
        public event HookEventHandler HookInvoked;
        protected void OnHookInvoked(HookEventArgs e)
        {
            if (HookInvoked != null)
                HookInvoked(this, e);
        }
        // ************************************************************************

        // ************************************************************************
        // Class constructor(s)
        public LocalWindowsHook(HookType hook)
        {
            hookType = hook;
            filterFunc = CoreHookProc;
        }
        public LocalWindowsHook(HookType hook, HookProc func)
        {
            hookType = hook;
            filterFunc = func;
        }
        // ************************************************************************

        // ************************************************************************
        // Default filter function
        protected int CoreHookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
                return CallNextHookEx(hhook, code, wParam, lParam);

            // Let clients determine what to do
            var e = new HookEventArgs();
            e.HookCode = code;
            e.WParam = wParam;
            e.LParam = lParam;
            OnHookInvoked(e);

            // Yield to the next hook in the chain
            return CallNextHookEx(hhook, code, wParam, lParam);
        }
        // ************************************************************************

        // ************************************************************************
        // Install the hook
        public void Install()
        {
            hhook = SetWindowsHookEx(
                hookType,
                filterFunc,
                IntPtr.Zero,
                Thread.CurrentThread.ManagedThreadId);
        }
        // ************************************************************************

        // ************************************************************************
        // Uninstall the hook
        public void Uninstall()
        {
            UnhookWindowsHookEx(hhook);
        }
        // ************************************************************************


        #region Win32 Imports
        // ************************************************************************
        // Win32: SetWindowsHookEx()
        [DllImport(ExternDll.User32)]
        protected static extern IntPtr SetWindowsHookEx(HookType code,
                                                        HookProc func,
                                                        IntPtr hInstance,
                                                        int threadId);
        // ************************************************************************

        // ************************************************************************
        // Win32: UnhookWindowsHookEx()
        [DllImport(ExternDll.User32)]
        protected static extern int UnhookWindowsHookEx(IntPtr hhook);
        // ************************************************************************

        // ************************************************************************
        // Win32: CallNextHookEx()
        [DllImport(ExternDll.User32)]
        protected static extern int CallNextHookEx(IntPtr hhook,
                                                   int code, IntPtr wParam, IntPtr lParam);
        // ************************************************************************
        #endregion
    }
}