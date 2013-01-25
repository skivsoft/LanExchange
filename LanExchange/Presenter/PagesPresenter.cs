using System;
using System.Windows.Forms;
using LanExchange.Model;
using LanExchange.View;
using LanExchange.UI;
using LanExchange.Model.Panel;
using LanExchange.Utils;

namespace LanExchange.Presenter
{
    public class PagesPresenter
    {
        private readonly IPagesView m_View;
        private readonly PagesModel m_Model;

        public event EventHandler PanelViewFocusedItemChanged;
        public event EventHandler PanelViewFilterTextChanged;

        public PagesPresenter(IPagesView pages)
        {
            m_View = pages;
            m_Model = new PagesModel();
            m_Model.AfterAppendTab += Model_AfterAppendTab;
            m_Model.AfterRemove += Model_AfterRemove;
            m_Model.AfterRename += Model_AfterRename;
            m_Model.IndexChanged += Model_IndexChanged;
        }

        public PagesModel GetModel()
        {
            return m_Model;
        }

        // TODO: Need check duplicates on new tab
        public void CommandNewTab()
        {
            var newTabName = InputBoxForm.Ask("Новая вкладка", "Введите имя", "", false);
            if (newTabName != null)
            {
                m_Model.AddTab(new PanelItemList(newTabName));
                m_Model.SaveSettings();
            }
        }

        public void CommandSendToNewTab()
        {
            var newTabName = InputBoxForm.Ask("Новая вкладка", "Введите имя", "", false);
            if (newTabName == null) return;
            var sourcePV = m_View.GetActivePanelView() as PanelView;
            if (sourcePV == null) return;
            var sourceObjects = sourcePV.GetPresenter().Objects;
            var destObjects = new PanelItemList(newTabName);
            foreach (int index in sourcePV.SelectedIndices)
            {
                var PItem = sourceObjects.GetAt(index);
                if (PItem == null || PItem.Name == AbstractPanelItem.BACK) 
                    continue;
                if (!(PItem is ComputerPanelItem))
                    continue;
                // copy computer and set partent to null
                var newItem = new ComputerPanelItem(null, (PItem as ComputerPanelItem).SI);
                newItem.ParentSubject = ConcreteSubject.UserItems;
                // add item to new panel
                destObjects.Items.Add(newItem);
            }
            // add tab
            m_Model.AddTab(destObjects);
            m_View.SetSelectedIndex(m_Model.Count-1);
            m_Model.SaveSettings();
        }

        public void CommandCloseTab()
        {
            int Index = m_View.PopupSelectedIndex;
            if (CanCloseTab())
            {
                m_Model.DelTab(Index);
                m_Model.SaveSettings();
            }
        }

        // TODO: Need check duplicates on rename tab
        public void CommandRenameTab()
        {
            int Index = m_View.PopupSelectedIndex;
            PanelItemList ItemList = GetModel().GetItem(Index);
            if (ItemList == null) return;
            string NewTabName = InputBoxForm.Ask("Переименование", "Введите имя", ItemList.TabName, false);
            if (NewTabName != null)
            {
                m_Model.RenameTab(Index, NewTabName);
                m_Model.SaveSettings();
            }
        }


        internal void AddTabsToMenuItem(ToolStripMenuItem menuitem, EventHandler handler, bool bHideActive)
        {
            for (int i = 0; i < m_Model.Count; i++)
            {
                if (bHideActive && (!CanCloseTab() || (i == m_View.GetSelectedIndex())))
                    continue;
                string S = m_Model.GetTabName(i);
                var Item = new ToolStripMenuItem
                {
                    Checked = (i == m_View.GetSelectedIndex()),
                    Text = m_View.Ellipsis(S, 20),
                    ToolTipText = S,
                    Tag = i
                };
                Item.Click += handler;
                menuitem.DropDownItems.Add(Item);
            }
        }

        public void mSelectTab_Click(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;
            if (menu != null && m_View != null)
            {
                int Index = (int)menu.Tag;
                if (m_View.GetSelectedIndex() != Index)
                    m_View.SetSelectedIndex(Index);
            }
        }

        internal bool CanCloseTab()
        {
            return m_View.TabPagesCount > 1;
        }

        public void Model_AfterAppendTab(object sender, PanelItemListEventArgs e)
        {
            LogUtils.Info("AfterAppendTab(\"{0}\")", e.Info.TabName);
            // create panel
            var PV = new PanelView { Dock = DockStyle.Fill };
            PV.GetPresenter().Objects = e.Info;
            ListView LV = PV.Controls[0] as ListView;
            if (LV != null)
            {
                LV.SmallImageList = LanExchangeIcons.SmallImageList;
                LV.LargeImageList = LanExchangeIcons.LargeImageList;
                LV.View = e.Info.CurrentView;
                if (MainForm.Instance != null)
                    MainForm.Instance.tipComps.SetToolTip(LV, " ");
            }
            PV.FocusedItemChanged += PV_FocusedItemChanged;
            PV.FilterTextChanged += PV_FilterTextChanged;
            // add new tab and insert panel into it
            m_View.NewTabFromItemList(e.Info);
            m_View.AddControl(m_View.TabPagesCount - 1, PV);
            // set update event
            PanelPresenter presenter = PV.GetPresenter();
            e.Info.Changed += presenter.Items_Changed;
            e.Info.SubscriptionChanged += Item_SubscriptionChanged;
            // update items
            e.Info.DataChanged(null, ConcreteSubject.UserItems);
        }

        public void PV_FocusedItemChanged(object sender, EventArgs e)
        {
            if (PanelViewFocusedItemChanged != null)
                PanelViewFocusedItemChanged(sender, e);
        }

        public void PV_FilterTextChanged(object sender, EventArgs e)
        {
            if (PanelViewFilterTextChanged != null)
                PanelViewFilterTextChanged(sender, e);
        }

        public void Item_SubscriptionChanged(object sender, EventArgs e)
        {
            PanelItemList Item = sender as PanelItemList;
            if (Item == null) return;
            int Index = m_Model.GetItemIndex(Item);
            if (Index != -1)
                m_View.SetTabToolTip(Index, Item.ToolTipText);
        }

        public void Model_AfterRemove(object sender, PanelIndexEventArgs e)
        {
            m_View.RemoveTabAt(e.Index);
        }

        public void Model_AfterRename(object sender, PanelItemListEventArgs e)
        {
            m_View.SelectedTabText = e.Info.TabName;
        }

        public void Model_IndexChanged(object sender, PanelIndexEventArgs e)
        {
            LogUtils.Info("PagesModel_IndexChanged({0})", e.Index);
            m_View.SetSelectedIndex(e.Index);
            m_View.FocusPanelView();
        }

        internal void EditTabParams()
        {
            int Index = m_View.PopupSelectedIndex;
            PanelItemList Info = m_Model.GetItem(Index);
            if (Info == null) return;

            using (var Form = new TabParamsForm())
            {
                Form.Text = String.Format(Form.Text, Info.TabName);
                Form.Groups = Info.Groups;
                Form.PrepareForm();
                if (Form.ShowDialog() == DialogResult.OK)
                {
                    Info.Groups = Form.Groups;
                    Info.UpdateSubsctiption();
                    m_Model.SaveSettings();
                }
            }
        }
    }

}
