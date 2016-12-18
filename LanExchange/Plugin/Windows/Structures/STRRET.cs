using System;
using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Windows.Structures
{
    // Contains strings returned from the IShellFolder interface methods
    [StructLayout(LayoutKind.Explicit)]
    public struct STRRET
    {
        [FieldOffset(0)]
        public uint uType;
        [FieldOffset(4)]
        public IntPtr pOleStr;
        [FieldOffset(4)]
        public IntPtr pStr;
        [FieldOffset(4)]
        public uint uOffset;
        [FieldOffset(4)]
        public IntPtr cStr;
    }
}