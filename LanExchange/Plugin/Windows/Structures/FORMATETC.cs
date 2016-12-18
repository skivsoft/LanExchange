using System;
using System.Runtime.InteropServices;
using LanExchange.Plugin.Windows.Enums;

namespace LanExchange.Plugin.Windows.Structures
{
    // A generalized Clipboard format, it is enhanced to encompass a 
    // target device, the aspect or view of the data, and a storage medium indicator
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct FORMATETC
    {
        public CF cfFormat;
        public IntPtr ptd;
        public DVASPECT dwAspect;
        public int lindex;
        public TYMED Tymd;
    }
}