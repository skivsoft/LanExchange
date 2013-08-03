using LanExchange.SDK;
using LanExchange.Utils;

namespace LanExchange.Model.Settings
{
    public class SettingsGeneral : SettingsBase
    {
        private bool m_RunMinimized;
        private bool m_AdvancedMode;

        public bool IsAutorun
        {
            get
            {
                return AutorunUtils.Autorun_Exists(Settings.GetExecutableFileName());
            }
            set
            {
                var exeFName = Settings.GetExecutableFileName();
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

        public bool RunMinimized
        {
            get { return m_RunMinimized; }
            set
            {
                if (m_RunMinimized != value)
                {
                    m_RunMinimized = value;
                    Modified = true;
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
                    Modified = true;
                }
            }
        }
    }
}
