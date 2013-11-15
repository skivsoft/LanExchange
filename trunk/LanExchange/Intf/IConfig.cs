using System;

namespace LanExchange.Intf
{
    public interface IConfig
    {
        event EventHandler<ConfigChangedArgs> Changed;

        int GetIntValue(ConfigNames name);
        bool GetBoolValue(ConfigNames name);
        string GetStringValue(ConfigNames name);
        
        void SetValue(ConfigNames name, object value);

        object GetDefaultValue(ConfigNames name);

        void Load();
    }
}