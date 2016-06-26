using System.Windows.Forms;
using LanExchange.Presentation.WinForms.Properties;

namespace LanExchange.Presentation.WinForms.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tipComps = new System.Windows.Forms.ToolTip(this.components);
            this.MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.mPanel = new System.Windows.Forms.MenuItem();
            this.mReRead = new System.Windows.Forms.MenuItem();
            this.mPanelSep2 = new System.Windows.Forms.MenuItem();
            this.mExit = new System.Windows.Forms.MenuItem();
            this.mView = new System.Windows.Forms.MenuItem();
            this.mViewLarge = new System.Windows.Forms.MenuItem();
            this.mViewSmall = new System.Windows.Forms.MenuItem();
            this.mViewList = new System.Windows.Forms.MenuItem();
            this.mViewDetails = new System.Windows.Forms.MenuItem();
            this.mLanguage = new System.Windows.Forms.MenuItem();
            this.mHelp = new System.Windows.Forms.MenuItem();
            this.mHelpWeb = new System.Windows.Forms.MenuItem();
            this.mHelpBugs = new System.Windows.Forms.MenuItem();
            this.mHelpLangs = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mHelpAbout = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // TrayIcon
            // 
            this.TrayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseUp);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(118, 17);
            // 
            // tipComps
            // 
            this.tipComps.IsBalloon = true;
            this.tipComps.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.tipComps.Popup += new System.Windows.Forms.PopupEventHandler(this.tipComps_Popup);
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mPanel,
            this.mView,
            this.mLanguage,
            this.mHelp});
            // 
            // mPanel
            // 
            this.mPanel.Index = 0;
            this.mPanel.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mReRead,
            this.mPanelSep2,
            this.mExit});
            this.mPanel.Text = global::LanExchange.Presentation.WinForms.Properties.Resources.mPanel_Text;
            this.mPanel.Popup += new System.EventHandler(this.mPanel_Popup);
            // 
            // mReRead
            // 
            this.mReRead.Index = 0;
            this.mReRead.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
            this.mReRead.Text = "&Re-read";
            this.mReRead.Click += new System.EventHandler(this.mReRead_Click);
            // 
            // mPanelSep2
            // 
            this.mPanelSep2.Index = 1;
            this.mPanelSep2.Text = "-";
            // 
            // mExit
            // 
            this.mExit.Index = 2;
            this.mExit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
            this.mExit.Text = "E&xit";
            // 
            // mView
            // 
            this.mView.Index = 1;
            this.mView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mViewLarge,
            this.mViewSmall,
            this.mViewList,
            this.mViewDetails});
            this.mView.Text = global::LanExchange.Presentation.WinForms.Properties.Resources.mView_Text;
            this.mView.Popup += new System.EventHandler(this.mView_Popup);
            // 
            // mViewLarge
            // 
            this.mViewLarge.Index = 0;
            this.mViewLarge.RadioCheck = true;
            this.mViewLarge.Tag = "";
            this.mViewLarge.Text = "La&rge icons";
            this.mViewLarge.Click += new System.EventHandler(this.mView_Click);
            // 
            // mViewSmall
            // 
            this.mViewSmall.Index = 1;
            this.mViewSmall.RadioCheck = true;
            this.mViewSmall.Tag = "";
            this.mViewSmall.Text = "Small ico&ns";
            this.mViewSmall.Click += new System.EventHandler(this.mView_Click);
            // 
            // mViewList
            // 
            this.mViewList.Index = 2;
            this.mViewList.RadioCheck = true;
            this.mViewList.Tag = "";
            this.mViewList.Text = "&List";
            this.mViewList.Click += new System.EventHandler(this.mView_Click);
            // 
            // mViewDetails
            // 
            this.mViewDetails.Checked = true;
            this.mViewDetails.Index = 3;
            this.mViewDetails.RadioCheck = true;
            this.mViewDetails.Tag = "";
            this.mViewDetails.Text = "&Details";
            this.mViewDetails.Click += new System.EventHandler(this.mView_Click);
            // 
            // mLanguage
            // 
            this.mLanguage.Index = 2;
            this.mLanguage.Text = global::LanExchange.Presentation.WinForms.Properties.Resources.mLanguage_Text;
            this.mLanguage.Popup += new System.EventHandler(this.mLanguage_Popup);
            // 
            // mHelp
            // 
            this.mHelp.Index = 3;
            this.mHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mHelpWeb,
            this.mHelpBugs,
            this.mHelpLangs,
            this.menuItem1,
            this.mHelpAbout});
            this.mHelp.Text = global::LanExchange.Presentation.WinForms.Properties.Resources.mHelp_Text;
            // 
            // mHelpWeb
            // 
            this.mHelpWeb.Index = 0;
            this.mHelpWeb.Text = "Lan&Exchange Webpage";
            this.mHelpWeb.Click += new System.EventHandler(this.mWebPage_Click);
            // 
            // mHelpBugs
            // 
            this.mHelpBugs.Index = 1;
            this.mHelpBugs.Text = "&BugTracker Webpage";
            this.mHelpBugs.Click += new System.EventHandler(this.mHelpBugs_Click);
            // 
            // mHelpLangs
            // 
            this.mHelpLangs.Index = 2;
            this.mHelpLangs.Text = "&Localization Webpage";
            this.mHelpLangs.Click += new System.EventHandler(this.mHelpLangs_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 3;
            this.menuItem1.Text = "-";
            // 
            // mHelpAbout
            // 
            this.mHelpAbout.Index = 4;
            this.mHelpAbout.Text = "&About";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 542);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        public System.Windows.Forms.ToolTip tipComps;
        private System.Windows.Forms.MainMenu MainMenu;
        private System.Windows.Forms.MenuItem mHelp;
        private System.Windows.Forms.MenuItem mHelpAbout;
        private System.Windows.Forms.MenuItem mPanel;
        private System.Windows.Forms.MenuItem mReRead;
        private System.Windows.Forms.MenuItem mHelpWeb;
        private System.Windows.Forms.MenuItem mExit;
        private System.Windows.Forms.MenuItem mLanguage;
        private System.Windows.Forms.MenuItem mHelpLangs;
        private System.Windows.Forms.MenuItem mHelpBugs;
        private System.Windows.Forms.MenuItem mView;
        private System.Windows.Forms.MenuItem mViewLarge;
        private System.Windows.Forms.MenuItem mViewSmall;
        private System.Windows.Forms.MenuItem mViewList;
        private System.Windows.Forms.MenuItem mViewDetails;
        private System.Windows.Forms.MenuItem mPanelSep2;
        private MenuItem menuItem1;
    }
}

