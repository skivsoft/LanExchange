using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using LanExchange;
using LanExchange.Model.VO;
using LanExchange.View;
using LanExchange.View.Forms;
using System.Collections;
using BrightIdeasSoftware;

namespace LanExchange.View.Components
{
    public partial class PanelView : UserControl
    {
        private IList<PanelItemVO> m_CurrentItems;
        private PanelItemVO[] m_SavedSelectedItems;
        private PanelItemVO m_SavedFocused;

        public PanelView()
        {
            InitializeComponent();
        }

        public void SetColumns(OLVColumn[] columns)
        {
            // all columns
            LV.AllColumns.Clear();
            LV.AllColumns.AddRange(columns);
            // visible columns only
            LV.Columns.Clear();
            foreach(OLVColumn column in columns)
                if (column.IsVisible)
                    LV.Columns.Add(column);
        }

        public void AddItems(IList<PanelItemVO> items)
        {
            m_CurrentItems = items;
            if (LV.VirtualListSize != items.Count)
            {
                LV.VirtualListSize = items.Count;
                SetItemsCountChanged();
            }
            //LV.Refresh();
        }

        public void SetObjects(IEnumerable collection)
        {
            LV.SetObjects(collection);
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

        public virtual void SetLevelDown()
        {
            OnLevelDown(new EventArgs());
        }

        public virtual void SetLevelUp()
        {
            OnLevelUp(new EventArgs());
        }

        protected virtual void SetItemsCountChanged()
        {
            OnItemsCountChanged(new EventArgs());
        }

        public PanelItemVO FirstPanelItem
        {
            get
            {
                if (LV.VirtualListSize == 0)
                    return null;
                else
                    return (PanelItemVO)LV.Items[0].Tag;
            }
        }

        public PanelItemVO SelectedPanelItem
        {
            get 
            {
                return (PanelItemVO)LV.SelectedObject;
            }
        }

        public void SaveSelected()
        {
            m_SavedSelectedItems = new PanelItemVO[LV.SelectedIndices.Count];
            for (int i = 0; i < m_SavedSelectedItems.Length; i++)
                m_SavedSelectedItems[i] = m_CurrentItems[LV.SelectedIndices[i]];
            m_SavedFocused = m_CurrentItems[LV.FocusedItem.Index];
        }

        public void RestoreSelected()
        {
            int Index;
            LV.SelectedIndices.Clear();
            for (int i = 0; i < m_SavedSelectedItems.Length; i++)
            {
                Index = m_CurrentItems.IndexOf(m_SavedSelectedItems[i]);
                if (Index != -1)
                    LV.SelectedIndices.Add(Index);
            }
            Index = m_CurrentItems.IndexOf(m_SavedFocused);
            if (Index != -1)
            {
                LV.FocusedItem = LV.Items[Index];
                LV.EnsureVisible(Index);
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFormMediator M = (IFormMediator)AppFacade.Instance.RetrieveMediator(AboutFormMediator.NAME);
            if (M != null)
            {
                M.ShowDialog();
            }
        }

        private void LV_DoubleClick(object sender, EventArgs e)
        {
            SetLevelDown();
        }
    }
}
