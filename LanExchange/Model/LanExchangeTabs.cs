namespace LanExchange.Model
{
    public class LanExchangeTabs
    {
        public int SelectedIndex { get; set; }
        public TabSettings[] Items { get; set; }

        public LanExchangeTabs()
        {
            Items = new TabSettings[0];
        }
    }
}
