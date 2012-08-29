using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model.VO;
using LanExchange.OSLayer;

namespace LanExchange.Model.VO
{
    public class ComputerVO : PanelItemVO
    {
        private string m_Comment = "";
        private ServerInfoVO m_SI = null;

        public ComputerVO(string name, object data)
            : base(name, data)
        {
            m_SI = data as ServerInfoVO;
            if (m_SI != null)
                m_Comment = m_SI.Comment;
        }

        public string Comment
        {
            get { return m_Comment; }
            set { m_Comment = value; }
        }

        public ServerInfoVO SI
        {
            get { return m_SI; }
        }
    }
}
