using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LanExchange.View;

namespace LanExchange.UI
{
    public partial class TabControlView : TabControl, ITabControlView
    {
        public TabControlView()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public int TabPagesCount
        {
            get { return TabPages.Count; }
        }

        public void NewTab(string tabname)
        {
            TabPage Tab = new TabPage(tabname);
            Controls.Add(Tab);
        }

    }
}
