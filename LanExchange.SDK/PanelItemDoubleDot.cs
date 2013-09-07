using System;

namespace LanExchange.SDK
{
    /// <summary>
    /// The ".." item
    /// </summary>
    [Serializable]
    public sealed class PanelItemDoubleDot : PanelItemBase
    {
        public PanelItemDoubleDot(PanelItemBase parent)
        {
            Parent = parent;
        }

        public override string ImageName
        {
            get { return PanelImageNames.DoubleDot; }
        }

        public override string ImageLegendText
        {
            get { return string.Empty; }
        }

        public override object Clone()
        {
            return new PanelItemDoubleDot(Parent);
        }

        public override string Name { get; set; }
    }
}