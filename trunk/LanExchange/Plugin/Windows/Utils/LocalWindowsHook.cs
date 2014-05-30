using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using LanExchange.OS;

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
        protected IntPtr m_hhook = IntPtr.Zero;
        protected HookProc m_filterFunc = null;
        protected HookType m_hookType;
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
            m_hookType = hook;
            m_filterFunc = new HookProc(this.CoreHookProc);
        }
        public LocalWindowsHook(HookType hook, HookProc func)
        {
            m_hookType = hook;
            m_filterFunc = func;
        }
        // ************************************************************************

        // ************************************************************************
        // Default filter function
        protected int CoreHookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
                return CallNextHookEx(m_hhook, code, wParam, lParam);

            // Let clients determine what to do
            var e = new HookEventArgs();
            e.HookCode = code;
            e.wParam = wParam;
            e.lParam = lParam;
            OnHookInvoked(e);

            // Yield to the next hook in the chain
            return CallNextHookEx(m_hhook, code, wParam, lParam);
        }
        // ************************************************************************

        // ************************************************************************
        // Install the hook
        public void Install()
        {
            m_hhook = SetWindowsHookEx(
                m_hookType,
                m_filterFunc,
                IntPtr.Zero,
                (int)Thread.CurrentThread.ManagedThreadId);
        }
        // ************************************************************************

        // ************************************************************************
        // Uninstall the hook
        public void Uninstall()
        {
            UnhookWindowsHookEx(m_hhook);
        }
        // ************************************************************************


        #region Win32 Imports
        // ************************************************************************
        // Win32: SetWindowsHookEx()
        [DllImport(ExternDll.User32)]
        protected static extern IntPtr SetWindowsHookEx(HookType code,
                                                        HookProc func,
                                                        IntPtr hInstance,
                                                        int threadID);
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