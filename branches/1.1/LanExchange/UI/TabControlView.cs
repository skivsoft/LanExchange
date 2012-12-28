using System;
using System.Windows.Forms;
using LanExchange.View;
using LanExchange.Model;

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

        /// <summary>
        /// Simple solution for long strings. 
        /// TODO: Changes needed here. Max length must be in pixels instead number of chars.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private string Ellipsis(string text, int length)
        {
            if (text.Length > length)
                return text.Substring(0, length) + "…";
            else
                return text;
        }

        public void NewTab(string tabname)
        {
            TabPage Tab = new TabPage();
            Tab.Text = Ellipsis(tabname, 20);
            Tab.ImageIndex = LanExchangeIcons.imgWorkgroup;
            Tab.ToolTipText = tabname;
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
