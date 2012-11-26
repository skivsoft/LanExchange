using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LanExchange
{
    public class ListViewEx : ListView
    {

        public ListViewEx()
        {
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
        }

        public void SetObject(PanelItemList ItemList)
        {
            PanelItemList List = GetObject();
            this.Tag = ItemList;
            List = null;
        }

        public PanelItemList GetObject()
        {
            return this.Tag as PanelItemList;
        }

        public PanelItemList CreateObject()
        {
            PanelItemList Result = new PanelItemList();
            this.SetObject(Result);
            return Result;
        }
    }
}
