using System;

namespace LanExchange.Presentation.Interfaces
{
    public interface ILogService
    {
        void Log(string format, params object[] args);

        void Log(Exception exception);
    }
}