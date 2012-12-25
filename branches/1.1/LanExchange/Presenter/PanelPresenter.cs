using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.View;

namespace LanExchange.Presenter
{
    public class PanelPresenter
    {
        private readonly IPanelView m_View;

        public PanelPresenter(IPanelView view)
        {
            m_View = view;

        }
        private void mCopyCompName_Click(object sender, EventArgs e)
        {
            //ListView LV = GetActiveListView();
            //PanelItemList ItemList = PanelItemList.GetObject(LV);
            //StringBuilder S = new StringBuilder();
            //foreach (int index in LV.SelectedIndices)
            //{
            //    if (S.Length > 0)
            //        S.AppendLine();
            //    S.Append(@"\\" + ItemList.Keys[index]);
            //}
            //if (S.Length > 0)
            //    Clipboard.SetText(S.ToString());
        }

        private void mCopyPath_Click(object sender, EventArgs e)
        {
            //ListView LV = GetActiveListView();
            //PanelItemList ItemList = PanelItemList.GetObject(LV);
            //StringBuilder S = new StringBuilder();
            //string ItemName = null;
            //SharePanelItem PItem = null;
            //foreach (int index in LV.SelectedIndices)
            //{
            //    if (S.Length > 0)
            //        S.AppendLine();
            //    ItemName = ItemList.Keys[index];
            //    if (!String.IsNullOrEmpty(ItemName))
            //    {
            //        PItem = ItemList.Get(ItemName) as SharePanelItem;
            //        if (PItem != null)
            //            S.Append(String.Format(@"\\{0}\{1}", PItem.ComputerName, PItem.Name));
            //    }
            //}
            //if (S.Length > 0)
            //    Clipboard.SetText(S.ToString());
        }

        private void mCopyComment_Click(object sender, EventArgs e)
        {
            //ListView LV = GetActiveListView();
            //PanelItemList ItemList = PanelItemList.GetObject(LV);
            //StringBuilder S = new StringBuilder();
            //PanelItem PItem = null;
            //foreach (int index in LV.SelectedIndices)
            //{
            //    if (S.Length > 0)
            //        S.AppendLine();
            //    PItem = ItemList.Get(ItemList.Keys[index]);
            //    if (PItem != null)
            //        S.Append(PItem.Comment);
            //}
            //if (S.Length > 0)
            //    Clipboard.SetText(S.ToString());
        }

        private void mCopySelected_Click(object sender, EventArgs e)
        {
            //ListView LV = GetActiveListView();
            //PanelItemList ItemList = PanelItemList.GetObject(LV);
            //StringBuilder S = new StringBuilder();
            //string ItemName = null;
            //PanelItem PItem = null;
            //foreach (int index in LV.SelectedIndices)
            //{
            //    if (S.Length > 0)
            //        S.AppendLine();
            //    ItemName = ItemList.Keys[index];
            //    PItem = ItemList.Get(ItemName);
            //    if (PItem != null)
            //    {
            //        S.Append(@"\\" + ItemName);
            //        S.Append("\t");
            //        S.Append(PItem.Comment);
            //    }
            //}
            //if (S.Length > 0)
            //    Clipboard.SetText(S.ToString());
        }
    }
}
