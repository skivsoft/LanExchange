using System.Collections.Generic;
using LanExchange.Model.Panel;

namespace LanExchange.Model.Settings
{
    public class Tab// : IComparable<TabSettings>
    {
        public string Name { get; set; }
        public System.Windows.Forms.View View { get; set; }
        public List<string> Domains { get; set; }
        public string Focused { get; set; }
        public string Filter { get; set; }

        public Tab()
        {
            View = System.Windows.Forms.View.Details;
            Domains = new List<string>();
        }

        public void SetScanGroups(IList<ISubject> value)
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

        //public int CompareTo(TabSettings other)
        //{
        //    return string.Compare(Name, other.Name, true);
        //}
    }
}
