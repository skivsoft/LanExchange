namespace LanExchange
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lItemsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lCompName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsBottom = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.eFilter = new System.Windows.Forms.TextBox();
            this.popTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mExit = new System.Windows.Forms.ToolStripMenuItem();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.Pages = new System.Windows.Forms.TabControl();
            this.popPages = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mNewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.mCloseTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mRenameTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mSaveTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.mSelectTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.mListTab = new System.Windows.Forms.ToolStripMenuItem();
            this.tipComps = new System.Windows.Forms.ToolTip(this.components);
            this.popComps = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mLargeIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.mSmallIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.mList = new System.Windows.Forms.ToolStripMenuItem();
            this.mDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mCopyCompName = new System.Windows.Forms.ToolStripMenuItem();
            this.mCopySelected = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.mSendToTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mSendToNewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mAfterSendTo = new System.Windows.Forms.ToolStripSeparator();
            this.ilSmall = new System.Windows.Forms.ImageList(this.components);
            this.ilLarge = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tsBottom.SuspendLayout();
            this.popTray.SuspendLayout();
            this.popPages.SuspendLayout();
            this.popComps.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.picLogo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(564, 90);
            this.panel1.TabIndex = 14;
            // 
            // picLogo
            // 
            this.picLogo.Image = global::SkivSoft.LanExchange.Properties.Resources.LanExchange_48x48;
            this.picLogo.Location = new System.Drawing.Point(16, 16);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(48, 48);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLogo.TabIndex = 2;
            this.picLogo.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(70, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shared Folders";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(70, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(479, 51);
            this.label2.TabIndex = 1;
            this.label2.Text = "Here you can see computers of local area network. Search by computer description " +
                "is available.";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lItemsCount,
            this.toolStripStatusLabel1,
            this.lCompName,
            this.toolStripStatusLabel3,
            this.lUserName});
            this.statusStrip1.Location = new System.Drawing.Point(0, 520);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(564, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lItemsCount
            // 
            this.lItemsCount.Name = "lItemsCount";
            this.lItemsCount.Size = new System.Drawing.Size(440, 17);
            this.lItemsCount.Spring = true;
            this.lItemsCount.Text = "    ";
            this.lItemsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.lCompName.Image = global::SkivSoft.LanExchange.Properties.Resources.CompOff;
            this.lCompName.Name = "lCompName";
            this.lCompName.Size = new System.Drawing.Size(35, 17);
            this.lCompName.Text = "    ";
            this.lCompName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(4, 17);
            this.toolStripStatusLabel3.Text = "toolStripStatusLabel3";
            // 
            // lUserName
            // 
            this.lUserName.Image = global::SkivSoft.LanExchange.Properties.Resources.UserName;
            this.lUserName.Name = "lUserName";
            this.lUserName.Size = new System.Drawing.Size(35, 17);
            this.lUserName.Text = "    ";
            this.lUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsBottom
            // 
            this.tsBottom.Controls.Add(this.label3);
            this.tsBottom.Controls.Add(this.eFilter);
            this.tsBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsBottom.Location = new System.Drawing.Point(0, 488);
            this.tsBottom.Name = "tsBottom";
            this.tsBottom.Size = new System.Drawing.Size(564, 32);
            this.tsBottom.TabIndex = 22;
            this.tsBottom.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Find:";
            // 
            // eFilter
            // 
            this.eFilter.BackColor = System.Drawing.Color.White;
            this.eFilter.Location = new System.Drawing.Point(56, 6);
            this.eFilter.Name = "eFilter";
            this.eFilter.Size = new System.Drawing.Size(200, 20);
            this.eFilter.TabIndex = 4;
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
            this.popTray.Size = new System.Drawing.Size(145, 104);
            // 
            // mOpen
            // 
            this.mOpen.Name = "mOpen";
            this.mOpen.Size = new System.Drawing.Size(144, 22);
            this.mOpen.Text = "Open";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(141, 6);
            // 
            // mSettings
            // 
            this.mSettings.Name = "mSettings";
            this.mSettings.Size = new System.Drawing.Size(144, 22);
            this.mSettings.Text = "Preferences...";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(141, 6);
            // 
            // mAbout
            // 
            this.mAbout.Name = "mAbout";
            this.mAbout.Size = new System.Drawing.Size(144, 22);
            this.mAbout.Text = "About...";
            // 
            // mExit
            // 
            this.mExit.Name = "mExit";
            this.mExit.Size = new System.Drawing.Size(144, 22);
            this.mExit.Text = "Exit";
            // 
            // TrayIcon
            // 
            this.TrayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.TrayIcon.BalloonTipTitle = "Оповещение";
            this.TrayIcon.ContextMenuStrip = this.popTray;
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "Общие папки";
            this.TrayIcon.Visible = true;
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "txt";
            this.dlgSave.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            this.dlgSave.RestoreDirectory = true;
            // 
            // Pages
            // 
            this.Pages.ContextMenuStrip = this.popPages;
            this.Pages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pages.Location = new System.Drawing.Point(0, 90);
            this.Pages.Name = "Pages";
            this.Pages.SelectedIndex = 0;
            this.Pages.Size = new System.Drawing.Size(564, 398);
            this.Pages.TabIndex = 23;
            // 
            // popPages
            // 
            this.popPages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mNewTab,
            this.toolStripSeparator9,
            this.mCloseTab,
            this.mRenameTab,
            this.mSaveTab,
            this.toolStripSeparator10,
            this.mSelectTab,
            this.toolStripSeparator11,
            this.mListTab});
            this.popPages.Name = "popPages";
            this.popPages.Size = new System.Drawing.Size(175, 154);
            this.popPages.Opened += new System.EventHandler(this.popPages_Opened);
            // 
            // mNewTab
            // 
            this.mNewTab.Name = "mNewTab";
            this.mNewTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.mNewTab.Size = new System.Drawing.Size(174, 22);
            this.mNewTab.Text = "New tab";
            this.mNewTab.Click += new System.EventHandler(this.mNewTab_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(171, 6);
            // 
            // mCloseTab
            // 
            this.mCloseTab.Name = "mCloseTab";
            this.mCloseTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.mCloseTab.Size = new System.Drawing.Size(174, 22);
            this.mCloseTab.Text = "Close tab";
            this.mCloseTab.Click += new System.EventHandler(this.mCloseTab_Click);
            // 
            // mRenameTab
            // 
            this.mRenameTab.Name = "mRenameTab";
            this.mRenameTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.mRenameTab.Size = new System.Drawing.Size(174, 22);
            this.mRenameTab.Text = "Rename tab";
            this.mRenameTab.Click += new System.EventHandler(this.mRenameTab_Click);
            // 
            // mSaveTab
            // 
            this.mSaveTab.Name = "mSaveTab";
            this.mSaveTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mSaveTab.Size = new System.Drawing.Size(174, 22);
            this.mSaveTab.Text = "Save tab";
            this.mSaveTab.Click += new System.EventHandler(this.mSaveTab_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(171, 6);
            // 
            // mSelectTab
            // 
            this.mSelectTab.Name = "mSelectTab";
            this.mSelectTab.Size = new System.Drawing.Size(174, 22);
            this.mSelectTab.Text = "Select tab";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(171, 6);
            this.toolStripSeparator11.Visible = false;
            // 
            // mListTab
            // 
            this.mListTab.Name = "mListTab";
            this.mListTab.Size = new System.Drawing.Size(174, 22);
            this.mListTab.Text = "Tab list";
            this.mListTab.Visible = false;
            this.mListTab.Click += new System.EventHandler(this.mListTab_Click);
            // 
            // tipComps
            // 
            this.tipComps.Active = false;
            this.tipComps.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.tipComps.ToolTipTitle = "Test";
            this.tipComps.Popup += new System.Windows.Forms.PopupEventHandler(this.tipComps_Popup);
            // 
            // popComps
            // 
            this.popComps.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mLargeIcons,
            this.mSmallIcons,
            this.mList,
            this.mDetails,
            this.toolStripSeparator5,
            this.mCopyCompName,
            this.mCopySelected,
            this.toolStripSeparator12,
            this.mSendToTab});
            this.popComps.Name = "popComps";
            this.popComps.Size = new System.Drawing.Size(179, 170);
            // 
            // mLargeIcons
            // 
            this.mLargeIcons.Name = "mLargeIcons";
            this.mLargeIcons.Size = new System.Drawing.Size(178, 22);
            this.mLargeIcons.Tag = "1";
            this.mLargeIcons.Text = "Large icons";
            // 
            // mSmallIcons
            // 
            this.mSmallIcons.Name = "mSmallIcons";
            this.mSmallIcons.Size = new System.Drawing.Size(178, 22);
            this.mSmallIcons.Tag = "3";
            this.mSmallIcons.Text = "Small icons";
            // 
            // mList
            // 
            this.mList.Name = "mList";
            this.mList.Size = new System.Drawing.Size(178, 22);
            this.mList.Tag = "4";
            this.mList.Text = "List";
            // 
            // mDetails
            // 
            this.mDetails.Checked = true;
            this.mDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mDetails.Name = "mDetails";
            this.mDetails.Size = new System.Drawing.Size(178, 22);
            this.mDetails.Tag = "2";
            this.mDetails.Text = "Details";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(175, 6);
            // 
            // mCopyCompName
            // 
            this.mCopyCompName.Name = "mCopyCompName";
            this.mCopyCompName.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mCopyCompName.Size = new System.Drawing.Size(178, 22);
            this.mCopyCompName.Tag = "";
            this.mCopyCompName.Text = "Copy";
            // 
            // mCopySelected
            // 
            this.mCopySelected.Name = "mCopySelected";
            this.mCopySelected.Size = new System.Drawing.Size(178, 22);
            this.mCopySelected.Text = "Copy row";
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(175, 6);
            // 
            // mSendToTab
            // 
            this.mSendToTab.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSendToNewTab,
            this.mAfterSendTo});
            this.mSendToTab.Name = "mSendToTab";
            this.mSendToTab.Size = new System.Drawing.Size(178, 22);
            this.mSendToTab.Text = "Send to another tab";
            // 
            // mSendToNewTab
            // 
            this.mSendToNewTab.Name = "mSendToNewTab";
            this.mSendToNewTab.Size = new System.Drawing.Size(133, 22);
            this.mSendToNewTab.Text = "To new tab";
            // 
            // mAfterSendTo
            // 
            this.mAfterSendTo.Name = "mAfterSendTo";
            this.mAfterSendTo.Size = new System.Drawing.Size(130, 6);
            // 
            // ilSmall
            // 
            this.ilSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ilSmall.ImageSize = new System.Drawing.Size(16, 16);
            this.ilSmall.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ilLarge
            // 
            this.ilLarge.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ilLarge.ImageSize = new System.Drawing.Size(32, 32);
            this.ilLarge.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 542);
            this.Controls.Add(this.Pages);
            this.Controls.Add(this.tsBottom);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LanExchange";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tsBottom.ResumeLayout(false);
            this.tsBottom.PerformLayout();
            this.popTray.ResumeLayout(false);
            this.popPages.ResumeLayout(false);
            this.popComps.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lItemsCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lCompName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lUserName;
        private System.Windows.Forms.Panel tsBottom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox eFilter;
        private System.Windows.Forms.ContextMenuStrip popTray;
        private System.Windows.Forms.ToolStripMenuItem mOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mAbout;
        private System.Windows.Forms.ToolStripMenuItem mExit;
        private System.Windows.Forms.NotifyIcon TrayIcon;
        public System.Windows.Forms.SaveFileDialog dlgSave;
        public System.Windows.Forms.TabControl Pages;
        private System.Windows.Forms.ContextMenuStrip popPages;
        private System.Windows.Forms.ToolStripMenuItem mNewTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem mCloseTab;
        private System.Windows.Forms.ToolStripMenuItem mRenameTab;
        private System.Windows.Forms.ToolStripMenuItem mSaveTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem mSelectTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem mListTab;
        public System.Windows.Forms.ToolTip tipComps;
        public System.Windows.Forms.ContextMenuStrip popComps;
        private System.Windows.Forms.ToolStripMenuItem mLargeIcons;
        private System.Windows.Forms.ToolStripMenuItem mSmallIcons;
        private System.Windows.Forms.ToolStripMenuItem mList;
        private System.Windows.Forms.ToolStripMenuItem mDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mCopyCompName;
        private System.Windows.Forms.ToolStripMenuItem mCopySelected;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem mSendToTab;
        private System.Windows.Forms.ToolStripMenuItem mSendToNewTab;
        private System.Windows.Forms.ToolStripSeparator mAfterSendTo;
        public System.Windows.Forms.ImageList ilSmall;
        public System.Windows.Forms.ImageList ilLarge;

    }
}

