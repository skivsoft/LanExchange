namespace LanExchange.SDK
{
    public abstract class PanelItemFactoryBase
    {
        public abstract PanelItemBase CreatePanelItem(PanelItemBase parent, string name);

        public abstract PanelItemBase CreateDefaultRoot();

        public abstract void RegisterColumns(IPanelColumnManager columnManager);
    }
}