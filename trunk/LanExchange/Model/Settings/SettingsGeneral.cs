using System.ComponentModel;
using LanExchange.SDK;
using LanExchange.Utils;

namespace LanExchange.Model.Settings
{
    public class SettingsGeneral : SettingsBase
    {
        [Description("Start program when user logon")]
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

        [Description("Minimize program on start")]
        public bool RunMinimized;

        [Description("Advanced features for administration")]
        public bool AdvancedMode;
    }
}
