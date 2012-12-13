using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LanExchange
{
    public class TabSettings
    {
        public string Name { get; set; }
        public View CurrentView { get; set; }
        public PanelItemList.PanelScanMode ScanMode { get; set; }
        public List<string> ScanGroups { get; set; }
        public string Filter { get; set; }

        public TabSettings()
        {
            CurrentView = View.Details;
            ScanGroups = new List<string>();
        }
    }
}
