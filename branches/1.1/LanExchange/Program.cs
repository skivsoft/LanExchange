#define __SINGLE_INSTANCE

using System;
using System.Windows.Forms;
using System.Reflection;
using NLog;
using LanExchange.UI;

namespace LanExchange
{
    internal static class Program
    {
        #if SINGLE_INSTANCE
        private const string EventOneCopyOnlyName = "{e8813243-5a4d-4569-85ad-e95848a1c579}";
        private static ApplicationContext context;
        #endif

        private readonly static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        static void LogHeader()
        {
            logger.Info("OSVersion: [{0}], Processors count: {1}", Environment.OSVersion, Environment.ProcessorCount);
            logger.Info(@"MachineName: {0}, UserName: {1}\{2}, Interactive: {3}", Environment.MachineName, Environment.UserDomainName, Environment.UserName, Environment.UserInteractive);
            logger.Info("Executable: [{0}], Version: {1}", Application.ExecutablePath, Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        #if SINGLE_INSTANCE

        private static EventWaitHandle eventOneCopyOnly;
        private static Thread threadOneCopyOnly;

        static void FirstInstance()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); // must be called before first form created

            using (context = new ApplicationContext(new MainForm()))
            {
                context.MainForm.Load += delegate { logger.Trace("Instance showed"); };

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
                                logger.Trace("Instance activated");
                            });
                            while (Method != null) { }
                            context.MainForm.Invoke(Method);
                        }//while
                    }
                    catch (ThreadAbortException)
                    {
                        logger.Trace("FirstInstanceCheck thread is cancelled.");
                    }
                });
                threadOneCopyOnly.Name = "One Copy Only";
                threadOneCopyOnly.IsBackground = true;
                threadOneCopyOnly.Start();
                Application.ApplicationExit += new EventHandler(AppExit);
                Application.Run(context);
            }//using
        }

        static void SecondInstance()
        {
            logger.Trace("Instance is second copy");
            eventOneCopyOnly.Set();
        }

        static void AppExit(object sender, EventArgs e)
        {
            threadOneCopyOnly.Abort();
        }

        #else

        static void SimpleStartInstance()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); // must be called before first form created
            Application.Run(new MainForm());
            // workaround for NLog's bug under Mono (hanging after app exit) 
            LogManager.Configuration = null;
        }

        #endif


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            logger.ErrorException(String.Format("Unhandled: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace), ex);
        }

        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //throw new Exception("This will go unhandled"); 
            try
            {
                LogHeader();
                #if SINGLE_INSTANCE
                bool createdNew;
                using (eventOneCopyOnly = new EventWaitHandle(false, EventResetMode.AutoReset, EventOneCopyOnlyName, out createdNew))
                {
                    if (createdNew)
                        FirstInstance();
                    else
                        SecondInstance();
                } // using
                #else
                SimpleStartInstance();
                #endif
            }
            catch (Exception e)
            {
                logger.ErrorException(String.Format("Error in Main(): {0}{1}{2}", e.Message, Environment.NewLine, e.StackTrace), e);
            }
        }
    }
}
