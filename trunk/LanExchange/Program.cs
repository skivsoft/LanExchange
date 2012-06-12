using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using System.Security.AccessControl;

namespace LanExchange
{
    internal static class Program
    {
        private const string EventOneCopyOnlyName = "{e8813243-5a4d-4569-85ad-e95848a1c579}";
        private static EventWaitHandle eventOneCopyOnly;
        private static ApplicationContext context;
        private static Thread threadOneCopyOnly;

        static void LogHeader()
        {
            TLogger.Print("OSVersion: [{0}], Processors count: {1}", Environment.OSVersion, Environment.ProcessorCount);
            TLogger.Print(@"MachineName: {0}, UserName: {1}\{2}, Interactive: {3}", Environment.MachineName, Environment.UserDomainName, Environment.UserName, Environment.UserInteractive);
            TLogger.Print("Executable: [{0}], Version: {1}", Application.ExecutablePath, Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        static void FirstInstance()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); // must be called before first form created

            using (context = new ApplicationContext(new MainForm()))
            {
                context.MainForm.Load += delegate { TLogger.Print("Instance showed"); };

                threadOneCopyOnly = new Thread(delegate()
                {
                    try
                    {
                        while (true)
                        {
                            eventOneCopyOnly.WaitOne();
                            MethodInvoker Method = new MethodInvoker(delegate
                            {
                                MainForm F = (MainForm)context.MainForm;
                                F.bReActivate = true;
                                F.IsFormVisible = true;
                                TLogger.Print("Instance activated");
                            });
                            while (Method != null) { }
                            context.MainForm.Invoke(Method);
                        }//while
                    }
                    catch (ThreadAbortException)
                    {
                        TLogger.Print("FirstInstanceCheck thread is cancelled.");
                    }
                });
                threadOneCopyOnly.Name = "One Copy Only";
                threadOneCopyOnly.IsBackground = true;
                threadOneCopyOnly.Start();
                Application.ApplicationExit += new EventHandler(AppExit);
                Application.Run(context);
                
            }//using
        }

        static void AppExit(object sender, EventArgs e)
        {
            threadOneCopyOnly.Abort();
        }

        static void SecondInstance()
        {
            TLogger.Print("Instance is second copy");
            eventOneCopyOnly.Set();
        }

        [STAThread]
        static void Main()
        {
            try
            {
                LogHeader();
                bool createdNew;
                using (eventOneCopyOnly = new EventWaitHandle(false, EventResetMode.AutoReset, EventOneCopyOnlyName, out createdNew))
                {
                    if (createdNew)
                        FirstInstance();
                    else
                        SecondInstance();
                } // using
            }
            catch (Exception e)
            {
                TLogger.Print("Exception in Main(): {0}\n{1}", e.Message, e.StackTrace);
            }
        }
    }
}
