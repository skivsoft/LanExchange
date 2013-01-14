using System;

namespace LanExchange.View
{
    public interface IMainView
    {
        void Restart();
        void ShowStatusText(string format, params object[] args);
    }
}
