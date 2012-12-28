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
            this.tsBottom = new System.Windows.Forms.Panel();
            this.imgClear = new System.Windows.Forms.PictureBox();
            this.eFilter = new System.Windows.Forms.TextBox();
            this.LV = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.popComps = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mComp = new System.Windows.Forms.ToolStripMenuItem();
            this.mCompOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mWMIDescription = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mCopyPath = new System.Windows.Forms.ToolStripMenuItem();
            this.mCopyCompName = new System.Windows.Forms.ToolStripMenuItem();
            this.mCopyComment = new System.Windows.Forms.ToolStripMenuItem();
            this.mCopySelected = new System.Windows.Forms.ToolStripMenuItem();
            this.mSendSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mSendToTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mSendToNewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mAfterSendTo = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mContextClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tsBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgClear)).BeginInit();
            this.popComps.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsBottom
            // 
            this.tsBottom.Controls.Add(this.imgClear);
            this.tsBottom.Controls.Add(this.eFilter);
            this.tsBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsBottom.Location = new System.Drawing.Point(0, 455);
            this.tsBottom.Name = "tsBottom";
            this.tsBottom.Size = new System.Drawing.Size(470, 32);
            this.tsBottom.TabIndex = 22;
            this.tsBottom.Visible = false;
            // 
            // imgClear
            // 
            this.imgClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgClear.BackColor = System.Drawing.Color.Transparent;
            this.imgClear.Image = global::LanExchange.Properties.Resources.clear_normal;
            this.imgClear.Location = new System.Drawing.Point(448, 8);
            this.imgClear.Name = "imgClear";
            this.imgClear.Size = new System.Drawing.Size(16, 16);
            this.imgClear.TabIndex = 6;
            this.imgClear.TabStop = false;
            this.imgClear.Click += new System.EventHandler(this.imgClear_Click);
            this.imgClear.MouseLeave += new System.EventHandler(this.imgClear_MouseLeave);
            this.imgClear.MouseHover += new System.EventHandler(this.imgClear_MouseHover);
            // 
            // eFilter
            // 
            this.eFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eFilter.BackColor = System.Drawing.Color.White;
            this.eFilter.Location = new System.Drawing.Point(8, 6);
            this.eFilter.Name = "eFilter";
            this.eFilter.Size = new System.Drawing.Size(436, 20);
            this.eFilter.TabIndex = 4;
            this.eFilter.TextChanged += new System.EventHandler(this.eFilter_TextChanged);
            this.eFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.eFilter_KeyDown);
            // 
            // LV
            // 
            this.LV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.LV.ContextMenuStrip = this.popComps;
            this.LV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV.FullRowSelect = true;
            this.LV.GridLines = true;
            this.LV.HideSelection = false;
            this.LV.Location = new System.Drawing.Point(0, 0);
            this.LV.Name = "LV";
            this.LV.ShowItemToolTips = true;
            this.LV.Size = new System.Drawing.Size(470, 455);
            this.LV.TabIndex = 23;
            this.LV.UseCompatibleStateImageBehavior = false;
            this.LV.View = System.Windows.Forms.View.Details;
            this.LV.VirtualMode = true;
            this.LV.ItemActivate += new System.EventHandler(this.lvRecent_ItemActivate);
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
            this.toolStripSeparator5,
            this.mCopyPath,
            this.mCopyCompName,
            this.mCopyComment,
            this.mCopySelected,
            this.mSendSeparator,
            this.mSendToTab,
            this.toolStripSeparator6,
            this.mContextClose});
            this.popComps.Name = "popComps";
            this.popComps.Size = new System.Drawing.Size(262, 292);
            this.popComps.Opening += new System.ComponentModel.CancelEventHandler(this.popComps_Opening);
            // 
            // mComp
            // 
            this.mComp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mCompOpen,
            this.mWMIDescription,
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
            this.mComp.Size = new System.Drawing.Size(261, 22);
            this.mComp.Tag = "";
            this.mComp.Text = "\\\\COMPUTER";
            this.mComp.Visible = false;
            // 
            // mCompOpen
            // 
            this.mCompOpen.Name = "mCompOpen";
            this.mCompOpen.ShortcutKeyDisplayString = "Shift+Enter";
            this.mCompOpen.Size = new System.Drawing.Size(301, 22);
            this.mCompOpen.Tag = "\\\\{0}";
            this.mCompOpen.Text = "Открыть в Проводнике";
            // 
            // mWMIDescription
            // 
            this.mWMIDescription.Name = "mWMIDescription";
            this.mWMIDescription.Size = new System.Drawing.Size(301, 22);
            this.mWMIDescription.Text = "Редактировать описание";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(298, 6);
            // 
            // mCompService
            // 
            this.mCompService.Name = "mCompService";
            this.mCompService.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.mCompService.Size = new System.Drawing.Size(301, 22);
            this.mCompService.Tag = "%systemroot%\\system32\\mmc.exe %systemroot%\\system32\\compmgmt.msc /computer:{0}";
            this.mCompService.Text = "Управление компьютером";
            // 
            // mCompMSTSC
            // 
            this.mCompMSTSC.Name = "mCompMSTSC";
            this.mCompMSTSC.Size = new System.Drawing.Size(301, 22);
            this.mCompMSTSC.Tag = "%systemroot%\\system32\\mstsc.exe /v:{0}";
            this.mCompMSTSC.Text = "Подключение к удаленному рабочему столу";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(298, 6);
            // 
            // mRadmin1
            // 
            this.mRadmin1.Name = "mRadmin1";
            this.mRadmin1.ShortcutKeyDisplayString = "Ctrl+Enter";
            this.mRadmin1.Size = new System.Drawing.Size(301, 22);
            this.mRadmin1.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0}";
            this.mRadmin1.Text = "Управление";
            // 
            // mRadmin2
            // 
            this.mRadmin2.Name = "mRadmin2";
            this.mRadmin2.Size = new System.Drawing.Size(301, 22);
            this.mRadmin2.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /noinput";
            this.mRadmin2.Text = "Просмотр";
            // 
            // mRadmin3
            // 
            this.mRadmin3.Name = "mRadmin3";
            this.mRadmin3.Size = new System.Drawing.Size(301, 22);
            this.mRadmin3.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /telnet";
            this.mRadmin3.Text = "Телнет";
            // 
            // mRadmin4
            // 
            this.mRadmin4.Name = "mRadmin4";
            this.mRadmin4.Size = new System.Drawing.Size(301, 22);
            this.mRadmin4.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /file";
            this.mRadmin4.Text = "Передача файлов";
            // 
            // mRadmin5
            // 
            this.mRadmin5.Name = "mRadmin5";
            this.mRadmin5.Size = new System.Drawing.Size(301, 22);
            this.mRadmin5.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /shutdown";
            this.mRadmin5.Text = "Выключение";
            // 
            // mFolder
            // 
            this.mFolder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFolderOpen,
            this.mFAROpen});
            this.mFolder.Enabled = false;
            this.mFolder.Name = "mFolder";
            this.mFolder.Size = new System.Drawing.Size(261, 22);
            this.mFolder.Tag = "";
            this.mFolder.Text = "\\\\COMPUTER\\FOLDER";
            this.mFolder.Visible = false;
            // 
            // mFolderOpen
            // 
            this.mFolderOpen.Name = "mFolderOpen";
            this.mFolderOpen.ShortcutKeyDisplayString = "Shift+Enter";
            this.mFolderOpen.Size = new System.Drawing.Size(257, 22);
            this.mFolderOpen.Tag = "{0}";
            this.mFolderOpen.Text = "Открыть в Проводнике";
            // 
            // mFAROpen
            // 
            this.mFAROpen.Name = "mFAROpen";
            this.mFAROpen.ShortcutKeyDisplayString = "Ctrl+Enter";
            this.mFAROpen.Size = new System.Drawing.Size(257, 22);
            this.mFAROpen.Tag = "FAR.EXE {0}";
            this.mFAROpen.Text = "Открыть в FAR";
            // 
            // mSeparatorAdmin
            // 
            this.mSeparatorAdmin.Name = "mSeparatorAdmin";
            this.mSeparatorAdmin.Size = new System.Drawing.Size(258, 6);
            this.mSeparatorAdmin.Visible = false;
            // 
            // mCompLargeIcons
            // 
            this.mCompLargeIcons.Name = "mCompLargeIcons";
            this.mCompLargeIcons.Size = new System.Drawing.Size(261, 22);
            this.mCompLargeIcons.Tag = "1";
            this.mCompLargeIcons.Text = "Крупные значки";
            this.mCompLargeIcons.Click += new System.EventHandler(this.mLargeIcons_Click);
            // 
            // mCompSmallIcons
            // 
            this.mCompSmallIcons.Name = "mCompSmallIcons";
            this.mCompSmallIcons.Size = new System.Drawing.Size(261, 22);
            this.mCompSmallIcons.Tag = "2";
            this.mCompSmallIcons.Text = "Мелкие значки";
            this.mCompSmallIcons.Click += new System.EventHandler(this.mLargeIcons_Click);
            // 
            // mCompList
            // 
            this.mCompList.Name = "mCompList";
            this.mCompList.Size = new System.Drawing.Size(261, 22);
            this.mCompList.Tag = "3";
            this.mCompList.Text = "Список";
            this.mCompList.Click += new System.EventHandler(this.mLargeIcons_Click);
            // 
            // mCompDetails
            // 
            this.mCompDetails.Checked = true;
            this.mCompDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mCompDetails.Name = "mCompDetails";
            this.mCompDetails.Size = new System.Drawing.Size(261, 22);
            this.mCompDetails.Tag = "4";
            this.mCompDetails.Text = "Таблица";
            this.mCompDetails.Click += new System.EventHandler(this.mLargeIcons_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(258, 6);
            // 
            // mCopyPath
            // 
            this.mCopyPath.Enabled = false;
            this.mCopyPath.Name = "mCopyPath";
            this.mCopyPath.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mCopyPath.Size = new System.Drawing.Size(261, 22);
            this.mCopyPath.Text = "Копировать «Общий ресурс»";
            this.mCopyPath.Visible = false;
            this.mCopyPath.Click += new System.EventHandler(this.mCopyPath_Click);
            // 
            // mCopyCompName
            // 
            this.mCopyCompName.Name = "mCopyCompName";
            this.mCopyCompName.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mCopyCompName.Size = new System.Drawing.Size(261, 22);
            this.mCopyCompName.Text = "Копировать «Сетевое имя»";
            this.mCopyCompName.Click += new System.EventHandler(this.mCopyCompName_Click);
            // 
            // mCopyComment
            // 
            this.mCopyComment.Name = "mCopyComment";
            this.mCopyComment.Size = new System.Drawing.Size(261, 22);
            this.mCopyComment.Text = "Копировать «Описание»";
            this.mCopyComment.Click += new System.EventHandler(this.mCopyComment_Click);
            // 
            // mCopySelected
            // 
            this.mCopySelected.Name = "mCopySelected";
            this.mCopySelected.Size = new System.Drawing.Size(261, 22);
            this.mCopySelected.Text = "Копировать выделенное";
            this.mCopySelected.Click += new System.EventHandler(this.mCopySelected_Click);
            // 
            // mSendSeparator
            // 
            this.mSendSeparator.Name = "mSendSeparator";
            this.mSendSeparator.Size = new System.Drawing.Size(258, 6);
            // 
            // mSendToTab
            // 
            this.mSendToTab.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSendToNewTab,
            this.mAfterSendTo});
            this.mSendToTab.Name = "mSendToTab";
            this.mSendToTab.Size = new System.Drawing.Size(261, 22);
            this.mSendToTab.Text = "Отправить в другую вкладку";
            // 
            // mSendToNewTab
            // 
            this.mSendToNewTab.Name = "mSendToNewTab";
            this.mSendToNewTab.Size = new System.Drawing.Size(162, 22);
            this.mSendToNewTab.Text = "В новую вкладку";
            // 
            // mAfterSendTo
            // 
            this.mAfterSendTo.Name = "mAfterSendTo";
            this.mAfterSendTo.Size = new System.Drawing.Size(159, 6);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(258, 6);
            // 
            // mContextClose
            // 
            this.mContextClose.Name = "mContextClose";
            this.mContextClose.Size = new System.Drawing.Size(261, 22);
            this.mContextClose.Text = "Закрыть";
            this.mContextClose.Click += new System.EventHandler(this.mContextClose_Click);
            // 
            // PanelView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LV);
            this.Controls.Add(this.tsBottom);
            this.Name = "PanelView";
            this.Size = new System.Drawing.Size(470, 487);
            this.tsBottom.ResumeLayout(false);
            this.tsBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgClear)).EndInit();
            this.popComps.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tsBottom;
        private System.Windows.Forms.PictureBox imgClear;
        public System.Windows.Forms.TextBox eFilter;
        private System.Windows.Forms.ListView LV;
        public System.Windows.Forms.ContextMenuStrip popComps;
        public System.Windows.Forms.ToolStripMenuItem mComp;
        private System.Windows.Forms.ToolStripMenuItem mCompOpen;
        private System.Windows.Forms.ToolStripMenuItem mWMIDescription;
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mCopyPath;
        private System.Windows.Forms.ToolStripMenuItem mCopyCompName;
        private System.Windows.Forms.ToolStripMenuItem mCopyComment;
        private System.Windows.Forms.ToolStripMenuItem mCopySelected;
        private System.Windows.Forms.ToolStripSeparator mSendSeparator;
        private System.Windows.Forms.ToolStripMenuItem mSendToTab;
        private System.Windows.Forms.ToolStripMenuItem mSendToNewTab;
        private System.Windows.Forms.ToolStripSeparator mAfterSendTo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mContextClose;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}
