using System;

namespace LanExchange.Presentation.Interfaces
{
    public interface IWithHandle
    {
        IntPtr Handle { get; }
    }
}