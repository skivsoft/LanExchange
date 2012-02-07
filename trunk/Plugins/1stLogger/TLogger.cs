using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace SkivSoft.LanExchange.SDK
{
    public class TLogger : ILanEXPlugin
    {
        static bool bFirstRun = true;
        private ILanEXMainApp App;

        public void Initialize(IServiceProvider serviceProvider)
        {
            App = serviceProvider.GetService(typeof(ILanEXMainApp)) as ILanEXMainApp;
            if (App != null)
            {
                App.LoggerPrintEventHandlerAdd(MyLoggerPrint);
            }
        }

        protected void MyLoggerPrint(object sender, LoggerPrintEventArgs e)
        {
            // имя лог файла
            string[] args = Environment.GetCommandLineArgs();
            string LogFName = Path.ChangeExtension(args[0], ".log");
            // открываем для добавления либо создаем файл, если не существует
            FileStream FS = null;
            try
            {
                if (!File.Exists(LogFName))
                    FS = File.Open(LogFName, FileMode.Create, FileAccess.Write, FileShare.Read);
                else
                    FS = File.Open(LogFName, FileMode.Append, FileAccess.Write, FileShare.Read);
                // строка для записи в лог
                DateTime DT = DateTime.Now;
                string str = String.Format("{0} {1} {2}:{3}|{4}{5}", DT.ToShortDateString(), DT.ToLongTimeString(),
                    Process.GetCurrentProcess().Id, Thread.CurrentThread.ManagedThreadId,
                    e.Text, Environment.NewLine);
                if (bFirstRun)
                {
                    str = Environment.NewLine + str;
                    bFirstRun = false;
                }
                // пишем в лог
                byte[] data = Encoding.UTF8.GetBytes(str);
                FS.Write(data, 0, data.Length);
            }
            catch { }
            finally
            {
                if (FS != null)
                {
                    FS.Flush();
                    FS.Close();
                }
            }
        }

        public string Author { get { return "Skiv"; } }
        public string Version { get { return "1.0"; } }
        public string Name { get { return "Logger"; } }
        public string Description { get { return "Журналирование действий программы в лог-файл."; } }

        public void Loaded()
        {
            // do nothing
        }
    }
}