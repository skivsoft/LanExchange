using System;
using System.Collections.Generic;
using System.Text;

namespace ModelNetwork.OSLayer
{
    public class Detect
    {
        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        // Will return result of 64but OS on all versions of windows that support .net
        public static bool Is64BitOperatingSystem()
        {
            if (IntPtr.Size == 8) //64bit will only run on 64bit
            {
                return true;
            }

            bool flag;
            return (DoesWin32MethodExist("kernel32.dll", "IsWow64Process") && Kernel32.IsWow64Process(Kernel32.GetCurrentProcess(), out flag)) && flag;
        }

        private static bool DoesWin32MethodExist(string moduleName, string methodName)
        {
            IntPtr moduleHandle = Kernel32.GetModuleHandle(moduleName);
            if (moduleHandle == IntPtr.Zero)
            {
                return false;
            }

            return Kernel32.GetProcAddress(moduleHandle, methodName) != IntPtr.Zero;
        }
    }
}
