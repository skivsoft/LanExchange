namespace LanExchange.Model.Settings
{
    public class Tabs
    {
        public int SelectedIndex { get; set; }
        public Tab[] Items { get; set; }

        public Tabs()
        {
            Items = new Tab[0];
        }
    }
}
