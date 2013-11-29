using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;
using LanExchange.Plugin.Network.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    [Serializable]
    public sealed class ComputerPanelItem : PanelItemBase
    {
        private readonly ServerInfo m_SI;

        public ComputerPanelItem()
        {
            m_SI = new ServerInfo();
        }

        public override int CountColumns
        {
            get { return base.CountColumns + 5; }
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

        [Localizable(false)]
        public override string FullName
        {
            get { return @"\\" + base.FullName; }
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
                case 5: return string.Empty;
                default:
                    return base.GetValue(index);
            }
        }

        public override string ImageName
        {
            get
            {
                if (Parent == null)
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

        public override object Clone()
        {
            var result = new ComputerPanelItem(Parent, SI);
            result.Comment = Comment;
            return result;
        }
    }
}
