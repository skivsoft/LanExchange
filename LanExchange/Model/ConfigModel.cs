using LanExchange.SDK;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace LanExchange.Model
{
    /// <summary>
    /// Program settings. Implemented as Singleton.
    /// </summary>
    [XmlType("LanExchangeConfig")]
    public class ConfigModel : ConfigBase
    {
        private const int MIN_INFO_LINES = 3;
        private const int MAX_INFO_LINES = 7;

        private bool showInfoPanel;
        private bool runMinimized;
        private bool advancedMode;
        private int numInfoLines;
        private string language;
        private int mainFormX;
        private int mainFormWidth;
        private bool showGridLines;

        [Localizable(false)]
        public ConfigModel()
        {
            ShowInfoPanel = true;
            RunMinimized = true;
            NumInfoLines = 3;
            Language = "English";
            ShowGridLines = true;
        }

        [DefaultValue(true)]
        public bool ShowInfoPanel
        {
            get { return showInfoPanel; } 
            set
            {
                if (showInfoPanel != value)
                {
                    showInfoPanel = value;
                    OnChanged(nameof(ShowInfoPanel));
                }
            }
        }

        [DefaultValue(true)]
        public bool RunMinimized
        {
            get { return runMinimized; }
            set
            {
                if (runMinimized != value)
                {
                    runMinimized = value;
                    OnChanged(nameof(RunMinimized));
                }
            }
        }

        public bool AdvancedMode
        {
            get { return advancedMode; }
            set
            {
                if (advancedMode != value)
                {
                    advancedMode = value;
                    OnChanged(nameof(AdvancedMode));
                }
            }
        }

        [DefaultValue(MIN_INFO_LINES)]
        public int NumInfoLines
        {
            get { return numInfoLines; }
            set
            {
                if (value < MIN_INFO_LINES)
                    value = MIN_INFO_LINES;
                if (value > MAX_INFO_LINES)
                    value = MAX_INFO_LINES;
                if (numInfoLines != value)
                {
                    numInfoLines = value;
                    OnChanged(nameof(NumInfoLines));
                }
            }
        }

        [DefaultValue("English")]
        public string Language
        {
            get { return language; }
            set
            {
                if (language != value)
                {
                    language = value;
                    OnChanged(nameof(Language));
                }
            }
        }

        public int MainFormX
        {
            get { return mainFormX; }
            set
            {
                if (mainFormX != value)
                {
                    mainFormX = value;
                    OnChanged(nameof(MainFormX));
                }
            }
        }

        public int MainFormWidth
        {
            get { return mainFormWidth; }
            set
            {
                if (mainFormWidth != value)
                {
                    mainFormWidth = value;
                    OnChanged(nameof(MainFormWidth));
                }
            }
        }

        [DefaultValue(true)]
        public bool ShowGridLines
        {
            get { return showGridLines; }
            set
            {
                if (showGridLines != value)
                {
                    showGridLines = value;
                    OnChanged(nameof(ShowGridLines));
                }
            }
        }
    }
}