using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace LanExchange.Plugin.Windows.Utils
{
    [Localizable(false)]
    public class LocalWindowsHook
    {
        protected IntPtr hhook = IntPtr.Zero;
        protected HookProc filterFunc = null;
        protected HookType hookType;
        public event HookEventHandler HookInvoked;

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

        // Filter function delegate
        public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);
        // Event delegate
        public delegate void HookEventHandler(object sender, HookEventArgs e);

        protected void OnHookInvoked(HookEventArgs e)
        {
            if (HookInvoked != null)
                HookInvoked(this, e);
        }

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

        public void Install()
        {
            hhook = SetWindowsHookEx(
                hookType,
                filterFunc,
                IntPtr.Zero,
                Thread.CurrentThread.ManagedThreadId);
        }

        public void Uninstall()
        {
            UnhookWindowsHookEx(hhook);
        }

        [DllImport(ExternDll.User32)]
        protected static extern IntPtr SetWindowsHookEx(HookType code, HookProc func, IntPtr hInstance, int threadId);

        [DllImport(ExternDll.User32)]
        protected static extern int UnhookWindowsHookEx(IntPtr hhook);

        [DllImport(ExternDll.User32)]
        protected static extern int CallNextHookEx(IntPtr hhook, int code, IntPtr wParam, IntPtr lParam);
    }
}