using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace LanExchange.Model.VO
{
    public class PanelItemVO
    {
        private string m_Name = String.Empty;
        private object m_Data = null;

        public PanelItemVO(string name, object data)
        {
            m_Name = name;
            m_Data = data;
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
    }
}
