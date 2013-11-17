using System;

namespace LanExchange.Intf
{
    public class ConfigChangedArgs : EventArgs
    {
        public ConfigChangedArgs(ConfigNames name)
        {
            Name = name;
        }

        public ConfigNames Name { get; private set; }
    }
}