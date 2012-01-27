using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;

namespace LanExchange
{
    internal static class Program
    {
        private const string EventOneCopyOnlyName = "{e8813243-5a4d-4569-85ad-e95848a1c579}";

        [STAThread]
        static void Main()
        {
            TLogger.Print("Program: {0}, version {1}", Application.ExecutablePath, Assembly.GetExecutingAssembly().GetName().Version.ToString());

            bool createdNew;
            using (EventWaitHandle eventOneCopyOnly = new EventWaitHandle(false, EventResetMode.AutoReset, EventOneCopyOnlyName, out createdNew))
            {
                if (createdNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false); // must be called before first form created

                    using (ApplicationContext context = new ApplicationContext(new MainForm()))
                    {
                        context.MainForm.Load += delegate { TLogger.Print("Instance showed"); };

                        Thread threadOneCopyOnly = new Thread(delegate()
                        {
                            while (true)
                            {
                                eventOneCopyOnly.WaitOne();
                                context.MainForm.Invoke(new MethodInvoker(delegate
                                {
                                    MainForm F = (MainForm)context.MainForm;
                                    F.bReActivate = true;
                                    F.IsFormVisible = true;
                                    TLogger.Print("Instance activated");
                                }));
                            }//while
                        });
                        threadOneCopyOnly.Name = "One Copy Only";
                        threadOneCopyOnly.IsBackground = true;
                        threadOneCopyOnly.Start();
                        try
                        {
                            Application.Run(context);
                        }
                        catch(Exception e)
                        {
                            TLogger.Print("APP EXCEPTION: {0}\n{1}", e.Message, e.StackTrace);
                        }
                    }//using
                }
                else
                {
                    TLogger.Print("Instance is second copy");
                    eventOneCopyOnly.Set();

                }//if
                TLogger.Print("Instance closed");
            } // using
        }
    }
}
