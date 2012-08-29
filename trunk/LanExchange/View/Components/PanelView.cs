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
        //private IList<PanelItemVO> m_CurrentItems;

        public PanelView()
        {
            InitializeComponent();
            LV.VirtualListDataSource = new FastPanelItemDataSource(LV);
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

        public event EventHandler LevelDown;
        public event EventHandler LevelUp;

        protected virtual void OnLevelDown(EventArgs e)
        {
            if (LevelDown != null) LevelDown(this, e);
        }

        protected virtual void OnLevelUp(EventArgs e)
        {
            if (LevelUp != null) LevelUp(this, e);
        }

        public virtual void SetLevelDown()
        {
            OnLevelDown(new EventArgs());
        }

        public virtual void SetLevelUp()
        {
            OnLevelUp(new EventArgs());
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
