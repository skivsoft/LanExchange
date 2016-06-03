using System;
using System.Xml.Serialization;

namespace LanExchange.Application.Interfaces
{
    [Serializable]
    public abstract class PanelItemRootBase : PanelItemBase
    {
        protected abstract string GetName();

        [XmlIgnore]
        public override string Name
        {
            get { return GetName(); }
            set
            {
            }
        }

        public override string ImageLegendText
        {
            get { return string.Empty; }
        }
    }
}