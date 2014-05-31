namespace LanExchange.SDK
{
    public interface IPanelItemFactory
    {
        PanelItemBase CreatePanelItem(PanelItemBase parent, string name);

        PanelItemBase CreateDefaultRoot();

        void RegisterColumns(IPanelColumnManager columnManager);
    }
}