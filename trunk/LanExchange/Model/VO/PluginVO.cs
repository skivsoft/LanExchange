using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.SDK.SDKModel.VO;
using LanExchange.SDK;

namespace LanExchange.Model.VO
{
    public class PluginVO : PanelItemVO
    {
        private string m_Version = "";
        private string m_Author = "";
        private string m_Description = "";

        public PluginVO(string name, string version, string desc, string author)
            : base(name, null)
        {
            m_Version = version;
            m_Description = desc;
            m_Author = author;
        }

        public override string[] SubItems
        {
            get
            {
                return new string[3] { m_Version, m_Description, m_Author };
            }
        }
    }
}
