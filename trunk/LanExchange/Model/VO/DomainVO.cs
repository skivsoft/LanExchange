using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model.VO;
using LanExchange.OSLayer;

namespace LanExchange.Model.VO
{
    public class DomainVO : PanelItemVO
    {
        private string m_MasterBrowser = "";

        public DomainVO(string name, string master)
            : base(name, null)
        {
            m_MasterBrowser = master;
        }

        public string MasterBrowser
        {
            get { return m_MasterBrowser; }
            set { m_MasterBrowser = value; }
        }
    }
}
