namespace LanExchange.SDK
{
    public abstract class PanelItemBaseFactory
    {
        public abstract PanelItemBase CreatePanelItem(PanelItemBase parent, string name);
        
        public virtual PanelItemBase CreateDefaultRoot()
        {
            return null;
        }
    }
}
