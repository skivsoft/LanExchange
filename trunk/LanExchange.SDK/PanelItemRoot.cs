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
        public static readonly PanelItemBase ROOT_OF_USERITEMS = new PanelItemRoot("USERITEMS");

        private string m_ImageName;

        public PanelItemRoot()
        {
        }

        public PanelItemRoot(string name)
        {
            Name = name;
        }

        public override object Clone()
        {
            return new PanelItemRoot(Name);
        }

        [XmlAttribute]
        public override string Name { get; set; }

        public override string ImageName
        {
            get { return m_ImageName; }
        }

        public void SetImageName(string value)
        {
            m_ImageName = value;
        }

        public override string ImageLegendText
        {
            get { return string.Empty; }
        }
    }
}