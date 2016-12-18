using System;

namespace LanExchange.Plugin.Windows.Enums
{
    // Indicate the type of events for which to receive notifications
    [Flags]
    public enum SHCNRF
    {
        InterruptLevel = 0x0001,
        ShellLevel = 0x0002,
        RecursiveInterrupt = 0x1000,
        NewDelivery = 0x8000
    }
}