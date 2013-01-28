namespace LanExchange.UI
{
    partial class PanelView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LV = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.popComps = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mComp = new System.Windows.Forms.ToolStripMenuItem();
            this.mCompOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mWMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.mCompService = new System.Windows.Forms.ToolStripMenuItem();
            this.mCompMSTSC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mRadmin1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mRadmin2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mRadmin3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mRadmin4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mRadmin5 = new System.Windows.Forms.ToolStripMenuItem();
            this.mFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.mFolderOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mFAROpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mSeparatorAdmin = new System.Windows.Forms.ToolStripSeparator();
            this.mCompLargeIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.mCompSmallIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.mCompList = new System.Windows.Forms.ToolStripMenuItem();
            this.mCompDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.mCopySeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mCopyPath = new System.Windows.Forms.ToolStripMenuItem();
            this.mCopyCompName = new System.Windows.Forms.ToolStripMenuItem();
            this.mCopyComment = new System.Windows.Forms.ToolStripMenuItem();
            this.mCopySelected = new System.Windows.Forms.ToolStripMenuItem();
            this.mSendSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mSendToNewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mContextClose = new System.Windows.Forms.ToolStripMenuItem();
            this.ePath = new System.Windows.Forms.TextBox();
            this.pFilter = new LanExchange.UI.FilterView();
            this.popComps.SuspendLayout();
            this.SuspendLayout();
            // 
            // LV
            // 
            this.LV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.LV.ContextMenuStrip = this.popComps;
            this.LV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV.FullRowSelect = true;
            this.LV.GridLines = true;
            this.LV.HideSelection = false;
            this.LV.Location = new System.Drawing.Point(0, 20);
            this.LV.Margin = new System.Windows.Forms.Padding(0);
            this.LV.Name = "LV";
            this.LV.ShowItemToolTips = true;
            this.LV.Size = new System.Drawing.Size(423, 348);
            this.LV.TabIndex = 26;
            this.LV.UseCompatibleStateImageBehavior = false;
            this.LV.View = System.Windows.Forms.View.Details;
            this.LV.VirtualMode = true;
            this.LV.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LV_ColumnClick);
            this.LV.ItemActivate += new System.EventHandler(this.lvComps_ItemActivate);
            this.LV.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvComps_ItemSelectionChanged);
            this.LV.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvComps_KeyDown);
            this.LV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvComps_KeyPress);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Сетевое имя";
            this.columnHeader1.Width = 130;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Описание";
            this.columnHeader2.Width = 250;
            // 
            // popComps
            // 
            this.popComps.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mComp,
            this.mFolder,
            this.mSeparatorAdmin,
            this.mCompLargeIcons,
            this.mCompSmallIcons,
            this.mCompList,
            this.mCompDetails,
            this.mCopySeparator,
            this.mCopyPath,
            this.mCopyCompName,
            this.mCopyComment,
            this.mCopySelected,
            this.mSendSeparator,
            this.mSendToNewTab,
            this.toolStripSeparator6,
            this.mContextClose});
            this.popComps.Name = "popComps";
            this.popComps.Size = new System.Drawing.Size(279, 314);
            this.popComps.Opening += new System.ComponentModel.CancelEventHandler(this.popComps_Opening);
            // 
            // mComp
            // 
            this.mComp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mCompOpen,
            this.mWMI,
            this.toolStripSeparator7,
            this.mCompService,
            this.mCompMSTSC,
            this.toolStripSeparator1,
            this.mRadmin1,
            this.mRadmin2,
            this.mRadmin3,
            this.mRadmin4,
            this.mRadmin5});
            this.mComp.Enabled = false;
            this.mComp.Name = "mComp";
            this.mComp.Size = new System.Drawing.Size(278, 22);
            this.mComp.Tag = "";
            this.mComp.Text = "\\\\COMPUTER";
            this.mComp.Visible = false;
            // 
            // mCompOpen
            // 
            this.mCompOpen.Name = "mCompOpen";
            this.mCompOpen.ShortcutKeyDisplayString = "Shift+Enter";
            this.mCompOpen.Size = new System.Drawing.Size(341, 22);
            this.mCompOpen.Tag = "\\\\{0}";
            this.mCompOpen.Text = "Открыть в проводнике";
            this.mCompOpen.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mWMI
            // 
            this.mWMI.Name = "mWMI";
            this.mWMI.ShortcutKeyDisplayString = "";
            this.mWMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F1)));
            this.mWMI.Size = new System.Drawing.Size(341, 22);
            this.mWMI.Text = "Инструментарий управления Windows®";
            this.mWMI.Click += new System.EventHandler(this.mWMI_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(338, 6);
            // 
            // mCompService
            // 
            this.mCompService.Name = "mCompService";
            this.mCompService.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.mCompService.Size = new System.Drawing.Size(341, 22);
            this.mCompService.Tag = "%systemroot%\\system32\\mmc.exe %systemroot%\\system32\\compmgmt.msc /computer:{0}";
            this.mCompService.Text = "Управление компьютером";
            this.mCompService.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mCompMSTSC
            // 
            this.mCompMSTSC.Name = "mCompMSTSC";
            this.mCompMSTSC.Size = new System.Drawing.Size(341, 22);
            this.mCompMSTSC.Tag = "%systemroot%\\system32\\mstsc.exe /v:{0}";
            this.mCompMSTSC.Text = "Подключение к удаленному рабочему столу";
            this.mCompMSTSC.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(338, 6);
            // 
            // mRadmin1
            // 
            this.mRadmin1.Name = "mRadmin1";
            this.mRadmin1.ShortcutKeyDisplayString = "Ctrl+Enter";
            this.mRadmin1.Size = new System.Drawing.Size(341, 22);
            this.mRadmin1.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0}";
            this.mRadmin1.Text = "Radmin® Управление";
            this.mRadmin1.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mRadmin2
            // 
            this.mRadmin2.Name = "mRadmin2";
            this.mRadmin2.Size = new System.Drawing.Size(341, 22);
            this.mRadmin2.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /noinput";
            this.mRadmin2.Text = "Radmin® Просмотр";
            this.mRadmin2.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mRadmin3
            // 
            this.mRadmin3.Name = "mRadmin3";
            this.mRadmin3.Size = new System.Drawing.Size(341, 22);
            this.mRadmin3.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /telnet";
            this.mRadmin3.Text = "Radmin® Телнет";
            this.mRadmin3.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mRadmin4
            // 
            this.mRadmin4.Name = "mRadmin4";
            this.mRadmin4.Size = new System.Drawing.Size(341, 22);
            this.mRadmin4.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /file";
            this.mRadmin4.Text = "Radmin® Передача файлов";
            this.mRadmin4.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mRadmin5
            // 
            this.mRadmin5.Name = "mRadmin5";
            this.mRadmin5.Size = new System.Drawing.Size(341, 22);
            this.mRadmin5.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /shutdown";
            this.mRadmin5.Text = "Radmin® Выключение";
            this.mRadmin5.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mFolder
            // 
            this.mFolder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFolderOpen,
            this.mFAROpen});
            this.mFolder.Enabled = false;
            this.mFolder.Name = "mFolder";
            this.mFolder.Size = new System.Drawing.Size(278, 22);
            this.mFolder.Tag = "";
            this.mFolder.Text = "\\\\COMPUTER\\FOLDER";
            this.mFolder.Visible = false;
            // 
            // mFolderOpen
            // 
            this.mFolderOpen.Name = "mFolderOpen";
            this.mFolderOpen.ShortcutKeyDisplayString = "Shift+Enter";
            this.mFolderOpen.Size = new System.Drawing.Size(265, 22);
            this.mFolderOpen.Tag = "{0}";
            this.mFolderOpen.Text = "Открыть в проводнике";
            this.mFolderOpen.Click += new System.EventHandler(this.mFolderOpen_Click);
            // 
            // mFAROpen
            // 
            this.mFAROpen.Name = "mFAROpen";
            this.mFAROpen.ShortcutKeyDisplayString = "Ctrl+Enter";
            this.mFAROpen.Size = new System.Drawing.Size(265, 22);
            this.mFAROpen.Tag = "FAR.EXE {0}";
            this.mFAROpen.Text = "Открыть в Far Manager";
            this.mFAROpen.Click += new System.EventHandler(this.mFolderOpen_Click);
            // 
            // mSeparatorAdmin
            // 
            this.mSeparatorAdmin.Name = "mSeparatorAdmin";
            this.mSeparatorAdmin.Size = new System.Drawing.Size(275, 6);
            this.mSeparatorAdmin.Visible = false;
            // 
            // mCompLargeIcons
            // 
            this.mCompLargeIcons.Name = "mCompLargeIcons";
            this.mCompLargeIcons.Size = new System.Drawing.Size(278, 22);
            this.mCompLargeIcons.Tag = "1";
            this.mCompLargeIcons.Text = "Обычные значки";
            this.mCompLargeIcons.Click += new System.EventHandler(this.mLargeIcons_Click);
            // 
            // mCompSmallIcons
            // 
            this.mCompSmallIcons.Name = "mCompSmallIcons";
            this.mCompSmallIcons.Size = new System.Drawing.Size(278, 22);
            this.mCompSmallIcons.Tag = "2";
            this.mCompSmallIcons.Text = "Мелкие значки";
            this.mCompSmallIcons.Click += new System.EventHandler(this.mLargeIcons_Click);
            // 
            // mCompList
            // 
            this.mCompList.Name = "mCompList";
            this.mCompList.Size = new System.Drawing.Size(278, 22);
            this.mCompList.Tag = "3";
            this.mCompList.Text = "Список";
            this.mCompList.Click += new System.EventHandler(this.mLargeIcons_Click);
            // 
            // mCompDetails
            // 
            this.mCompDetails.Checked = true;
            this.mCompDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mCompDetails.Name = "mCompDetails";
            this.mCompDetails.Size = new System.Drawing.Size(278, 22);
            this.mCompDetails.Tag = "4";
            this.mCompDetails.Text = "Таблица";
            this.mCompDetails.Click += new System.EventHandler(this.mLargeIcons_Click);
            // 
            // mCopySeparator
            // 
            this.mCopySeparator.Name = "mCopySeparator";
            this.mCopySeparator.Size = new System.Drawing.Size(275, 6);
            // 
            // mCopyPath
            // 
            this.mCopyPath.Enabled = false;
            this.mCopyPath.Name = "mCopyPath";
            this.mCopyPath.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mCopyPath.Size = new System.Drawing.Size(278, 22);
            this.mCopyPath.Text = "Копировать «Общий ресурс»";
            this.mCopyPath.Visible = false;
            this.mCopyPath.Click += new System.EventHandler(this.mCopyPath_Click);
            // 
            // mCopyCompName
            // 
            this.mCopyCompName.Name = "mCopyCompName";
            this.mCopyCompName.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mCopyCompName.Size = new System.Drawing.Size(278, 22);
            this.mCopyCompName.Text = "Копировать «Сетевое имя»";
            this.mCopyCompName.Click += new System.EventHandler(this.mCopyCompName_Click);
            // 
            // mCopyComment
            // 
            this.mCopyComment.Name = "mCopyComment";
            this.mCopyComment.Size = new System.Drawing.Size(278, 22);
            this.mCopyComment.Text = "Копировать «Описание»";
            this.mCopyComment.Click += new System.EventHandler(this.mCopyComment_Click);
            // 
            // mCopySelected
            // 
            this.mCopySelected.Name = "mCopySelected";
            this.mCopySelected.Size = new System.Drawing.Size(278, 22);
            this.mCopySelected.Text = "Копировать строку";
            this.mCopySelected.Click += new System.EventHandler(this.mCopySelected_Click);
            // 
            // mSendSeparator
            // 
            this.mSendSeparator.Name = "mSendSeparator";
            this.mSendSeparator.Size = new System.Drawing.Size(275, 6);
            // 
            // mSendToNewTab
            // 
            this.mSendToNewTab.Name = "mSendToNewTab";
            this.mSendToNewTab.Size = new System.Drawing.Size(278, 22);
            this.mSendToNewTab.Text = "Отправить в другую вкладку...";
            this.mSendToNewTab.Click += new System.EventHandler(this.mSendToNewTab_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(275, 6);
            // 
            // mContextClose
            // 
            this.mContextClose.Name = "mContextClose";
            this.mContextClose.Size = new System.Drawing.Size(278, 22);
            this.mContextClose.Text = "Закрыть";
            this.mContextClose.Click += new System.EventHandler(this.mContextClose_Click);
            // 
            // ePath
            // 
            this.ePath.BackColor = System.Drawing.SystemColors.Control;
            this.ePath.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ePath.Dock = System.Windows.Forms.DockStyle.Top;
            this.ePath.Enabled = false;
            this.ePath.Location = new System.Drawing.Point(0, 0);
            this.ePath.Margin = new System.Windows.Forms.Padding(0);
            this.ePath.Name = "ePath";
            this.ePath.ReadOnly = true;
            this.ePath.Size = new System.Drawing.Size(423, 20);
            this.ePath.TabIndex = 25;
            this.ePath.TabStop = false;
            this.ePath.DoubleClick += new System.EventHandler(this.ePath_DoubleClick);
            this.ePath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ePath_KeyDown);
            this.ePath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvComps_KeyPress);
            // 
            // pFilter
            // 
            this.pFilter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pFilter.IsVisible = true;
            this.pFilter.LinkedControl = this.LV;
            this.pFilter.Location = new System.Drawing.Point(0, 368);
            this.pFilter.Margin = new System.Windows.Forms.Padding(0);
            this.pFilter.Name = "pFilter";
            this.pFilter.Size = new System.Drawing.Size(423, 30);
            this.pFilter.TabIndex = 27;
            this.pFilter.Visible = false;
            this.pFilter.FilterCountChanged += new System.EventHandler(this.pFilter_FilterCountChanged);
            // 
            // PanelView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LV);
            this.Controls.Add(this.ePath);
            this.Controls.Add(this.pFilter);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PanelView";
            this.Size = new System.Drawing.Size(423, 398);
            this.popComps.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView LV;
        public System.Windows.Forms.ContextMenuStrip popComps;
        public System.Windows.Forms.ToolStripMenuItem mComp;
        private System.Windows.Forms.ToolStripMenuItem mCompOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem mCompService;
        private System.Windows.Forms.ToolStripMenuItem mCompMSTSC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mRadmin1;
        private System.Windows.Forms.ToolStripMenuItem mRadmin2;
        private System.Windows.Forms.ToolStripMenuItem mRadmin3;
        private System.Windows.Forms.ToolStripMenuItem mRadmin4;
        private System.Windows.Forms.ToolStripMenuItem mRadmin5;
        private System.Windows.Forms.ToolStripMenuItem mFolder;
        public System.Windows.Forms.ToolStripMenuItem mFolderOpen;
        private System.Windows.Forms.ToolStripMenuItem mFAROpen;
        private System.Windows.Forms.ToolStripSeparator mSeparatorAdmin;
        private System.Windows.Forms.ToolStripMenuItem mCompLargeIcons;
        private System.Windows.Forms.ToolStripMenuItem mCompSmallIcons;
        private System.Windows.Forms.ToolStripMenuItem mCompList;
        private System.Windows.Forms.ToolStripMenuItem mCompDetails;
        private System.Windows.Forms.ToolStripSeparator mCopySeparator;
        private System.Windows.Forms.ToolStripMenuItem mCopyPath;
        private System.Windows.Forms.ToolStripMenuItem mCopyCompName;
        private System.Windows.Forms.ToolStripMenuItem mCopyComment;
        private System.Windows.Forms.ToolStripMenuItem mCopySelected;
        private System.Windows.Forms.ToolStripSeparator mSendSeparator;
        private System.Windows.Forms.ToolStripMenuItem mSendToNewTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mContextClose;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripMenuItem mWMI;
        private FilterView pFilter;
        private System.Windows.Forms.TextBox ePath;
    }
}
