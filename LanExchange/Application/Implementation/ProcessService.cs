using System.Diagnostics;
using LanExchange.Application.Interfaces;

namespace LanExchange.Application.Implementation
{
    /// <summary>
    /// The process service implementation.
    /// </summary>
    /// <seealso cref="LanExchange.Application.Interfaces.IProcessService" />
    internal sealed class ProcessService : IProcessService
    {
        public void Start(string fileName)
        {
            Process.Start(fileName);
        }
    }
}