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
            this.mSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status = new System.Windows.Forms.StatusStrip();
            this.lItemsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lCompName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.popPages = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mNewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.mCloseTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mRenameTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mSelectTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.mTabParams = new System.Windows.Forms.ToolStripMenuItem();
            this.tipComps = new System.Windows.Forms.ToolTip(this.components);
            this.popTop = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Pages = new LanExchange.UI.TabControlView();
            this.pInfo = new LanExchange.UI.InfoView();
            this.popTray.SuspendLayout();
            this.Status.SuspendLayout();
            this.popPages.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrayIcon
            // 
            this.TrayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.TrayIcon.BalloonTipTitle = "Оповещение";
            this.TrayIcon.ContextMenuStrip = this.popTray;
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseClick);
            // 
            // popTray
            // 
            this.popTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mOpen,
            this.toolStripSeparator3,
            this.mSettings,
            this.toolStripSeparator4,
            this.mAbout,
            this.mExit});
            this.popTray.Name = "popTray";
            this.popTray.Size = new System.Drawing.Size(159, 104);
            this.popTray.Opening += new System.ComponentModel.CancelEventHandler(this.popTray_Opening);
            // 
            // mOpen
            // 
            this.mOpen.Name = "mOpen";
            this.mOpen.Size = new System.Drawing.Size(158, 22);
            this.mOpen.Text = "Открыть";
            this.mOpen.Click += new System.EventHandler(this.mOpen_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(155, 6);
            // 
            // mSettings
            // 
            this.mSettings.Name = "mSettings";
            this.mSettings.Size = new System.Drawing.Size(158, 22);
            this.mSettings.Text = "Настройки...";
            this.mSettings.Click += new System.EventHandler(this.mSettings_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(155, 6);
            // 
            // mAbout
            // 
            this.mAbout.Name = "mAbout";
            this.mAbout.Size = new System.Drawing.Size(158, 22);
            this.mAbout.Text = "О программе...";
            this.mAbout.Click += new System.EventHandler(this.mAbout_Click);
            // 
            // mExit
            // 
            this.mExit.Name = "mExit";
            this.mExit.Size = new System.Drawing.Size(158, 22);
            this.mExit.Text = "Выход";
            this.mExit.Click += new System.EventHandler(this.mExit_Click);
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
            this.Status.Size = new System.Drawing.Size(564, 22);
            this.Status.TabIndex = 15;
            this.Status.Text = "statusStrip1";
            // 
            // lItemsCount
            // 
            this.lItemsCount.Name = "lItemsCount";
            this.lItemsCount.Size = new System.Drawing.Size(472, 17);
            this.lItemsCount.Spring = true;
            this.lItemsCount.Text = "    ";
            this.lItemsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lItemsCount.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lItemsCount_MouseDown);
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
            this.lCompName.Click += new System.EventHandler(this.lCompName_Click);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel4.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabel4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(4, 17);
            this.toolStripStatusLabel4.Text = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Visible = false;
            // 
            // lUserName
            // 
            this.lUserName.Name = "lUserName";
            this.lUserName.Size = new System.Drawing.Size(19, 17);
            this.lUserName.Text = "    ";
            this.lUserName.Visible = false;
            // 
            // popPages
            // 
            this.popPages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mNewTab,
            this.toolStripSeparator9,
            this.mCloseTab,
            this.mRenameTab,
            this.mSelectTab,
            this.toolStripSeparator11,
            this.mTabParams});
            this.popPages.Name = "popPages";
            this.popPages.Size = new System.Drawing.Size(245, 126);
            this.popPages.Opened += new System.EventHandler(this.popPages_Opened);
            // 
            // mNewTab
            // 
            this.mNewTab.Name = "mNewTab";
            this.mNewTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.mNewTab.Size = new System.Drawing.Size(244, 22);
            this.mNewTab.Text = "Новая вкладка";
            this.mNewTab.Click += new System.EventHandler(this.mNewTab_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(241, 6);
            // 
            // mCloseTab
            // 
            this.mCloseTab.Name = "mCloseTab";
            this.mCloseTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.mCloseTab.Size = new System.Drawing.Size(244, 22);
            this.mCloseTab.Text = "Закрыть вкладку";
            this.mCloseTab.Click += new System.EventHandler(this.mCloseTab_Click);
            // 
            // mRenameTab
            // 
            this.mRenameTab.Name = "mRenameTab";
            this.mRenameTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.mRenameTab.Size = new System.Drawing.Size(244, 22);
            this.mRenameTab.Text = "Переименовать вкладку";
            this.mRenameTab.Click += new System.EventHandler(this.mRenameTab_Click);
            // 
            // mSelectTab
            // 
            this.mSelectTab.Name = "mSelectTab";
            this.mSelectTab.Size = new System.Drawing.Size(244, 22);
            this.mSelectTab.Text = "Выбрать вкладку";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(241, 6);
            // 
            // mTabParams
            // 
            this.mTabParams.Name = "mTabParams";
            this.mTabParams.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mTabParams.Size = new System.Drawing.Size(244, 22);
            this.mTabParams.Text = "Настройка вкладки...";
            this.mTabParams.Click += new System.EventHandler(this.mTabParams_Click);
            // 
            // tipComps
            // 
            this.tipComps.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.tipComps.ToolTipTitle = "Test";
            this.tipComps.Popup += new System.Windows.Forms.PopupEventHandler(this.tipComps_Popup);
            // 
            // popTop
            // 
            this.popTop.Name = "popTop";
            this.popTop.Size = new System.Drawing.Size(61, 4);
            this.popTop.Opening += new System.ComponentModel.CancelEventHandler(this.popTop_Opening);
            this.popTop.Opened += new System.EventHandler(this.popTop_Opened);
            // 
            // Pages
            // 
            this.Pages.ContextMenuStrip = this.popPages;
            this.Pages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pages.Location = new System.Drawing.Point(0, 60);
            this.Pages.Multiline = true;
            this.Pages.Name = "Pages";
            this.Pages.SelectedIndex = 0;
            this.Pages.SelectedTabText = "";
            this.Pages.ShowToolTips = true;
            this.Pages.Size = new System.Drawing.Size(564, 460);
            this.Pages.TabIndex = 24;
            this.Pages.Selected += new System.Windows.Forms.TabControlEventHandler(this.Pages_Selected);
            // 
            // pInfo
            // 
            this.pInfo.BackColor = System.Drawing.Color.White;
            this.pInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pInfo.InfoComp = "    ";
            this.pInfo.InfoDesc = "    ";
            this.pInfo.InfoOS = "    ";
            this.pInfo.Location = new System.Drawing.Point(0, 0);
            this.pInfo.Name = "pInfo";
            this.pInfo.Size = new System.Drawing.Size(564, 60);
            this.pInfo.TabIndex = 23;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 542);
            this.Controls.Add(this.Pages);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.pInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.popTray.ResumeLayout(false);
            this.Status.ResumeLayout(false);
            this.Status.PerformLayout();
            this.popPages.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ContextMenuStrip popTray;
        private System.Windows.Forms.ToolStripMenuItem mExit;
        private System.Windows.Forms.ToolStripMenuItem mOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mSettings;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.StatusStrip Status;
        public System.Windows.Forms.ToolStripStatusLabel lItemsCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lCompName;
        public System.Windows.Forms.ToolTip tipComps;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mAbout;
        private System.Windows.Forms.ContextMenuStrip popPages;
        private System.Windows.Forms.ToolStripMenuItem mNewTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem mCloseTab;
        private System.Windows.Forms.ToolStripMenuItem mRenameTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem mSelectTab;
        private System.Windows.Forms.ContextMenuStrip popTop;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel lUserName;
        private System.Windows.Forms.ToolStripMenuItem mTabParams;
        private InfoView pInfo;
        private TabControlView Pages;
    }
}

