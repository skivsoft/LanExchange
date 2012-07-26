using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.Model.VO
{
    public class IntVO
    {
        private int m_Value;

        public IntVO(int value)
        {
            m_Value = value;
        }

        public int Value
        {
            get { return m_Value; }
        }
    }
}
