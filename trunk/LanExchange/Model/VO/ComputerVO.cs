using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.SDK.SDKModel.VO;

namespace LanExchange.Model.VO
{
    public class ComputerVO : PanelItemVO
    {
        private string m_Comment = "";
        private string m_Version = "";

        public ComputerVO(string name, object data)
            : base(name, data)
        {

        }

        public string Comment
        {
            get { return m_Comment; }
            set { m_Comment = value; }
        }

        public string Version
        {
            get { return m_Version; }
            set { m_Version = value; }
        }

    }
}
