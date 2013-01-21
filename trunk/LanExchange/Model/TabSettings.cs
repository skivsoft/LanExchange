using System.Collections.Generic;

namespace LanExchange.Model
{
    public class TabSettings// : IComparable<TabSettings>
    {
        public string Name { get; set; }
        public System.Windows.Forms.View CurrentView { get; set; }
        public bool ScanMode { get; set; }
        public IList<string> ScanGroups { get; set; }
        public string Filter { get; set; }

        public TabSettings()
        {
            CurrentView = System.Windows.Forms.View.Details;
            ScanGroups = new List<string>();
        }

        //public int CompareTo(TabSettings other)
        //{
        //    return string.Compare(Name, other.Name, true);
        //}
    }
}
