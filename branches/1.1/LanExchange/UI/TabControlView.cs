using System;
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



        public string SelectedTabText
        {
            get
            {
                if (TabPages.Count > 0 && SelectedTab != null)
                    return SelectedTab.Text;
                else
                    return String.Empty;
            }
            set
            {
                if (TabPages.Count > 0 && SelectedTab != null)
                    SelectedTab.Text = value;
            }
        }


        public void AddControl(int Index, Control control)
        {
            if (Index < 0 || Index > TabPages.Count - 1)
                throw new ArgumentOutOfRangeException("Index");
            TabPages[Index].Controls.Add(control);
        }


        public void RemoveTabAt(int Index)
        {
            TabPages.RemoveAt(Index);
        }
    }
}
