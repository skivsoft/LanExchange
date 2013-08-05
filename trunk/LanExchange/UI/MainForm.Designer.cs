namespace LanExchange.UI
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
                m_Hotkeys.Dispose();
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
            this.popTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.mExitTray = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status = new System.Windows.Forms.StatusStrip();
            this.lItemsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lCompName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tipComps = new System.Windows.Forms.ToolTip(this.components);
            this.popTop = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Pages = new LanExchange.UI.PagesView();
            this.pInfo = new LanExchange.UI.InfoView();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.mPanelLarge = new System.Windows.Forms.ToolStripMenuItem();
            this.mPanelSmall = new System.Windows.Forms.ToolStripMenuItem();
            this.mPanelList = new System.Windows.Forms.ToolStripMenuItem();
            this.mPanelDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mReRead = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mPanelExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.langEN_US = new System.Windows.Forms.ToolStripMenuItem();
            this.langRU_RU = new System.Windows.Forms.ToolStripMenuItem();
            this.langKK_KZ = new System.Windows.Forms.ToolStripMenuItem();
            this.mHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mWebPage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.timerTabSettingsSaver = new System.Windows.Forms.Timer(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.popTray.SuspendLayout();
            this.Status.SuspendLayout();
            this.popTop.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrayIcon
            // 
            this.TrayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.TrayIcon.BalloonTipTitle = "Оповещение";
            this.TrayIcon.ContextMenuStrip = this.popTray;
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseUp);
            // 
            // popTray
            // 
            this.popTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mOpen,
            this.toolStripSeparator3,
            this.toolStripMenuItem5,
            this.mExitTray});
            this.popTray.Name = "popTray";
            this.popTray.Size = new System.Drawing.Size(178, 76);
            this.popTray.Opening += new System.ComponentModel.CancelEventHandler(this.popTray_Opening);
            // 
            // mOpen
            // 
            this.mOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.mOpen.Name = "mOpen";
            this.mOpen.ShortcutKeyDisplayString = "Ctrl+Win+X";
            this.mOpen.Size = new System.Drawing.Size(177, 22);
            this.mOpen.Text = "Open";
            this.mOpen.Click += new System.EventHandler(this.mOpen_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(174, 6);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(177, 22);
            this.toolStripMenuItem5.Text = "About";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.mAbout_Click);
            // 
            // mExitTray
            // 
            this.mExitTray.Name = "mExitTray";
            this.mExitTray.Size = new System.Drawing.Size(177, 22);
            this.mExitTray.Text = "Exit";
            this.mExitTray.Click += new System.EventHandler(this.mExit_Click);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // Status
            // 
            this.Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lItemsCount,
            this.toolStripStatusLabel1,
            this.lCompName,
            this.toolStripStatusLabel4,
            this.lUserName});
            this.Status.Location = new System.Drawing.Point(0, 520);
            this.Status.Name = "Status";
            this.Status.ShowItemToolTips = true;
            this.Status.Size = new System.Drawing.Size(564, 22);
            this.Status.TabIndex = 15;
            this.Status.Text = "statusStrip1";
            this.Status.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Status_MouseDoubleClick);
            // 
            // lItemsCount
            // 
            this.lItemsCount.Name = "lItemsCount";
            this.lItemsCount.Size = new System.Drawing.Size(503, 17);
            this.lItemsCount.Spring = true;
            this.lItemsCount.Text = "    ";
            this.lItemsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lItemsCount.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lItemsCount_MouseUp);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(4, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // lCompName
            // 
            this.lCompName.Name = "lCompName";
            this.lCompName.Size = new System.Drawing.Size(19, 17);
            this.lCompName.Text = "    ";
            this.lCompName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lCompName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lCompName_MouseUp);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel4.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabel4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(4, 17);
            this.toolStripStatusLabel4.Text = "toolStripStatusLabel4";
            // 
            // lUserName
            // 
            this.lUserName.Name = "lUserName";
            this.lUserName.Size = new System.Drawing.Size(19, 17);
            this.lUserName.Text = "    ";
            // 
            // tipComps
            // 
            this.tipComps.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.tipComps.ToolTipTitle = "DefaultTitle";
            this.tipComps.Popup += new System.Windows.Forms.PopupEventHandler(this.tipComps_Popup);
            // 
            // popTop
            // 
            this.popTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemToolStripMenuItem});
            this.popTop.Name = "popTop";
            this.popTop.Size = new System.Drawing.Size(130, 26);
            this.popTop.Opening += new System.ComponentModel.CancelEventHandler(this.popTop_Opening);
            this.popTop.Opened += new System.EventHandler(this.popTop_Opened);
            // 
            // menuItemToolStripMenuItem
            // 
            this.menuItemToolStripMenuItem.Name = "menuItemToolStripMenuItem";
            this.menuItemToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.menuItemToolStripMenuItem.Text = "MenuItem";
            // 
            // Pages
            // 
            this.Pages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pages.Location = new System.Drawing.Point(0, 88);
            this.Pages.Margin = new System.Windows.Forms.Padding(0);
            this.Pages.Name = "Pages";
            this.Pages.SelectedIndex = -1;
            this.Pages.SelectedTabText = "";
            this.Pages.Size = new System.Drawing.Size(564, 432);
            this.Pages.TabIndex = 24;
            // 
            // pInfo
            // 
            this.pInfo.BackColor = System.Drawing.SystemColors.Window;
            this.pInfo.ContextMenuStrip = this.popTop;
            this.pInfo.CountLines = 0;
            this.pInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pInfo.Location = new System.Drawing.Point(0, 24);
            this.pInfo.MinimumSize = new System.Drawing.Size(0, 64);
            this.pInfo.Name = "pInfo";
            this.pInfo.Size = new System.Drawing.Size(564, 64);
            this.pInfo.TabIndex = 23;
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mPanel,
            this.mLanguage,
            this.mHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(564, 24);
            this.MainMenu.TabIndex = 25;
            this.MainMenu.Text = "menuStrip1";
            // 
            // mPanel
            // 
            this.mPanel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mPanelLarge,
            this.mPanelSmall,
            this.mPanelList,
            this.mPanelDetails,
            this.toolStripSeparator1,
            this.mReRead,
            this.toolStripSeparator8,
            this.mPanelExit});
            this.mPanel.Name = "mPanel";
            this.mPanel.Size = new System.Drawing.Size(48, 20);
            this.mPanel.Text = "&Panel";
            this.mPanel.DropDownOpening += new System.EventHandler(this.mPanel_DropDownOpening);
            // 
            // mPanelLarge
            // 
            this.mPanelLarge.Name = "mPanelLarge";
            this.mPanelLarge.Size = new System.Drawing.Size(156, 22);
            this.mPanelLarge.Tag = "0";
            this.mPanelLarge.Text = "Large icons";
            this.mPanelLarge.Click += new System.EventHandler(this.mPanelLarge_Click);
            // 
            // mPanelSmall
            // 
            this.mPanelSmall.Name = "mPanelSmall";
            this.mPanelSmall.Size = new System.Drawing.Size(156, 22);
            this.mPanelSmall.Tag = "2";
            this.mPanelSmall.Text = "Small icons";
            this.mPanelSmall.Click += new System.EventHandler(this.mPanelLarge_Click);
            // 
            // mPanelList
            // 
            this.mPanelList.Name = "mPanelList";
            this.mPanelList.Size = new System.Drawing.Size(156, 22);
            this.mPanelList.Tag = "3";
            this.mPanelList.Text = "List";
            this.mPanelList.Click += new System.EventHandler(this.mPanelLarge_Click);
            // 
            // mPanelDetails
            // 
            this.mPanelDetails.Checked = true;
            this.mPanelDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mPanelDetails.Name = "mPanelDetails";
            this.mPanelDetails.Size = new System.Drawing.Size(156, 22);
            this.mPanelDetails.Tag = "1";
            this.mPanelDetails.Text = "Details";
            this.mPanelDetails.Click += new System.EventHandler(this.mPanelLarge_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // mReRead
            // 
            this.mReRead.Name = "mReRead";
            this.mReRead.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.mReRead.Size = new System.Drawing.Size(156, 22);
            this.mReRead.Text = "Re-read";
            this.mReRead.Click += new System.EventHandler(this.rereadToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(153, 6);
            // 
            // mPanelExit
            // 
            this.mPanelExit.Name = "mPanelExit";
            this.mPanelExit.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.mPanelExit.Size = new System.Drawing.Size(156, 22);
            this.mPanelExit.Text = "Exit";
            this.mPanelExit.Click += new System.EventHandler(this.mExit_Click);
            // 
            // mLanguage
            // 
            this.mLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.langEN_US,
            this.langRU_RU,
            this.langKK_KZ});
            this.mLanguage.Name = "mLanguage";
            this.mLanguage.Size = new System.Drawing.Size(71, 20);
            this.mLanguage.Text = "&Language";
            this.mLanguage.Visible = false;
            // 
            // langEN_US
            // 
            this.langEN_US.Checked = true;
            this.langEN_US.CheckState = System.Windows.Forms.CheckState.Checked;
            this.langEN_US.Name = "langEN_US";
            this.langEN_US.Size = new System.Drawing.Size(180, 22);
            this.langEN_US.Text = "English";
            // 
            // langRU_RU
            // 
            this.langRU_RU.Name = "langRU_RU";
            this.langRU_RU.Size = new System.Drawing.Size(180, 22);
            this.langRU_RU.Text = "Russian — Русский";
            // 
            // langKK_KZ
            // 
            this.langKK_KZ.Name = "langKK_KZ";
            this.langKK_KZ.Size = new System.Drawing.Size(180, 22);
            this.langKK_KZ.Text = "Kazakh — Қазақ тілі";
            // 
            // mHelp
            // 
            this.mHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator2,
            this.mWebPage,
            this.toolStripSeparator6,
            this.mHelpAbout});
            this.mHelp.Name = "mHelp";
            this.mHelp.Size = new System.Drawing.Size(44, 20);
            this.mHelp.Text = "&Help";
            // 
            // mWebPage
            // 
            this.mWebPage.Name = "mWebPage";
            this.mWebPage.Size = new System.Drawing.Size(196, 22);
            this.mWebPage.Text = "LanExchange Webpage";
            this.mWebPage.Click += new System.EventHandler(this.mWebPage_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(193, 6);
            // 
            // mHelpAbout
            // 
            this.mHelpAbout.Name = "mHelpAbout";
            this.mHelpAbout.Size = new System.Drawing.Size(196, 22);
            this.mHelpAbout.Text = "About";
            this.mHelpAbout.Click += new System.EventHandler(this.mAbout_Click);
            // 
            // timerTabSettingsSaver
            // 
            this.timerTabSettingsSaver.Interval = 5000;
            this.timerTabSettingsSaver.Tick += new System.EventHandler(this.timerTabSettingsSaver_Tick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(193, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.toolStripMenuItem1.Size = new System.Drawing.Size(196, 22);
            this.toolStripMenuItem1.Text = "Shortcut keys";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 542);
            this.Controls.Add(this.Pages);
            this.Controls.Add(this.pInfo);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.Status);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.popTray.ResumeLayout(false);
            this.Status.ResumeLayout(false);
            this.Status.PerformLayout();
            this.popTop.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ContextMenuStrip popTray;
        private System.Windows.Forms.ToolStripMenuItem mExitTray;
        private System.Windows.Forms.ToolStripMenuItem mOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.StatusStrip Status;
        public System.Windows.Forms.ToolStripStatusLabel lItemsCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lCompName;
        public System.Windows.Forms.ToolTip tipComps;
        private System.Windows.Forms.ContextMenuStrip popTop;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel lUserName;
        internal InfoView pInfo;
        internal PagesView Pages;
        private System.Windows.Forms.ToolStripMenuItem menuItemToolStripMenuItem;
        private System.Windows.Forms.Timer timerTabSettingsSaver;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mHelp;
        private System.Windows.Forms.ToolStripMenuItem mHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem mPanel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mReRead;
        private System.Windows.Forms.ToolStripMenuItem mPanelLarge;
        private System.Windows.Forms.ToolStripMenuItem mPanelSmall;
        private System.Windows.Forms.ToolStripMenuItem mPanelList;
        private System.Windows.Forms.ToolStripMenuItem mPanelDetails;
        private System.Windows.Forms.ToolStripMenuItem mWebPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem mPanelExit;
        private System.Windows.Forms.ToolStripMenuItem mLanguage;
        private System.Windows.Forms.ToolStripMenuItem langEN_US;
        private System.Windows.Forms.ToolStripMenuItem langRU_RU;
        private System.Windows.Forms.ToolStripMenuItem langKK_KZ;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

