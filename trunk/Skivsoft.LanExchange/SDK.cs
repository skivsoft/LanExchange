using System;
using System.Reflection;
using System.Windows.Forms;
using SkivSoft.LanExchange.SDK;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace SkivSoft.LanExchange
{
    public class TMainApp : IMLanEXMainApp, IServiceProvider
    {
        public Dictionary<string, IMLanEXPlugin> plugins = null;
        
        public TMainApp()
        {
            plugins = new Dictionary<string, IMLanEXPlugin>();
        }
        
        public void PrintKarrramba()
        {
            MessageBox.Show("Karrramba");
        }
        
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(SkivSoft.LanExchange.SDK.IMLanEXMainApp))
            {
                return this;
            }
            return null;
        }


        protected event LoggerPrintEventHandler LoggerPrint;

        public void LoggerPrintEventHandlerAdd(LoggerPrintEventHandler handler)
        {
            LoggerPrint += handler;
            LogPrint("Log handler installed {0}", AppDomain.CurrentDomain.FriendlyName);
        }

        public void LoggerPrintEventHandlerRemove(LoggerPrintEventHandler handler)
        {
            LoggerPrint -= handler;
            LogPrint("Log handler removed");
        }

        protected virtual void OnLoggerPrint(LoggerPrintEventArgs e)
        {
            if (this.LoggerPrint != null)
                try
                {
                    this.LoggerPrint(this, e);
                }
                catch { }
        }

        public void LogPrint(string format, params object[] args)
        {
            OnLoggerPrint(new LoggerPrintEventArgs(String.Format(format, args)));
        }

        public void LogPrint(Exception ex)
        {
            LogPrint("Exception: {0}\n{1}", ex.Message, ex.StackTrace);
        }

        /// <summary>
        /// Загрузка плагинов из папки plugins.
        /// Для приложения устнавливается уровень безопасности Intranet.
        /// Это значит что сборки загруженные не из своего каталога будут имень меньше прав.
        /// В частности если сборки загружены из каталога plugins/ то не смогут писать в файл на сетевом диске.
        /// В связи с этим плагины размещаются в том же каталоге что и исполняемый файл и находятся по маске Plugin*.dll.
        /// </summary>
        public void LoadPlugins()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            //folder = Path.Combine(folder, "plugins");
            if (!Directory.Exists(folder))
                return;
            string[] files = Directory.GetFiles(folder, "Plugin*.dll", SearchOption.TopDirectoryOnly);
            foreach(string file in files)
            {
                try
                {
                    LogPrint("Load plugin library [{0}]", file);
                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("SkivSoft.LanExchange.SDK.IMLanEXPlugin");

                        if (iface != null)
                        {
                            SkivSoft.LanExchange.SDK.IMLanEXPlugin plugin = (SkivSoft.LanExchange.SDK.IMLanEXPlugin)Activator.CreateInstance(type);
                            if (plugin != null)
                            {
                                plugins.Add(plugin.Name, plugin);
                                plugin.Initialize(this);
                                LogPrint("Plugin \"{0}\" initialized", plugin.Name);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogPrint(ex);
                }
            }
        }

        public string ComputerName 
        {
            get
            {
                return Environment.MachineName;
            }
        }

        public string UserName
        {
            get
            {
                System.Security.Principal.WindowsIdentity user = System.Security.Principal.WindowsIdentity.GetCurrent();
                string[] A = user.Name.Split('\\');
                return A.Length > 1 ? A[1] : A[0];
            }
        }
    }
}