using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace LanExchange
{
    #region MainForm
    public partial class MainForm : Form
    {
        public static MainForm Instance = null;

        #region Init & Load main form
        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            // initialization of application controller
            TMainApp.App.Init();
        }

        public void ActivateFromNewInstance()
        {
            //F.bReActivate = true;
            //F.IsFormVisible = true;
            this.Activate();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (TMainApp.App == null) return;
            TMainApp.App.DoLoaded();

            //MainForm.MainApp.LogPrint("MainForm_Load");
            /*
            listView1.Items.Clear();
            foreach (var Pair in MainApp.plugins)
            {
                ListViewItem Item = new ListViewItem();
                Item.Text = Pair.Value.Name + " " + Pair.Value.Version;
                Item.SubItems.Add(Pair.Value.Author);
                Item.SubItems.Add(Pair.Value.Description);
                listView1.Items.Add(Item);
            }
             */
        }
        #endregion

        #region Edit tabs, Switching tabs

        private void mNewTab_Click(object sender, EventArgs e)
        {
            TMainApp.App.TabController.NewTab();
        }

        private void mCloseTab_Click(object sender, EventArgs e)
        {
            TMainApp.App.TabController.CloseTab();
        }

        private void mRenameTab_Click(object sender, EventArgs e)
        {
            TMainApp.App.TabController.RenameTab();
        }

        private void mSaveTab_Click(object sender, EventArgs e)
        {
            TMainApp.App.TabController.SaveTab();
        }

        private void mListTab_Click(object sender, EventArgs e)
        {
            TMainApp.App.TabController.ListTab();
        }

        private void popPages_Opened(object sender, EventArgs e)
        {
            mSelectTab.DropDownItems.Clear();
            TMainApp.App.TabController.AddTabsToMenuItem(new TLanEXMenuItem(mSelectTab), TMainApp.App.TabController.mSelectTab_Click, false);
            mCloseTab.Enabled = TMainApp.App.TabController.CanModifyTab(Pages.SelectedIndex);
            mRenameTab.Enabled = mCloseTab.Enabled;
        }

        private void mSendToTab_DropDownOpened(object sender, EventArgs e)
        {

        }

        #endregion

        private void tipComps_Popup(object sender, PopupEventArgs e)
        {
            if (!(e.AssociatedControl is ListView))
                return;
            ListView LV = (ListView)e.AssociatedControl;
            Point P = LV.PointToClient(Control.MousePosition);
            ListViewHitTestInfo Info = LV.HitTest(P);
            if (Info != null && Info.Item != null)
                (sender as ToolTip).ToolTipTitle = Info.Item.Text;
            else
                (sender as ToolTip).ToolTipTitle = "Information";
        }

    }
    #endregion
}
