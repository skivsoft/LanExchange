using System;

namespace LanExchange.Intf
{
    public class ConfigChangedArgs : EventArgs
    {
        public ConfigChangedArgs(ConfigNames name, object value)
        {
            Name = name;
            Value = value;
            NewValue = value;
        }

        public ConfigNames Name { get; private set; }

        public object Value { get; private set; }

        public object NewValue { get; set; }
    }
}