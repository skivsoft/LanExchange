namespace LanExchange.SDK
{
    /// <summary>
    /// The ".." item
    /// </summary>
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
    }
}
