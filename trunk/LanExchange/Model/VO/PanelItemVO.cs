using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.Model.VO
{
    public class PanelItemVO
    {
        private string m_Name = "";
        private string m_Comment = "";

        public PanelItemVO(string name, string comment)
        {
            m_Name = name;
            m_Comment = comment;
        }


        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public string Comment
        {
            get { return m_Comment; }
            set { m_Comment = value; }
        }

    }
}
