using System.Collections.Generic;
using LanExchange.Model.Panel;
using LanExchange.Utils;

namespace LanExchange.Model.Settings
{
    public class Tab
    {
        public string Name { get; set; }
        public PanelItemList.View View { get; set; }
        public List<string> Domains { get; set; }
        public ServerInfo[] Items { get; set; }
        public string Focused { get; set; }
        public string Filter { get; set; }

        public Tab()
        {
            View = PanelItemList.View.Details;
            Domains = new List<string>();
            Items = new ServerInfo[0];
        }

        public void SetScanGroups(IEnumerable<ISubject> value)
        {
            Domains.Clear();
            foreach(var item in value)
                Domains.Add(item.Subject);
        }

        public IList<ISubject> GetScanGroups()
        {
            var result = new List<ISubject>();
            foreach(var domain in Domains)
                result.Add(new DomainPanelItem(domain));
            return result;
        }
    }
}
