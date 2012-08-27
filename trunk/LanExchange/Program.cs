using System;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

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
            // Run Application in MVC-style
            AppFacade facade = (AppFacade)AppFacade.Instance;
            facade.SendNotification(Globals.CMD_STARTUP);
        }
    }

}
