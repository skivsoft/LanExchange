using System;
using System.Diagnostics;
using LanExchange.Application.Interfaces;

namespace LanExchange.Application.Implementation
{
    /// <summary>
    /// The log service implementation.
    /// </summary>
    internal sealed class EmptyLogService : ILogService
    {
        public void Log(string format, params object[] args)
        {
            Debug.Print(format, args);
        }

        public void Log(Exception exception)
        {
            Debug.Print(exception.ToString());
        }
    }
}