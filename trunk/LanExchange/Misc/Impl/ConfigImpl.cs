using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using LanExchange.Intf;
using LanExchange.Utils;
using System.Collections.Generic;

namespace LanExchange.Misc.Impl
{
    /// <summary>
    /// Program settings. Implemented as Singleton.
    /// </summary>
    [XmlType("Config")]
    public class ConfigImpl : IConfig
    {
        private bool m_Modified;

        private readonly Hashtable m_Config;
        private readonly Hashtable m_Default;

        public event EventHandler<ConfigChangedArgs> Changed;

        public int GetIntValue(ConfigNames name)
        {
            var value = m_Config[name] ?? m_Default[name];
            return value == null ? 0 : (int)value;
        }

        //public void SetIntValue(SettingsNames name, int value)
        //{
        //    var oldValue = GetIntValue(name);
        //    if (oldValue != value)
        //    {
        //        m_Config[name] = value;
        //        OnChanged(name, value);
        //    }
        //}

        [Localizable(false)]
        public ConfigImpl()
        {
            m_Config = new Hashtable();
            m_Default = new Hashtable();
            m_Default.Add(ConfigNames.ShowMainMenu, true);
            m_Default.Add(ConfigNames.RunMinimized, true);
            m_Default.Add(ConfigNames.AdvancedMode, false);
            m_Default.Add(ConfigNames.NumInfoLines, 3);
            m_Default.Add(ConfigNames.Language, "English");
        }

        private bool Modified
        {
            get { return m_Modified; }
            set 
            {
                m_Modified = value;
                SaveIfModified();
            }
        }

        public void Load()
        {
            var fileName = App.FolderManager.ConfigFileName;
            if (!File.Exists(fileName)) return;
            try
            {
                var temp = (ConfigImpl)SerializeUtils.DeserializeObjectFromXMLFile(fileName, typeof(ConfigImpl));
                if (temp != null)
                {
                    foreach (var element in temp.Elements)
                        SetValue(element.Key, element.Value);
                    Modified = false;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }


        public void SaveIfModified()
        {
            if (!Modified) return;
            var fileName = App.FolderManager.ConfigFileName;
            try
            {
                SerializeUtils.SerializeObjectToXMLFile(fileName, this);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            m_Modified = false;
        }

        public bool GetBoolValue(ConfigNames name)
        {
            var value = m_Config[name] ?? m_Default[name];
            return value != null && (bool)value;
        }

        //public void SetBoolValue(string name, bool value)
        //{
        //    var oldValue = GetBoolValue(name);
        //    if (oldValue != value)
        //    {
        //        m_Config[name] = value;
        //        OnChanged(name, value);
        //    }
        //}

        public string GetStringValue(ConfigNames name)
        {
            var value = m_Config[name] ?? m_Default[name];
            return value == null ? string.Empty : (string)value;
        }

        //public void SetStringValue(string name, string value)
        //{
        //    var oldValue = GetStringValue(name);
        //    if (string.Compare(oldValue, value, StringComparison.CurrentCultureIgnoreCase) != 0)
        //    {
        //        m_Config[name] = value;
        //        OnChanged(name, value);
        //    }
        //}

        public void SetValue(ConfigNames name, object value)
        {
            var oldValue = (IComparable)(m_Config[name] ?? m_Default[name]);
            if (oldValue.CompareTo(value as IComparable) != 0)
            {
                m_Config[name] = value;
                OnChanged(name, value);
            }
        }

        public object GetDefaultValue(ConfigNames name)
        {
            return m_Default[name];
        }

        private void OnChanged(ConfigNames name, object value)
        {
            if (Changed != null)
            {
                var args = new ConfigChangedArgs(name, value);
                Changed(this, args);
                m_Config[name] = args.NewValue;
            }
            Modified = true;
        }

        public ConfigElement[] Elements
        {
            get
            {
                var result = new List<ConfigElement>();
                foreach(DictionaryEntry item in m_Config)
                {
                    var element = new ConfigElement();
                    element.Key = (ConfigNames)item.Key;
                    element.Value = item.Value.ToString();
                    result.Add(element);
                }
                return result.ToArray();
            }
            set
            {
                m_Config.Clear();
                foreach(var element in value)
                    m_Config.Add(element.Key, element.Value);
            }
        }

        // properties below must not be modified instantly

        //public bool IsAutorun
        //{
        //    get
        //    {
        //        return AutorunUtils.Autorun_Exists(App.FolderManager.ExeFileName);
        //    }
        //    set
        //    {
        //        var exeFName = App.FolderManager.ExeFileName;
        //        if (value)
        //        {
        //            AutorunUtils.Autorun_Add(exeFName);
        //        }
        //        else
        //        {
        //            AutorunUtils.Autorun_Delete(exeFName);
        //        }
        //    }
        //}
    }
}