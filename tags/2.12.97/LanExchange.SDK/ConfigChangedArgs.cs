using System;

namespace LanExchange.SDK
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