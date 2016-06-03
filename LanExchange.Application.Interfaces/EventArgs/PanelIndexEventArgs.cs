namespace LanExchange.Application.Interfaces.EventArgs
{
    public sealed class PanelIndexEventArgs : System.EventArgs
    {
        public PanelIndexEventArgs(int index)
        {
            Index = index;
        }

        public int Index { get; }
    }
}