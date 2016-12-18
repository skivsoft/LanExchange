using System;

namespace LanExchange.Plugin.Windows.Enums
{
    // Flags that indicate the meaning of the dwItem1 and dwItem2 parameters
    [Flags]
    public enum SHCNF
    {
        IDLIST = 0x0000,
        PATHA = 0x0001,
        PRINTERA = 0x0002,
        DWORD = 0x0003,
        PATHW = 0x0005,
        PRINTERW = 0x0006,
        TYPE = 0x00FF,
        FLUSH = 0x1000,
        FLUSHNOWAIT = 0x2000
    }
}