using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model.VO;
using LanExchange.OSLayer;

namespace ModelNetwork.Model.VO
{
    public class ComputerVO : PanelItemVO
    {
        private string m_Comment = "";
        private string m_Version = "";
        private NetApi32.SERVER_INFO_101 m_SI;

        public ComputerVO(string name, object data)
            : base(name, data)
        {
            m_SI = (NetApi32.SERVER_INFO_101)data;
            m_Comment = m_SI.sv101_comment;
            m_Version = m_SI.Version();
        }

        public override string[] SubItems
        {
            get
            {
                return new string[2] { m_Comment, m_Version };
            }
        }

        public override int CompareTo(PanelItemVO obj, int Index)
        {
            if (Index != 2)
                return base.CompareTo(obj, Index);
            ComputerVO comp = obj as ComputerVO;
            uint u1 = m_SI.sv101_platform_id;
            uint u2 = comp.m_SI.sv101_platform_id;
            if (u1 < u2) return -1;
            else 
                if (u1 > u2) return 1;
            bool s1 = m_SI.IsServer();
            bool c1 = m_SI.IsDomainController();
            bool s2 = comp.m_SI.IsServer();
            bool c2 = comp.m_SI.IsDomainController();
            if (!s1 && s2) return -1;
            else
                if (s1 && !s2) return 1;
                else
                    if (!c1 && c2) return 1;
                    else
                        if (c1 && !c2) return -1;
            u1 = m_SI.sv101_version_major;
            u2 = comp.m_SI.sv101_version_major;
            if (u1 < u2) return -1;
            else
                if (u1 > u2) return 1;
            u1 = m_SI.sv101_version_minor;
            u2 = comp.m_SI.sv101_version_minor;
            if (u1 < u2) return -1;
            else
                if (u1 > u2) return 1;
                else
                    return 0;
        }


        public string Comment
        {
            get { return m_Comment; }
            set { m_Comment = value; }
        }

        public string Version
        {
            get { return m_Version; }
        }

    }
}
