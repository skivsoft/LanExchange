using System.ComponentModel;
using System.Runtime.InteropServices;
using System;

namespace LanExchange.Utils
{
    [Localizable(false)]
    public static class Kernel32
    {

        // Will return result of 64but OS on all versions of windows that support .net
        public static bool Is64BitOperatingSystem()
        {
            if (IntPtr.Size == 8) //64bit will only run on 64bit
            {
                return true;
            }

            bool flag;
            return (DoesWin32MethodExist("kernel32.dll", "IsWow64Process") && IsWow64Process(GetCurrentProcess(), out flag)) && flag;
        }

        private static bool DoesWin32MethodExist(string moduleName, string methodName)
        {
            IntPtr moduleHandle = GetModuleHandle(moduleName);
            if (moduleHandle == IntPtr.Zero)
            {
                return false;
            }

            return GetProcAddress(moduleHandle, methodName) != IntPtr.Zero;
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr module, [MarshalAs(UnmanagedType.LPStr)]string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process(IntPtr process, out bool wow64Process);
    }
}