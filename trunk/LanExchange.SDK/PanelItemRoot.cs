using System;
using System.Xml.Serialization;

namespace LanExchange.SDK
{
    /// <summary>
    /// The single class for any root item of plugin.
    /// </summary>
    [Serializable]
    public sealed class PanelItemRoot : PanelItemBase
    {
        public static readonly PanelItemBase ROOT_OF_USERITEMS = new PanelItemRoot();

        public override object Clone()
        {
            return new PanelItemRoot();
        }

        [XmlAttribute]
        public override string Name { get; set; }

        public override string ImageName
        {
            get { return string.Empty; }
        }

        public override string ImageLegendText
        {
            get { return string.Empty; }
        }
    }
}