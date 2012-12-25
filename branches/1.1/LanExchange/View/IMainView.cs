using System;

namespace LanExchange.View
{
    public interface IMainView
    {
        bool AdminMode { get; set; }
        void Restart();
    }
}
