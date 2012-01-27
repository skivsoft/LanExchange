using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LanExchange
{
    public class TTabInfo
    {
        public string TabName = "";
        public string FilterText = "";
        public View CurrentView = View.Details;
        public List<string> Items = null;
        public List<string> SelectedItems = null;

        public TTabInfo()
        {
        }

        protected TabPage GetAsControl()
        {
            return null;
        }

        protected void SetAsControl(TabPage value)
        {
            ListView LV = (ListView)value.Controls[0];
            TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
            this.Items = ItemList.ListView_GetSelected(LV, true);
            this.SelectedItems = ItemList.ListView_GetSelected(LV, false);
            this.TabName = value.Text;
            this.FilterText = ItemList.FilterText;
            this.CurrentView = LV.View;
        }

        public TabPage AsControl
        {
            get { return GetAsControl(); }
            set { SetAsControl(value); }
        }
    }

    public class TTabInfoList
    {
        public List<TTabInfo> InfoList = new List<TTabInfo>();
        public string SelectedTabName = "";
        public int Count = 0;

        protected TabControl GetAsControl()
        {
            return null;
        }

        protected void SetAsControl(TabControl value)
        {
            this.SelectedTabName = value.SelectedTab.Text;
            this.Count = value.TabCount;
            foreach (TabPage Tab in value.TabPages)
            {
                TTabInfo Info = new TTabInfo();
                Info.AsControl = Tab;
                InfoList.Add(Info);
            }
        }

        public TabControl AsControl
        {
            get { return GetAsControl(); }
            set { SetAsControl(value); }
        }
    }
}
