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
            this.tipComps = new System.Windows.Forms.ToolTip(this.components);
            this.popTop = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Pages = new LanExchange.UI.PagesView();
            this.pInfo = new LanExchange.UI.InfoView();
            this.popWorkers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mStopWorkers = new System.Windows.Forms.ToolStripMenuItem();
            this.timerTabSettingsSaver = new System.Windows.Forms.Timer(this.components);
            this.popTray.SuspendLayout();
            this.Status.SuspendLayout();
            this.popTop.SuspendLayout();
            this.popWorkers.SuspendLayout();
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
            this.mSettings,
            this.toolStripSeparator4,
            this.mAbout,
            this.mExit});
            this.popTray.Name = "popTray";
            this.popTray.Size = new System.Drawing.Size(213, 126);
            this.popTray.Opening += new System.ComponentModel.CancelEventHandler(this.popTray_Opening);
            // 
            // mOpen
            // 
            this.mOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.mOpen.Name = "mOpen";
            this.mOpen.ShortcutKeyDisplayString = "Ctrl+Win+X";
            this.mOpen.Size = new System.Drawing.Size(212, 22);
            this.mOpen.Text = "Открыть";
            this.mOpen.Click += new System.EventHandler(this.mOpen_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(209, 6);
            // 
            // mSettings
            // 
            this.mSettings.Name = "mSettings";
            this.mSettings.Size = new System.Drawing.Size(212, 22);
            this.mSettings.Text = "Настройки программы...";
            this.mSettings.Click += new System.EventHandler(this.mSettings_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(209, 6);
            // 
            // mAbout
            // 
            this.mAbout.Name = "mAbout";
            this.mAbout.Size = new System.Drawing.Size(212, 22);
            this.mAbout.Text = "О программе...";
            this.mAbout.Click += new System.EventHandler(this.mAbout_Click);
            // 
            // mExit
            // 
            this.mExit.Name = "mExit";
            this.mExit.Size = new System.Drawing.Size(212, 22);
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
            this.Status.ShowItemToolTips = true;
            this.Status.Size = new System.Drawing.Size(564, 22);
            this.Status.TabIndex = 15;
            this.Status.Text = "statusStrip1";
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
            this.Pages.Location = new System.Drawing.Point(0, 60);
            this.Pages.Margin = new System.Windows.Forms.Padding(0);
            this.Pages.Name = "Pages";
            this.Pages.SelectedIndex = -1;
            this.Pages.SelectedTabText = "";
            this.Pages.Size = new System.Drawing.Size(564, 460);
            this.Pages.TabIndex = 24;
            // 
            // pInfo
            // 
            this.pInfo.BackColor = System.Drawing.Color.White;
            this.pInfo.ContextMenuStrip = this.popTop;
            this.pInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pInfo.InfoComp = "    ";
            this.pInfo.InfoDesc = "    ";
            this.pInfo.InfoOS = "    ";
            this.pInfo.Location = new System.Drawing.Point(0, 0);
            this.pInfo.MinimumSize = new System.Drawing.Size(0, 60);
            this.pInfo.Name = "pInfo";
            this.pInfo.Size = new System.Drawing.Size(564, 60);
            this.pInfo.TabIndex = 23;
            // 
            // popWorkers
            // 
            this.popWorkers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mStopWorkers});
            this.popWorkers.Name = "contextMenuStrip1";
            this.popWorkers.Size = new System.Drawing.Size(235, 26);
            // 
            // mStopWorkers
            // 
            this.mStopWorkers.Name = "mStopWorkers";
            this.mStopWorkers.Size = new System.Drawing.Size(234, 22);
            this.mStopWorkers.Text = "Остановить фоновые потоки";
            this.mStopWorkers.Click += new System.EventHandler(this.mStopWorkers_Click);
            // 
            // timerTabSettingsSaver
            // 
            this.timerTabSettingsSaver.Interval = 5000;
            this.timerTabSettingsSaver.Tick += new System.EventHandler(this.timerTabSettingsSaver_Tick);
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
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.popTray.ResumeLayout(false);
            this.Status.ResumeLayout(false);
            this.Status.PerformLayout();
            this.popTop.ResumeLayout(false);
            this.popWorkers.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenuStrip popTop;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel lUserName;
        internal InfoView pInfo;
        internal PagesView Pages;
        private System.Windows.Forms.ContextMenuStrip popWorkers;
        private System.Windows.Forms.ToolStripMenuItem mStopWorkers;
        private System.Windows.Forms.ToolStripMenuItem menuItemToolStripMenuItem;
        private System.Windows.Forms.Timer timerTabSettingsSaver;
    }
}

