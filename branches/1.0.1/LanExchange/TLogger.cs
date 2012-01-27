using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace LanExchange
{
    class TLogger
    {
        static bool bFirstRun = true;

        /// <summary>
        /// Запись строки в лог-файл.
        /// </summary>
        /// <param name="Text">Текст сообщения.</param>
        static public void Print(string Text)
        {
            // имя лог файла
            string LogFName = Path.ChangeExtension(Application.ExecutablePath, ".log");
            // открываем для добавления либо создаем файл, если не существует
            FileStream FS;
            try
            {
                if (!File.Exists(LogFName))
                    FS = File.Open(LogFName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                else
                    FS = File.Open(LogFName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                try
                {
                    // строка для записи в лог
                    DateTime DT = DateTime.Now;
                    string str = String.Format("{0} {1} {2}:{3}|{4}{5}", DT.ToShortDateString(), DT.ToLongTimeString(), 
                        Process.GetCurrentProcess().Id, Thread.CurrentThread.ManagedThreadId,
                        Text, Environment.NewLine);
                    if (bFirstRun)
                    {
                        str = Environment.NewLine + str;
                        bFirstRun = false;
                    }
                    // пишем в лог
                    byte[] data = Encoding.UTF8.GetBytes(str);
                    FS.Write(data, 0, data.Length);
                }
                finally
                {
                    FS.Flush();
                    FS.Close();
                    FS.Dispose();
                }
            }
            catch { }
        }

        /// <summary>
        /// Запись строки в лог-файл.
        /// </summary>
        /// <param name="format">Формат строки.</param>
        /// <param name="args">Параметры.</param>
        static public void Print(string format, params object[] args)
        {
            Print(String.Format(format, args));
        }
    }
}
