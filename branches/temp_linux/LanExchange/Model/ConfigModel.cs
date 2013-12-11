using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using LanExchange.Misc;
using LanExchange.SDK;
using LanExchange.SDK.Model;

namespace LanExchange.Model
{
    /// <summary>
    /// Program settings. Implemented as Singleton.
    /// </summary>
    [XmlType("LanExchangeConfig")]
    public class ConfigModel : IConfigModel
    {
        private const int MIN_INFO_LINES = 3;
        public event EventHandler<ConfigChangedArgs> Changed;
        private bool m_ShowInfoPanel;
        private bool m_RunMinimized;
        private bool m_AdvancedMode;
        private int m_NumInfoLines;
        private string m_Language;
        private int m_MainFormX;
        private int m_MainFormWidth;
        private bool m_ShowGridLines;

        [Localizable(false)]
        public ConfigModel()
        {
            ShowInfoPanel = true;
            RunMinimized = true;
            NumInfoLines = 3;
            Language = "English";
            ShowGridLines = true;
        }

        public void Load()
        {
            var fileName = App.FolderManager.ConfigFileName;
            if (!File.Exists(fileName)) return;
            try
            {
                var temp = (ConfigModel)SerializeUtils.DeserializeObjectFromXMLFile(fileName, typeof(ConfigModel));
                if (temp != null)
                    ReflectionUtils.CopyObjectProperties(temp, this);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }


        public void Save()
        {
            var fileName = App.FolderManager.ConfigFileName;
            try
            {
                SerializeUtils.SerializeObjectToXMLFile(fileName, this);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        private void OnChanged(ConfigNames name)
        {
            if (Changed != null)
                Changed(this, new ConfigChangedArgs(name));
        }

        [DefaultValue(true)]
        public bool ShowInfoPanel
        {
            get { return m_ShowInfoPanel; } 
            set
            {
                if (m_ShowInfoPanel != value)
                {
                    m_ShowInfoPanel = value;
                    OnChanged(ConfigNames.ShowInfoPanel);
                }
            }
        }

        [DefaultValue(true)]
        public bool RunMinimized
        {
            get { return m_RunMinimized; }
            set
            {
                if (m_RunMinimized != value)
                {
                    m_RunMinimized = value;
                    OnChanged(ConfigNames.RunMinimized);
                }
            }
        }

        public bool AdvancedMode
        {
            get { return m_AdvancedMode; }
            set
            {
                if (m_AdvancedMode != value)
                {
                    m_AdvancedMode = value;
                    OnChanged(ConfigNames.AdvancedMode);
                }
            }
        }

        [DefaultValue(MIN_INFO_LINES)]
        public int NumInfoLines
        {
            get { return m_NumInfoLines; }
            set
            {
                if (value < MIN_INFO_LINES)
                    value = MIN_INFO_LINES;
                if (App.PanelColumns != null)
                {
                    var maxColumns = Math.Max(MIN_INFO_LINES, App.PanelColumns.MaxColumns);
                    if (value > maxColumns)
                        value = maxColumns;
                }
                if (m_NumInfoLines != value)
                {
                    m_NumInfoLines = value;
                    OnChanged(ConfigNames.NumInfoLines);
                }
            }
        }

        [DefaultValue("English")]
        public string Language
        {
            get { return m_Language; }
            set
            {
                if (m_Language != value)
                {
                    m_Language = value;
                    OnChanged(ConfigNames.Language);
                }
            }
        }

        public int MainFormX
        {
            get { return m_MainFormX; }
            set
            {
                if (m_MainFormX != value)
                {
                    m_MainFormX = value;
                    OnChanged(ConfigNames.MainFormX);
                }
            }
        }

        public int MainFormWidth
        {
            get { return m_MainFormWidth; }
            set
            {
                if (m_MainFormWidth != value)
                {
                    m_MainFormWidth = value;
                    OnChanged(ConfigNames.MainFormWidth);
                }
            }
        }

        [DefaultValue(true)]
        public bool ShowGridLines
        {
            get { return m_ShowGridLines; }
            set
            {
                if (m_ShowGridLines != value)
                {
                    m_ShowGridLines = value;
                    OnChanged(ConfigNames.ShowGridLines);
                }
            }
        }
    }
}