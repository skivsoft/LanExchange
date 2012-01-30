using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace Restart
{

    public class TLogger
    {
        static bool bFirstRun = true;

        /// <summary>
        /// Запись строки в лог-файл.
        /// </summary>
        /// <param name="Text">Текст сообщения.</param>
        static public void Print(string Text)
        {
            // имя лог файла
            //Process Proc = Process.GetCurrentProcess();
            //string LogFName = Path.ChangeExtension(Proc.MainModule.FileName, ".log");

            string[] args = Environment.GetCommandLineArgs();
            string LogFName = Path.ChangeExtension(args[0], ".log");
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
    
    class Program
    {
        const int WM_QUIT = 0x0012;

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        static void ShowUsage()
        {
            Console.WriteLine("Usage: Restart.exe <PID> <CmdLine>");
            Console.WriteLine("This program waits until process <PID> exited then starts <CmdLine>.");
        }

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowUsage();
                return;
            }
            int PID;
            if (!Int32.TryParse(args[0], out PID))
            {
                ShowUsage();
                return;
            }
            Process Proc = null;
            try
            {
                Proc = Process.GetProcessById(PID);
                string ProgName = Proc.MainModule.FileName;

                TLogger.Print("Sending WM_QUIT to window {0} of process {1}", Proc.MainWindowHandle.ToString(), PID);
                HandleRef HRef = new HandleRef(null, Proc.MainWindowHandle);
                PostMessage(HRef, WM_QUIT, IntPtr.Zero, IntPtr.Zero);
                Proc = null;
                try
                {
                    Proc = Process.GetProcessById(PID);
                }
                catch { }
                if (Proc != null)
                {
                    TLogger.Print("Waiting until proccess {0} exited...", PID);
                    if (!Proc.WaitForExit(1000))
                    {
                        TLogger.Print("Terminating process {0}", PID);
                        Proc.Kill();
                    }
                }
            
                string Params = "";
                for (int i = 1; i < args.Length; i++)
                {
                    if (Params.Length != 0)
                        Params += " ";
                    Params += args[i];
                }
                TLogger.Print("Starting program \"{0}\" with params \"{1}\"", ProgName, Params);
            
                Process.Start(ProgName, Params);
            
            }
            catch(Exception e)
            {
                TLogger.Print("Exception: " + e.Message);
            }
        }
    }
}
