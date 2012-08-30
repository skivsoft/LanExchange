using System;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace LanExchange
{
    static class Program
    {

        static bool CheckParam(string param)
        {
            bool Result = false;
            string[] args = Environment.GetCommandLineArgs();
            foreach (string str in args)
            {
                if (String.Compare(str, param, true) == 0)
                {
                    Result = false;
                    break;
                }
            }
            return Result;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Run Application in MVC-style
            AppFacade facade = (AppFacade)AppFacade.Instance;
            facade.SendNotification(AppFacade.CMD_STARTUP, CheckParam("/DEMO"));
        }
    }

}
