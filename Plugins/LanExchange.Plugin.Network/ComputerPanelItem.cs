using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;
using LanExchange.Plugin.Network.Properties;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Network
{
    [Serializable]
    public sealed class ComputerPanelItem : PanelItemBase
    {
        private readonly ServerInfo serverInfo;

        public ComputerPanelItem()
        {
            serverInfo = new ServerInfo();
        }

        public override int CountColumns
        {
            get { return base.CountColumns + 6; }
        }

        /// <summary>
        /// Constructor creates ComputerPanelItem from <see cref="ServerInfo"/> object.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public ComputerPanelItem(PanelItemBase parent, ServerInfo si) : base(parent)
        {
            serverInfo = si ?? new ServerInfo();
        }

        public ComputerPanelItem(PanelItemBase parent, string name) : base(parent)
        {
            serverInfo = new ServerInfo { Name = name };

       }

        public ServerInfo SI
        {
            get { return serverInfo; }
        }

        [XmlAttribute]
        public override string Name
        {
            get { return serverInfo.Name; }
            set { serverInfo.Name = value; }
        }

        [Localizable(false)]
        public override string FullName
        {
            get { return @"\\" + base.FullName; }
        }

        [XmlAttribute]
        public uint Platform
        {
            get { return serverInfo.Version.PlatformId; }
            set { serverInfo.Version.PlatformId = value; }
        }

        [Localizable(false)]
        [XmlAttribute]
        public string Ver
        {
            get { return string.Format("{0}.{1}", serverInfo.Version.Major, serverInfo.Version.Minor); }
            set
            {
                var aValue = value.Split('.');
                if (aValue.Length == 2)
                {
                    uint uValue1;
                    uint uValue2;
                    if (uint.TryParse(aValue[0], out uValue1) && uint.TryParse(aValue[1], out uValue2))
                    {
                        serverInfo.Version.Major = uValue1;
                        serverInfo.Version.Minor = uValue2;
                    }
                }
            }
        }

        [Localizable(false)]
        [XmlAttribute]
        public string Type
        {
            get { return serverInfo.Version.Type.ToString("X"); }
            set
            {
                uint uValue;
                if (uint.TryParse(value, NumberStyles.HexNumber, null, out uValue))
                    serverInfo.Version.Type = uValue;
            }
        }

        [XmlAttribute]
        public string Comment
        {
            get { return serverInfo.Comment; }
            set { serverInfo.Comment = value; }
        }

        [XmlIgnore]
        public string LoggedUsers { get; set; }

        public override IComparable GetValue(int index)
        {
            switch (index)
            {
                case 1: return Comment;
                case 2: return serverInfo.Version;
                case 3: return string.Empty;
                case 4: return string.Empty;
                case 5: return string.Empty;
                case 6: return string.Empty;
                default:
                    return base.GetValue(index);
            }
        }

        public override string ImageName
        {
            get
            {
                if (Parent == null)
                    return PanelImageNames.COMPUTER + PanelImageNames.GreenPostfix;
                return IsReachable ? PanelImageNames.COMPUTER : PanelImageNames.COMPUTER + PanelImageNames.RedPostfix;
            }
        }

        public override string ImageLegendText
        {
            get
            {
                switch (ImageName)
                {
                    case PanelImageNames.COMPUTER:
                        return Resources.ImageLegendText_ComputerNormal;
                    case PanelImageNames.COMPUTER + PanelImageNames.HiddenPostfix:
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
