using System;
using System.Reflection;
using SkivSoft.LanExchange.SDK;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Text;
using System.Drawing;

namespace LanExchange
{
    public abstract class TMainApp : ILanEXMainApp, IServiceProvider
    {
        public static TMainApp App = null;
        public Dictionary<string, ILanEXPlugin> plugins = null;
        private ILanEXForm MainFormInstance = null;
        private ILanEXTabControl PagesInstance = null;
        private ILanEXStatusStrip StatusStripInstance = null;
      
        public TMainApp()
        {
            plugins = new Dictionary<string, ILanEXPlugin>();
        }

        public void Init()
        {
            // create main form wrapper
            this.MainFormInstance = CreateMainForm();
            this.PagesInstance = CreatePages();
            this.StatusStripInstance = CreateStatusStrip();
        }

        public event LoggerPrintEventHandler LoggerPrint;
        public event EventHandler Loaded;


        public virtual object GetService(Type serviceType)
        {
            if (serviceType == typeof(SkivSoft.LanExchange.SDK.ILanEXMainApp))
            {
                return this;
            }
            return null;
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
                        Type iface = type.GetInterface("SkivSoft.LanExchange.SDK.ILanEXPlugin");

                        if (iface != null)
                        {
                            SkivSoft.LanExchange.SDK.ILanEXPlugin plugin = (SkivSoft.LanExchange.SDK.ILanEXPlugin)Activator.CreateInstance(type);
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

        public virtual void DoLoaded()
        {
            LogPrint("MainForm loaded");
            // shows current computer name and user name
            StatusStripInstance.SetText(1, this.ComputerName);
            StatusStripInstance.SetText(2, this.UserName);
            // call Loaded() for each plugin
            if (Loaded != null)
            {
                try
                {
                    Loaded(this, new EventArgs());
                }
                catch (Exception ex)
                {
                    LogPrint(ex);
                }
            }
        }

        public ILanEXForm MainForm { get { return this.MainFormInstance; } }
        public ILanEXTabControl Pages { get { return this.PagesInstance; } }
        public ILanEXStatusStrip StatusStrip { get { return this.StatusStripInstance; } }

        // abstract methods UI related (WinForms/WPF)
        public abstract ILanEXForm CreateMainForm();
        public abstract ILanEXTabControl CreatePages();
        public abstract ILanEXStatusStrip CreateStatusStrip();
        public abstract ILanEXControl CreateControl(Type type);
        public abstract void ListView_SetupTip(ILanEXListView LV);
        public abstract void ListView_Setup(ILanEXListView LV);
        public abstract void ListView_Update(ILanEXListView LV);
        public abstract string InputBoxAsk(string caption, string prompt, string defText);
    }
}