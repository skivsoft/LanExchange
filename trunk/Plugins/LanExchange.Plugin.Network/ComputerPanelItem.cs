using System;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Xml.Serialization;
using LanExchange.Plugin.Network.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    [Serializable]
    public sealed class ComputerPanelItem : PanelItemBase
    {
        private readonly ServerInfo m_SI;

        public static void RegisterColumns(IPanelColumnManager columnManager)
        {
            columnManager.RegisterColumn(typeof(ComputerPanelItem), new PanelColumnHeader(Resources.NetworkName));
            columnManager.RegisterColumn(typeof(ComputerPanelItem), new PanelColumnHeader(Resources.Description, 250));
            columnManager.RegisterColumn(typeof(ComputerPanelItem), new PanelColumnHeader(Resources.OSVersion) { Visible=false, Width=110 });
            // lazy columns
            columnManager.RegisterColumn(typeof(ComputerPanelItem), new PanelColumnHeader(Resources.Ping) { Callback = GetReachable, Visible = false, Width = 110, Refreshable = true });
            columnManager.RegisterColumn(typeof(ComputerPanelItem), new PanelColumnHeader(Resources.IPAddress) { Callback = GetIPAddress, Visible = false, Width = 80 });
            columnManager.RegisterColumn(typeof(ComputerPanelItem), new PanelColumnHeader(Resources.MACAddress) { Callback = GetMACAddress, Visible = false, Width = 110 });
        }

        private static IPAddress InternalGetIPAddress(string computerName)
        {
            return Dns.GetHostEntry(computerName).AddressList[0];
        }

        [Localizable(false)]
        public static IComparable GetReachable(PanelItemBase item)
        {
            var ipAddr = InternalGetIPAddress(item.Name);
            string result = string.Empty;
            using (var ping = new Ping())
            {
                var pingReply = ping.Send(ipAddr);
                if (pingReply == null)
                    item.IsReachable = false;
                else if (pingReply.Status == IPStatus.Success)
                {
                    result = pingReply.RoundtripTime == 0 ? Resources.OK : string.Format(Resources.OK_ms, pingReply.RoundtripTime);
                    item.IsReachable = true;
                }
                else
                {
                    result = pingReply.Status.ToString();
                    if (result.StartsWith("Destination"))
                        result = result.Substring("Destination".Length);
                    item.IsReachable = false;
                }
            }
            return result;
        }

        /// <summary>
        /// Returns ip addres for specified computer "item".
        /// </summary>
        /// <param name="item">ComputerPanelItem object.</param>
        /// <returns>IPAddressComparable object.</returns>
        public static IComparable GetIPAddress(PanelItemBase item)
        {
            return new IPAddressComparable(InternalGetIPAddress(item.Name));
        }

        /// <summary>
        /// Returns MAC address for specified computer "item".
        /// Sends ARP request to computer's ip address.
        /// URL: http://www.codeproject.com/KB/IP/host_info_within_network.aspx
        /// </summary>
        /// <param name="item">ComputerPanelItem object.</param>
        /// <returns>MAC-address string.</returns>
        public static IComparable GetMACAddress(PanelItemBase item)
        {
            var ipAddr = InternalGetIPAddress(item.Name);
            var ab = new byte[6];
            int len = ab.Length;
            NativeMethods.SendARP(ipAddr.GetHashCode(), 0, ab, ref len);
            return BitConverter.ToString(ab, 0, 6);
        }

        public ComputerPanelItem()
        {
            m_SI = new ServerInfo();
        }

        public override int CountColumns
        {
            get { return base.CountColumns + 4; }
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

        [XmlAttribute]
        public override string Name
        {
            get { return m_SI.Name; }
            set { m_SI.Name = value; }
        }

        [XmlAttribute]
        public uint Platform
        {
            get { return m_SI.Version.PlatformID; }
            set { m_SI.Version.PlatformID = value; }
        }

        [Localizable(false)]
        [XmlAttribute]
        public string Ver
        {
            get { return string.Format("{0}.{1}", m_SI.Version.Major, m_SI.Version.Minor); }
            set
            {
                var aValue = value.Split('.');
                if (aValue.Length == 2)
                {
                    uint uValue1;
                    uint uValue2;
                    if (uint.TryParse(aValue[0], out uValue1) && uint.TryParse(aValue[1], out uValue2))
                    {
                        m_SI.Version.Major = uValue1;
                        m_SI.Version.Minor = uValue2;
                    }
                }
            }
        }

        [Localizable(false)]
        [XmlAttribute]
        public string Type
        {
            get { return m_SI.Version.Type.ToString("X"); }
            set
            {
                uint uValue;
                if (uint.TryParse(value, NumberStyles.HexNumber, null, out uValue))
                    m_SI.Version.Type = uValue;
            }
        }

        [XmlAttribute]
        public string Comment
        {
            get { return m_SI.Comment; }
            set { m_SI.Comment = value; }
        }

        public override IComparable GetValue(int index)
        {
            switch (index)
            {
                case 1: return Comment;
                case 2: return m_SI.Version;
                case 3: return string.Empty;
                case 4: return string.Empty;
                default:
                    return base.GetValue(index);
            }
        }

        public override string ImageName
        {
            get
            {
                if (Parent == PanelItemRoot.ROOT_OF_USERITEMS)
                    return PanelImageNames.ComputerCustom;
                return IsReachable ? PanelImageNames.ComputerNormal : PanelImageNames.ComputerUnreachable;
            }
        }

        public override string ImageLegendText
        {
            get
            {
                switch (ImageName)
                {
                    case PanelImageNames.ComputerNormal:
                        return Resources.ImageLegendText_ComputerNormal;
                    case PanelImageNames.ComputerDisabled:
                        return Resources.ImageLegendText_ComputerDisabled;
                    default:
                        return string.Empty;
                }
            }
        }

        [Localizable(false)]
        public override string ToolTipText
        {
            get
            {
                return String.Format("{0}\n{1}\n{2}", Comment, m_SI.Version, m_SI.GetTopicalityText());
            }
        }

        [Localizable(false)]
        public override string ToString()
        {
            return @"\\" + base.ToString();
        }

        public override object Clone()
        {
            var result = new ComputerPanelItem(Parent, SI);
            result.Comment = Comment;
            return result;
        }
    }
}
