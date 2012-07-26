using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.Model.VO
{
    public class StringVO
    {
        private string m_Value = "";

        public StringVO(string value)
        {
            m_Value = value;
        }

        public string Value
        {
            get { return m_Value; }
        }
    }
}
