// *****************************************************************************
// START DATE: Jan 22, 2012
// *****************************************************************************

using System;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Extensions;
using LanExchange.Presentation.WinForms;

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
            CoreFacade
                .InitializeDIContainer()
                .RegisterPresentationLayer()
                .Resolve<IAppBootstrap>()
                .Run();
        }
    }
}