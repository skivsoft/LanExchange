using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using LanExchange.Presenter;
using LanExchange.Utils;
using System.Security.Principal;
using System.Collections.Generic;

namespace LanExchange.Model.Settings
{
    /// <summary>
    /// Program settings. Implemented as Singleton.
    /// </summary>
    public class Settings
    {
        private static Settings m_Instance;
        private static bool m_Modified;

        private HashtableSerializable m_Config;
        private Hashtable m_Default;

        public event EventHandler<SettingsChangedArgs> Changed;

        private Settings()
        {
            m_Config = new HashtableSerializable();
            m_Default = new Hashtable();
            m_Default.Add("ShowMainMenu", true);
            m_Default.Add("RunMinimized", true);
            m_Default.Add("AdvancedMode", false);
            m_Default.Add("NumInfoLines", 3);
            m_Default.Add("Language", "en-US");
            
            WMIClassesInclude = new List<string>();
            WMIClassesInclude.Add("Win32_Desktop");
            WMIClassesInclude.Add("Win32_DesktopMonitor");
            WMIClassesInclude.Add("Win32_DiskDrive");
            WMIClassesInclude.Add("Win32_BIOS");
            WMIClassesInclude.Add("Win32_Processor");
            WMIClassesInclude.Add("Win32_PhysicalMemory");
        }

        public static Settings Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    var temp = new Settings();
                    m_Instance = temp;
                }
                return m_Instance;
            }
        }

        private static bool Modified
        {
            get { return m_Modified; }
            set 
            {
                m_Modified = value;
                SaveIfModified();
            }

        }


        /// <summary>
        /// Remove duplicates from list.
        /// </summary>
        /// <param name="list">a list of strings</param>
        public static void List_Distinct(List<string> list)
        {
            list.Sort();
            Int32 index = 0;
            while (index < list.Count - 1)
            {
                if (list[index] == list[index + 1])
                    list.RemoveAt(index);
                else
                    index++;
            } 
        }

        public void Load()
        {
            var fileName = GetConfigFileName();
            if (!File.Exists(fileName)) return;
            try
            {
                var temp = (Settings)SerializeUtils.DeserializeObjectFromXMLFile(fileName, typeof(Settings));
                if (temp != null)
                {
                    //List_Distinct(temp.WMIClassesInclude);
                    //m_Instance = null;
                    //m_Instance = temp;
                    foreach (var key in temp.General)
                        SetValue(key.ToString(), temp.General[key]);
                    Modified = false;
                }
            }
            catch (Exception)
            {
            }
        }


        public static void SaveIfModified()
        {
            if (!Modified) return;
            var fileName = GetConfigFileName();
            try
            {
                SerializeUtils.SerializeObjectToXMLFile(fileName, Instance);
            }
            catch (Exception e)
            {
            }
            m_Modified = false;
        }

        public static string GetExecutableFileName()
        {
            var Params = Environment.GetCommandLineArgs();
            return Params.Length > 0 ? Params[0] : string.Empty;
        }

        public static string GetConfigFileName()
        {
            return Path.ChangeExtension(GetExecutableFileName(), ".cfg");
        }

        public static string GetCurrentUserName()
        {
            var user = WindowsIdentity.GetCurrent();
            if (user != null)
            {
                string[] A = user.Name.Split('\\');
                return A.Length > 1 ? A[1] : A[0];
            }
            return string.Empty;
        }

        public bool GetBoolValue(string name)
        {
            var value = m_Config[name] ?? m_Default[name];
            return value != null && (bool)value;
        }

        public void SetBoolValue(string name, bool value)
        {
            var oldValue = GetBoolValue(name);
            if (oldValue != value)
            {
                m_Config[name] = value;
                OnChanged(name, value);
            }
        }

        public string GetStringValue(string name)
        {
            var value = m_Config[name] ?? m_Default[name];
            return value == null ? string.Empty : (string)value;
        }

        public void SetStringValue(string name, string value)
        {
            var oldValue = GetStringValue(name);
            if (string.Compare(oldValue, value, StringComparison.CurrentCultureIgnoreCase) != 0)
            {
                m_Config[name] = value;
                OnChanged(name, value);
            }
        }

        public int GetIntValue(string name)
        {
            var value = m_Config[name] ?? m_Default[name];
            return value == null ? 0 : (int)value;
        }

        public void SetIntValue(string name, int value)
        {
            var oldValue = GetIntValue(name);
            if (oldValue != value)
            {
                m_Config[name] = value;
                OnChanged(name, value);
            }
        }

        private void SetValue(string name, object value)
        {
            var oldValue = (IComparable)(m_Config[name] ?? m_Default[name]);
            if (oldValue.CompareTo(value as IComparable) != 0)
            {
                m_Config[name] = value;
                OnChanged(name, value);
            }
        }

        private void OnChanged(string name, object value)
        {
            if (Changed != null)
            {
                var args = new SettingsChangedArgs(name, value);
                Changed(this, args);
                m_Config[name] = args.NewValue;
            }
            m_Modified = true;
            SaveIfModified();
        }

        public HashtableSerializable General
        {
            get { return m_Config; }
            set { m_Config = value; }
        }

        // properties below must not be modified instantly

        public int MainFormX { get; set; }
        public int MainFormWidth { get; set; }

        public List<string> WMIClassesInclude { get; set; }

        public bool IsAutorun
        {
            get
            {
                return AutorunUtils.Autorun_Exists(GetExecutableFileName());
            }
            set
            {
                var exeFName = GetExecutableFileName();
                if (value)
                {
                    AutorunUtils.Autorun_Add(exeFName);
                }
                else
                {
                    AutorunUtils.Autorun_Delete(exeFName);
                }
            }
        }

        [DefaultValue(true)]
        public bool RunMinimized { get; set; }

        [DefaultValue(false)]
        public bool AdvancedMode { get; set; }


    }
}