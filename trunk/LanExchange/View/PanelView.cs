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
            LV.Items.Clear();
            foreach (PanelItemVO item in items)
            {
                ListViewItem LVI = LV.Items.Add(item.Name);
                LVI.SubItems.Add(item.Comment);
            }
        }


        public event EventHandler LevelDown;
        public event EventHandler LevelUp;

        protected virtual void OnLevelDown(EventArgs e)
        {
            if (LevelDown != null) LevelDown(this, e);
        }

        protected virtual void SetLevelDown()
        {
            OnLevelDown(new EventArgs());
        }

        protected virtual void OnLevelUp(EventArgs e)
        {
            if (LevelUp != null) LevelUp(this, e);
        }

        protected virtual void SetLevelUp()
        {
            OnLevelUp(new EventArgs());
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

    }
}
