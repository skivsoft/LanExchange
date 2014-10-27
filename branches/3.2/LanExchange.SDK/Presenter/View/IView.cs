using System;

namespace LanExchange.SDK.Presenter.View
{
    /// <summary>
    /// Base interface for any View interface.
    /// </summary>
    public interface IView
    {
        IntPtr Handle { get; }
    }
}