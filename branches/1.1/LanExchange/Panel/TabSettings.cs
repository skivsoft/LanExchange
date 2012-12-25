using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LanExchange
{
    public class TabSettings
    {
        public string Name { get; set; }
        public System.Windows.Forms.View CurrentView { get; set; }
        public PanelItemList.PanelScanMode ScanMode { get; set; }
        public List<string> ScanGroups { get; set; }
        public string Filter { get; set; }

        public TabSettings()
        {
            CurrentView = System.Windows.Forms.View.Details;
            ScanGroups = new List<string>();
        }
    }
}
