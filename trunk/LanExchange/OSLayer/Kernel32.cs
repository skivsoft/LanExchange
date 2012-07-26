using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LanExchange.OSLayer
{
    public class Kernel32
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr module, [MarshalAs(UnmanagedType.LPStr)]string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process(IntPtr process, out bool wow64Process);
    }
}
