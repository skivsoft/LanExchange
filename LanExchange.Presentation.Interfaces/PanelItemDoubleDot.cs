using System;
using System.Xml.Serialization;

namespace LanExchange.Presentation.Interfaces
{
    /// <summary>
    /// The ".." item
    /// </summary>
    [Serializable]
    public sealed class PanelItemDoubleDot : PanelItemBase
    {
        public PanelItemDoubleDot()
        {
        }
        
        public PanelItemDoubleDot(PanelItemBase parent) : base(parent)
        {
        }

        public override string ImageName
        {
            get { return PanelImageNames.DOUBLEDOT; }
        }

        public override string ImageLegendText
        {
            get { return string.Empty; }
        }

        [XmlAttribute]
        public override string Name { get; set; }

        public override object Clone()
        {
            return new PanelItemDoubleDot(Parent);
        }
    }
}