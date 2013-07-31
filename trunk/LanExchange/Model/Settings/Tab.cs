using System.Collections.Generic;
using LanExchange.Model.Panel;
using LanExchange.SDK;
using LanExchange.Utils;

namespace LanExchange.Model.Settings
{
    public class Tab
    {
        public string Name { get; set; }
        public PanelViewMode View { get; set; }
        public List<string> Domains { get; set; }
        // TODO NEED UNCOMMENT THIS!
        //public ServerInfo[] Items { get; set; }
        public string Focused { get; set; }
        public string Filter { get; set; }

        public Tab()
        {
            View = PanelViewMode.Details;
            Domains = new List<string>();
            // TODO NEED UNCOMMENT THIS!
            //Items = new ServerInfo[0];
        }
    }
}
