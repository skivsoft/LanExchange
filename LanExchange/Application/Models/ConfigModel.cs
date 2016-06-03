using System.ComponentModel;
using System.Xml.Serialization;
using LanExchange.Application.Interfaces.Services;

namespace LanExchange.Application.Models
{
    /// <summary>
    /// Program settings. Implemented as Singleton.
    /// </summary>
    [XmlType("LanExchangeConfig")]
    public class ConfigModel : ConfigBase
    {
        private bool runMinimized;
        private bool advancedMode;
        private string language;
        private int mainFormX;
        private int mainFormWidth;
        private bool showGridLines;

        [Localizable(false)]
        public ConfigModel()
        {
            RunMinimized = true;
            Language = "English";
            ShowGridLines = true;
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