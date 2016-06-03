namespace LanExchange.Presentation.Interfaces.EventArgs
{
    public sealed class DataReadyArgs : System.EventArgs
    {
        public DataReadyArgs(PanelItemBase item)
        {
            Item = item;
        }

        public PanelItemBase Item { get; private set; }
    }
}