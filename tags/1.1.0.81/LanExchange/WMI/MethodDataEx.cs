using System;
using System.Management;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.WMI
{
    internal class MethodDataEx
    {
        private readonly MethodData m_Data;

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="data"></param>
        public MethodDataEx(MethodData data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            m_Data = data;
        }

        internal bool HasQualifier(string qName)
        {
            foreach (var qd in m_Data.Qualifiers)
                if (qd.Name.Equals(qName))
                    return true;
            return false;
        }

        public override string ToString()
        {
            var list = new List<PropertyDataEx>();
            if (m_Data.InParameters != null)
                foreach (var pd in m_Data.InParameters.Properties)
                    list.Add(new PropertyDataEx(pd));
            if (m_Data.OutParameters != null)
                foreach (var pd in m_Data.OutParameters.Properties)
                    list.Add(new PropertyDataEx(pd));
            list.Sort();
            //string sReturn = string.Empty;
            var sb = new StringBuilder();
            foreach (var prop in list)
            {
                if (prop.ParamType == WMIParamType.RETURN)
                {
                    //sReturn = prop.Type.ToString();
                    continue;
                }
                if (sb.Length > 0)
                    sb.Append(", ");
                if (prop.ParamType == WMIParamType.OUT)
                    sb.Append("out ");
                //sb.Append(prop.Type);
                //sb.Append(" ");
                string s = prop.Name; 
                sb.Append(s.Substring(0, 1).ToLower());
                sb.Append(s.Substring(1));
            }
            return string.Format("{0}({1})", m_Data.Name, sb);
        }
    }
}
