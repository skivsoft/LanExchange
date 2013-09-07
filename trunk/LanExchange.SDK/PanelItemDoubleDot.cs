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

        public override object Clone()
        {
            return new PanelItemDoubleDot(Parent);
        }
    }
}