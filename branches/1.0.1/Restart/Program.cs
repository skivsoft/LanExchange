using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace Restart
{
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

        static void LogStr(string str)
        {
            Process Proc = Process.GetCurrentProcess();
            string LogFName = Path.ChangeExtension(Proc.MainModule.FileName, ".log");
            using (FileStream FS = File.Open(LogFName, FileMode.Append, FileAccess.Read, FileShare.ReadWrite))
            {
                str += Environment.NewLine;
                byte[] data = Encoding.UTF8.GetBytes(str);
                FS.Write(data, 0, data.Length);
                FS.Flush();
                FS.Close();
            }
            Console.WriteLine(str);
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

                LogStr(String.Format("Sending WM_QUIT to window {0} of process {1}", Proc.MainWindowHandle.ToString(), PID));
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
                    LogStr(String.Format("Waiting until proccess {0} exited...", PID));
                    if (!Proc.WaitForExit(1000))
                    {
                        LogStr(String.Format("Terminating process {0}", PID));
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
                LogStr(String.Format("Starting program \"{0}\" with params \"{1}\"", ProgName, Params));
            
                Process.Start(ProgName, Params);
            
            }
            catch(Exception e)
            {
                LogStr("Exception: " + e.Message);
            }
        }
    }
}
