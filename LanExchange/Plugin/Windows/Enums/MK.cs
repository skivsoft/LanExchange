using System;

namespace LanExchange.Plugin.Windows.Enums
{
    // Flags indicating which mouse buttons are clicked and which modifier keys are pressed
    [Flags]
    public enum MK
    {
        LBUTTON = 0x0001,
        RBUTTON = 0x0002,
        SHIFT = 0x0004,
        CONTROL = 0x0008,
        MBUTTON = 0x0010,
        ALT = 0x0020
    }
}