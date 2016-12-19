using System;

namespace LanExchange.Application.Interfaces
{
    public interface ILogService
    {
        void Log(string format, params object[] args);

        void Log(Exception exception);
    }
}