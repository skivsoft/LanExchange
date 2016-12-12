using System;
using System.Xml.Serialization;

namespace LanExchange.Presentation.Interfaces
{
    [Serializable]
    public abstract class PanelItemRootBase : PanelItemBase
    {
        [XmlIgnore]
        public override string Name
        {
            get { return GetName(); }
            set { }
        }

        public override string ImageLegendText
        {
            get { return string.Empty; }
        }

        protected abstract string GetName();
    }
}