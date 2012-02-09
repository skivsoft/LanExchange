namespace SkivSoft.LanExchange
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lItemsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.lCompName = new System.Windows.Forms.ToolStripStatusLabel();
            this.lUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tsBottom.SuspendLayout();
            this.popTray.SuspendLayout();
            this.popPages.SuspendLayout();
            this.popComps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = null;
            this.panel1.AccessibleName = null;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = null;
            this.panel1.Controls.Add(this.picLogo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Font = null;
            this.panel1.Name = "panel1";
            this.tipComps.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Name = "label1";
            this.tipComps.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Font = null;
            this.label2.Name = "label2";
            this.tipComps.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // statusStrip1
            // 
            this.statusStrip1.AccessibleDescription = null;
            this.statusStrip1.AccessibleName = null;
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.BackgroundImage = null;
            this.statusStrip1.Font = null;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lItemsCount,
            this.toolStripStatusLabel1,
            this.lCompName,
            this.toolStripStatusLabel3,
            this.lUserName});
            this.statusStrip1.Name = "statusStrip1";
            this.tipComps.SetToolTip(this.statusStrip1, resources.GetString("statusStrip1.ToolTip"));
            // 
            // lItemsCount
            // 
            this.lItemsCount.AccessibleDescription = null;
            this.lItemsCount.AccessibleName = null;
            resources.ApplyResources(this.lItemsCount, "lItemsCount");
            this.lItemsCount.BackgroundImage = null;
            this.lItemsCount.Name = "lItemsCount";
            this.lItemsCount.Spring = true;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AccessibleDescription = null;
            this.toolStripStatusLabel1.AccessibleName = null;
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.BackgroundImage = null;
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.AccessibleDescription = null;
            this.toolStripStatusLabel3.AccessibleName = null;
            resources.ApplyResources(this.toolStripStatusLabel3, "toolStripStatusLabel3");
            this.toolStripStatusLabel3.BackgroundImage = null;
            this.toolStripStatusLabel3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            // 
            // tsBottom
            // 
            this.tsBottom.AccessibleDescription = null;
            this.tsBottom.AccessibleName = null;
            resources.ApplyResources(this.tsBottom, "tsBottom");
            this.tsBottom.BackgroundImage = null;
            this.tsBottom.Controls.Add(this.label3);
            this.tsBottom.Controls.Add(this.eFilter);
            this.tsBottom.Font = null;
            this.tsBottom.Name = "tsBottom";
            this.tipComps.SetToolTip(this.tsBottom, resources.GetString("tsBottom.ToolTip"));
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.Font = null;
            this.label3.Name = "label3";
            this.tipComps.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // eFilter
            // 
            this.eFilter.AccessibleDescription = null;
            this.eFilter.AccessibleName = null;
            resources.ApplyResources(this.eFilter, "eFilter");
            this.eFilter.BackColor = System.Drawing.Color.White;
            this.eFilter.BackgroundImage = null;
            this.eFilter.Font = null;
            this.eFilter.Name = "eFilter";
            this.tipComps.SetToolTip(this.eFilter, resources.GetString("eFilter.ToolTip"));
            // 
            // popTray
            // 
            this.popTray.AccessibleDescription = null;
            this.popTray.AccessibleName = null;
            resources.ApplyResources(this.popTray, "popTray");
            this.popTray.BackgroundImage = null;
            this.popTray.Font = null;
            this.popTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mOpen,
            this.toolStripSeparator3,
            this.mSettings,
            this.toolStripSeparator4,
            this.mAbout,
            this.mExit});
            this.popTray.Name = "popTray";
            this.tipComps.SetToolTip(this.popTray, resources.GetString("popTray.ToolTip"));
            // 
            // mOpen
            // 
            this.mOpen.AccessibleDescription = null;
            this.mOpen.AccessibleName = null;
            resources.ApplyResources(this.mOpen, "mOpen");
            this.mOpen.BackgroundImage = null;
            this.mOpen.Name = "mOpen";
            this.mOpen.ShortcutKeyDisplayString = null;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AccessibleDescription = null;
            this.toolStripSeparator3.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // mSettings
            // 
            this.mSettings.AccessibleDescription = null;
            this.mSettings.AccessibleName = null;
            resources.ApplyResources(this.mSettings, "mSettings");
            this.mSettings.BackgroundImage = null;
            this.mSettings.Name = "mSettings";
            this.mSettings.ShortcutKeyDisplayString = null;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AccessibleDescription = null;
            this.toolStripSeparator4.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // mAbout
            // 
            this.mAbout.AccessibleDescription = null;
            this.mAbout.AccessibleName = null;
            resources.ApplyResources(this.mAbout, "mAbout");
            this.mAbout.BackgroundImage = null;
            this.mAbout.Name = "mAbout";
            this.mAbout.ShortcutKeyDisplayString = null;
            // 
            // mExit
            // 
            this.mExit.AccessibleDescription = null;
            this.mExit.AccessibleName = null;
            resources.ApplyResources(this.mExit, "mExit");
            this.mExit.BackgroundImage = null;
            this.mExit.Name = "mExit";
            this.mExit.ShortcutKeyDisplayString = null;
            // 
            // TrayIcon
            // 
            this.TrayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            resources.ApplyResources(this.TrayIcon, "TrayIcon");
            this.TrayIcon.ContextMenuStrip = this.popTray;
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "txt";
            resources.ApplyResources(this.dlgSave, "dlgSave");
            this.dlgSave.RestoreDirectory = true;
            // 
            // Pages
            // 
            this.Pages.AccessibleDescription = null;
            this.Pages.AccessibleName = null;
            resources.ApplyResources(this.Pages, "Pages");
            this.Pages.BackgroundImage = null;
            this.Pages.ContextMenuStrip = this.popPages;
            this.Pages.Font = null;
            this.Pages.Name = "Pages";
            this.Pages.SelectedIndex = 0;
            this.tipComps.SetToolTip(this.Pages, resources.GetString("Pages.ToolTip"));
            // 
            // popPages
            // 
            this.popPages.AccessibleDescription = null;
            this.popPages.AccessibleName = null;
            resources.ApplyResources(this.popPages, "popPages");
            this.popPages.BackgroundImage = null;
            this.popPages.Font = null;
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
            this.tipComps.SetToolTip(this.popPages, resources.GetString("popPages.ToolTip"));
            this.popPages.Opened += new System.EventHandler(this.popPages_Opened);
            // 
            // mNewTab
            // 
            this.mNewTab.AccessibleDescription = null;
            this.mNewTab.AccessibleName = null;
            resources.ApplyResources(this.mNewTab, "mNewTab");
            this.mNewTab.BackgroundImage = null;
            this.mNewTab.Name = "mNewTab";
            this.mNewTab.ShortcutKeyDisplayString = null;
            this.mNewTab.Click += new System.EventHandler(this.mNewTab_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.AccessibleDescription = null;
            this.toolStripSeparator9.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            // 
            // mCloseTab
            // 
            this.mCloseTab.AccessibleDescription = null;
            this.mCloseTab.AccessibleName = null;
            resources.ApplyResources(this.mCloseTab, "mCloseTab");
            this.mCloseTab.BackgroundImage = null;
            this.mCloseTab.Name = "mCloseTab";
            this.mCloseTab.ShortcutKeyDisplayString = null;
            this.mCloseTab.Click += new System.EventHandler(this.mCloseTab_Click);
            // 
            // mRenameTab
            // 
            this.mRenameTab.AccessibleDescription = null;
            this.mRenameTab.AccessibleName = null;
            resources.ApplyResources(this.mRenameTab, "mRenameTab");
            this.mRenameTab.BackgroundImage = null;
            this.mRenameTab.Name = "mRenameTab";
            this.mRenameTab.ShortcutKeyDisplayString = null;
            this.mRenameTab.Click += new System.EventHandler(this.mRenameTab_Click);
            // 
            // mSaveTab
            // 
            this.mSaveTab.AccessibleDescription = null;
            this.mSaveTab.AccessibleName = null;
            resources.ApplyResources(this.mSaveTab, "mSaveTab");
            this.mSaveTab.BackgroundImage = null;
            this.mSaveTab.Name = "mSaveTab";
            this.mSaveTab.ShortcutKeyDisplayString = null;
            this.mSaveTab.Click += new System.EventHandler(this.mSaveTab_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.AccessibleDescription = null;
            this.toolStripSeparator10.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator10, "toolStripSeparator10");
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            // 
            // mSelectTab
            // 
            this.mSelectTab.AccessibleDescription = null;
            this.mSelectTab.AccessibleName = null;
            resources.ApplyResources(this.mSelectTab, "mSelectTab");
            this.mSelectTab.BackgroundImage = null;
            this.mSelectTab.Name = "mSelectTab";
            this.mSelectTab.ShortcutKeyDisplayString = null;
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.AccessibleDescription = null;
            this.toolStripSeparator11.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator11, "toolStripSeparator11");
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            // 
            // mListTab
            // 
            this.mListTab.AccessibleDescription = null;
            this.mListTab.AccessibleName = null;
            resources.ApplyResources(this.mListTab, "mListTab");
            this.mListTab.BackgroundImage = null;
            this.mListTab.Name = "mListTab";
            this.mListTab.ShortcutKeyDisplayString = null;
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
            this.popComps.AccessibleDescription = null;
            this.popComps.AccessibleName = null;
            resources.ApplyResources(this.popComps, "popComps");
            this.popComps.BackgroundImage = null;
            this.popComps.Font = null;
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
            this.tipComps.SetToolTip(this.popComps, resources.GetString("popComps.ToolTip"));
            // 
            // mLargeIcons
            // 
            this.mLargeIcons.AccessibleDescription = null;
            this.mLargeIcons.AccessibleName = null;
            resources.ApplyResources(this.mLargeIcons, "mLargeIcons");
            this.mLargeIcons.BackgroundImage = null;
            this.mLargeIcons.Name = "mLargeIcons";
            this.mLargeIcons.ShortcutKeyDisplayString = null;
            this.mLargeIcons.Tag = "1";
            // 
            // mSmallIcons
            // 
            this.mSmallIcons.AccessibleDescription = null;
            this.mSmallIcons.AccessibleName = null;
            resources.ApplyResources(this.mSmallIcons, "mSmallIcons");
            this.mSmallIcons.BackgroundImage = null;
            this.mSmallIcons.Name = "mSmallIcons";
            this.mSmallIcons.ShortcutKeyDisplayString = null;
            this.mSmallIcons.Tag = "3";
            // 
            // mList
            // 
            this.mList.AccessibleDescription = null;
            this.mList.AccessibleName = null;
            resources.ApplyResources(this.mList, "mList");
            this.mList.BackgroundImage = null;
            this.mList.Name = "mList";
            this.mList.ShortcutKeyDisplayString = null;
            this.mList.Tag = "4";
            // 
            // mDetails
            // 
            this.mDetails.AccessibleDescription = null;
            this.mDetails.AccessibleName = null;
            resources.ApplyResources(this.mDetails, "mDetails");
            this.mDetails.BackgroundImage = null;
            this.mDetails.Checked = true;
            this.mDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mDetails.Name = "mDetails";
            this.mDetails.ShortcutKeyDisplayString = null;
            this.mDetails.Tag = "2";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AccessibleDescription = null;
            this.toolStripSeparator5.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // mCopyCompName
            // 
            this.mCopyCompName.AccessibleDescription = null;
            this.mCopyCompName.AccessibleName = null;
            resources.ApplyResources(this.mCopyCompName, "mCopyCompName");
            this.mCopyCompName.BackgroundImage = null;
            this.mCopyCompName.Name = "mCopyCompName";
            this.mCopyCompName.ShortcutKeyDisplayString = null;
            this.mCopyCompName.Tag = "";
            // 
            // mCopySelected
            // 
            this.mCopySelected.AccessibleDescription = null;
            this.mCopySelected.AccessibleName = null;
            resources.ApplyResources(this.mCopySelected, "mCopySelected");
            this.mCopySelected.BackgroundImage = null;
            this.mCopySelected.Name = "mCopySelected";
            this.mCopySelected.ShortcutKeyDisplayString = null;
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.AccessibleDescription = null;
            this.toolStripSeparator12.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator12, "toolStripSeparator12");
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            // 
            // mSendToTab
            // 
            this.mSendToTab.AccessibleDescription = null;
            this.mSendToTab.AccessibleName = null;
            resources.ApplyResources(this.mSendToTab, "mSendToTab");
            this.mSendToTab.BackgroundImage = null;
            this.mSendToTab.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSendToNewTab,
            this.mAfterSendTo});
            this.mSendToTab.Name = "mSendToTab";
            this.mSendToTab.ShortcutKeyDisplayString = null;
            // 
            // mSendToNewTab
            // 
            this.mSendToNewTab.AccessibleDescription = null;
            this.mSendToNewTab.AccessibleName = null;
            resources.ApplyResources(this.mSendToNewTab, "mSendToNewTab");
            this.mSendToNewTab.BackgroundImage = null;
            this.mSendToNewTab.Name = "mSendToNewTab";
            this.mSendToNewTab.ShortcutKeyDisplayString = null;
            // 
            // mAfterSendTo
            // 
            this.mAfterSendTo.AccessibleDescription = null;
            this.mAfterSendTo.AccessibleName = null;
            resources.ApplyResources(this.mAfterSendTo, "mAfterSendTo");
            this.mAfterSendTo.Name = "mAfterSendTo";
            // 
            // ilSmall
            // 
            this.ilSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.ilSmall, "ilSmall");
            this.ilSmall.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ilLarge
            // 
            this.ilLarge.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.ilLarge, "ilLarge");
            this.ilLarge.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lCompName
            // 
            this.lCompName.AccessibleDescription = null;
            this.lCompName.AccessibleName = null;
            resources.ApplyResources(this.lCompName, "lCompName");
            this.lCompName.BackgroundImage = null;
            this.lCompName.Image = global::SkivSoft.LanExchange.Properties.Resources.CompOff;
            this.lCompName.Name = "lCompName";
            // 
            // lUserName
            // 
            this.lUserName.AccessibleDescription = null;
            this.lUserName.AccessibleName = null;
            resources.ApplyResources(this.lUserName, "lUserName");
            this.lUserName.BackgroundImage = null;
            this.lUserName.Image = global::SkivSoft.LanExchange.Properties.Resources.UserName;
            this.lUserName.Name = "lUserName";
            // 
            // picLogo
            // 
            this.picLogo.AccessibleDescription = null;
            this.picLogo.AccessibleName = null;
            resources.ApplyResources(this.picLogo, "picLogo");
            this.picLogo.BackgroundImage = null;
            this.picLogo.Font = null;
            this.picLogo.Image = global::SkivSoft.LanExchange.Properties.Resources.LanExchange_48x48;
            this.picLogo.ImageLocation = null;
            this.picLogo.Name = "picLogo";
            this.picLogo.TabStop = false;
            this.tipComps.SetToolTip(this.picLogo, resources.GetString("picLogo.ToolTip"));
            // 
            // MainForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.Pages);
            this.Controls.Add(this.tsBottom);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Font = null;
            this.Name = "MainForm";
            this.tipComps.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tsBottom.ResumeLayout(false);
            this.tsBottom.PerformLayout();
            this.popTray.ResumeLayout(false);
            this.popPages.ResumeLayout(false);
            this.popComps.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
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

