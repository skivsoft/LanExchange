using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace LanExchange.WMI
{
    internal class MethodDataEx
    {
        private readonly MethodData m_Data;

        public MethodDataEx(MethodData data)
        {
            if (data == null)
                throw new ArgumentNullException();
            m_Data = data;
        }
    }
}
