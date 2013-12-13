using System;

namespace LanExchange.SDK
{
    public sealed class ConfigChangedArgs : EventArgs
    {
        public ConfigChangedArgs(ConfigNames name)
        {
            Name = name;
        }

        public ConfigNames Name { get; private set; }
    }
}