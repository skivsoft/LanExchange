using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Model.Settings
{
    public class Tab
    {
        public string Name { get; set; }
        public PanelViewMode View { get; set; }
        public ObjectPath<PanelItemBase> Path { get; set; }
        public PanelItemBase Focused { get; set; }
        public string Filter { get; set; }
        public PanelItemBase[] Items;

        public Tab()
        {
            View = PanelViewMode.Details;
            Items = new PanelItemBase[0];
        }
    }
}
