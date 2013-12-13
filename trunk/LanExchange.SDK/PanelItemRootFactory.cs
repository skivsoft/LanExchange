namespace LanExchange.SDK
{
    public class PanelItemRootFactory<TRoot> : PanelItemFactoryBase where TRoot : PanelItemRootBase, new()
    {

        public override PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new TRoot();
        }

        public override PanelItemBase CreateDefaultRoot()
        {
            return null;
        }

        public override void RegisterColumns(IPanelColumnManager columnManager)
        {
        }
    }
}