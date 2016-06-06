using System.ComponentModel;
using System.Xml.Serialization;

namespace LanExchange.Presentation.Interfaces.Config
{
    /// <summary>
    /// Program settings. Implemented as Singleton.
    /// </summary>
    [XmlType("LanExchangeConfig")]
    public class MainConfig : ConfigBase
    {
        private bool runMinimized;
        private string language;
        private int mainFormX;
        private int mainFormWidth;

        [Localizable(false)]
        public MainConfig()
        {
            RunMinimized = true;
            Language = "English";
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
    }
}