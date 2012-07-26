using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); // must be called before first form created
            MainForm form = new MainForm();
            // START MVC NOW
            ApplicationFacade facade = (ApplicationFacade)ApplicationFacade.Instance;
            facade.Startup(form);
            // RUN
            Application.Run(form);
        }
    }

}
