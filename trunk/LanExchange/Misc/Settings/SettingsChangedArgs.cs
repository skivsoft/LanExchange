using System;

namespace LanExchange.Model.Settings
{
    public class SettingsChangedArgs : EventArgs
    {
        private readonly string m_Name;
        private readonly object m_Value;

        public SettingsChangedArgs(string name, object value)
        {
            m_Name = name;
            m_Value = value;
            NewValue = value;
        }

        public string Name
        {
            get { return m_Name; }
        }

        public object Value
        {
            get { return m_Value; }
        }

        public object NewValue { get; set; }
    }
}