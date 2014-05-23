﻿using System.ComponentModel;
using System;

namespace LanExchange.SDK
{
    [Localizable(false)]
    public static class EnvironmentUtils
    {
        private const string PROCESSOR_ARCHITECTURE = "PROCESSOR_ARCHITECTURE";

        public static bool Is64BitOperatingSystem()
        {
            if (IntPtr.Size == 8) 
                return true;
            return string.CompareOrdinal(Environment.GetEnvironmentVariable(PROCESSOR_ARCHITECTURE), "x86") != 0;
        }

        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }
    }
}