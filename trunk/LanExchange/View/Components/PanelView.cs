using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LanExchange.Model.VO;

namespace LanExchange.View.Components
{
    public partial class PanelView : UserControl
    {
        private IList<PanelItemVO> m_CurrentItems;

        public PanelView()
        {
            InitializeComponent();
            LV.Columns.Clear();
            LV.Columns.Add("Сетевое имя", 130);
            LV.Columns.Add("Описание", 250);
        }

        public void AddItems(IList<PanelItemVO> items)
        {
            m_CurrentItems = items;
            int OldCount = LV.VirtualListSize;
            LV.VirtualListSize = items.Count;
            if (OldCount != items.Count)
            {
                SetItemsCountChanged();
            }
        }

        public event EventHandler LevelDown;
        public event EventHandler LevelUp;
        public event EventHandler ItemsCountChanged;

        protected virtual void OnLevelDown(EventArgs e)
        {
            if (LevelDown != null) LevelDown(this, e);
        }

        protected virtual void OnLevelUp(EventArgs e)
        {
            if (LevelUp != null) LevelUp(this, e);
        }

        protected virtual void OnItemsCountChanged(EventArgs e)
        {
            if (ItemsCountChanged != null) ItemsCountChanged(this, e);
        }

        protected virtual void SetLevelDown()
        {
            OnLevelDown(new EventArgs());
        }

        protected virtual void SetLevelUp()
        {
            OnLevelUp(new EventArgs());
        }

        protected virtual void SetItemsCountChanged()
        {
            OnItemsCountChanged(new EventArgs());
        }

        public PanelItemVO SelectedPanelItem
        {
            get 
            {
                if (LV.FocusedItem == null) return null;
                return (PanelItemVO)LV.FocusedItem.Tag;
            }
        }

        private void LV_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Enter:
                    SetLevelDown();
                    e.Handled = true;
                    break;
                case Keys.Back:
                    SetLevelUp();
                    e.Handled = true;
                    break;
            }
        }

        private void LV_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SetLevelDown();
        }

        private void LV_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex < 0 || e.ItemIndex > m_CurrentItems.Count - 1)
                return;
            PanelItemVO Item = m_CurrentItems[e.ItemIndex];
            e.Item = new ListViewItem(Item.Name);
            e.Item.SubItems.Add(Item.Comment);
            e.Item.Tag = Item;
        }
    }
}
