using System;
using System.Globalization;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network.Panel
{
    public class ComputerPanelItem : PanelItemBase, IWmiComputer
    {
        private ServerInfo m_SI;

        public ComputerPanelItem()
        {
            m_SI = new ServerInfo();
        }

        /// <summary>
        /// Constructor creates ComputerPanelItem from <see cref="ServerInfo"/> object.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public ComputerPanelItem(PanelItemBase parent, ServerInfo si) : base(parent)
        {
            m_SI = si ?? new ServerInfo();
        }

        public ComputerPanelItem(PanelItemBase parent, string name) : base(parent)
        {
            m_SI = new ServerInfo { Name = name };
        }

        public ServerInfo SI
        {
            get { return m_SI; }
        }

        public override string Name
        {
            get { return m_SI.Name; }
            set { m_SI.Name = value; }
        }

        public uint Platform
        {
            get { return m_SI.PlatformID; }
            set { m_SI.PlatformID = value; }
        }

        public string Ver
        {
            get { return string.Format("{0}.{1}", m_SI.VersionMajor, m_SI.VersionMinor); }
            set
            {
                var aValue = value.Split('.');
                if (aValue.Length == 2)
                {
                    uint uValue1;
                    uint uValue2;
                    if (uint.TryParse(aValue[0], out uValue1) && uint.TryParse(aValue[1], out uValue2))
                    {
                        m_SI.VersionMajor = uValue1;
                        m_SI.VersionMinor = uValue2;
                    }
                }
            }
        }

        public string Type
        {
            get { return m_SI.Type.ToString("X"); }
            set
            {
                uint uValue;
                if (uint.TryParse(value, NumberStyles.HexNumber, null, out uValue))
                    m_SI.Type = uValue;
            }
        }

        public string Comment
        {
            get { return m_SI.Comment; }
            set { m_SI.Comment = value; }
        }

        public override int CountColumns
        {
            get { return 3; }
        }

        public override PanelColumnHeader CreateColumnHeader(int index)
        {
            PanelColumnHeader result = null;
            switch (index)
            {
                case 0:
                    result = new PanelColumnHeader("Network name");
                    break;
                case 1:
                    result = new PanelColumnHeader("Description", 250);
                    break;
                case 2:
                    result = new PanelColumnHeader("OS version");
                    result.Visible = false;
                    break;
                //case 3:
                //    result = new PanelColumnHeader("IP address");
                //    result.Visible = false;
                //    break;
            }
            return result;
        }

        public override IComparable this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return Name;
                    case 1:
                        return Comment;
                    case 2:
                        return m_SI.Version();
                    //case 3:
                    //    var iph = Dns.GetHostEntry(Name);
                    //    return iph.AddressList[0].ToString();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public override string ImageName
        {
            get
            {
                if (Name == s_DoubleDot)
                    return PanelImageNames.DoubleDot;
                return IsReachable ? PanelImageNames.ComputerNormal : PanelImageNames.ComputerDisabled;
            }
        }

        public override string ImageLegendText
        {
            get
            {
                switch (ImageName)
                {
                    case PanelImageNames.ComputerNormal:
                        return "Computer was found in local area network.";
                    case PanelImageNames.ComputerDisabled:
                        return "Computer does not reachable via PING.";
                    default:
                        return string.Empty;
                }
            }
        }

        public override string ToolTipText
        {
            get
            {
                return String.Format("{0}\n{1}\n{2}", Comment, m_SI.Version(), m_SI.GetTopicalityText());
            }
        }

        public override string ToString()
        {
            return @"\\" + base.ToString();
        }
    }
}
