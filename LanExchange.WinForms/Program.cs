// *****************************************************************************
// START DATE: Jan 22, 2012
// *****************************************************************************

using System;
using LanExchange.SDK.Extensions;
using LanExchange.Presentation.Interfaces;

namespace LanExchange
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CoreFacade.InitializeDIContainer()
                .Resolve<IAppBootstrap>()
                .Run();
        }
    }
}