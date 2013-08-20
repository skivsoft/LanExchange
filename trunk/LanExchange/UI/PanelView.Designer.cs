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
            this.mCopyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mCopySelected = new System.Windows.Forms.ToolStripMenuItem();
            this.mSeparatorSend = new System.Windows.Forms.ToolStripSeparator();
            this.mSendToNewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mContextClose = new System.Windows.Forms.ToolStripMenuItem();
            this.ePath = new System.Windows.Forms.TextBox();
            this.LV = new LanExchange.UI.ListViewer();
            this.pFilter = new LanExchange.UI.FilterView();
            this.popCompsOld = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripMenuItem();
            this.popComps.SuspendLayout();
            this.popCompsOld.SuspendLayout();
            this.SuspendLayout();
            // 
            // popComps
            // 
            this.popComps.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mComp,
            this.mFolder,
            this.mSeparatorAdmin,
            this.mCopyMenu,
            this.mSeparatorSend,
            this.mSendToNewTab,
            this.toolStripSeparator6,
            this.mContextClose});
            this.popComps.Name = "popComps";
            this.popComps.Size = new System.Drawing.Size(195, 154);
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
            this.mComp.Size = new System.Drawing.Size(194, 22);
            this.mComp.Tag = "";
            this.mComp.Text = "\\\\COMPUTER";
            this.mComp.Visible = false;
            // 
            // mCompOpen
            // 
            this.mCompOpen.Name = "mCompOpen";
            this.mCompOpen.ShortcutKeyDisplayString = "Shift+Enter";
            this.mCompOpen.Size = new System.Drawing.Size(328, 22);
            this.mCompOpen.Tag = "\\\\{0}";
            this.mCompOpen.Text = "Открыть в проводнике";
            this.mCompOpen.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mWMI
            // 
            this.mWMI.Name = "mWMI";
            this.mWMI.ShortcutKeyDisplayString = "";
            this.mWMI.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F1)));
            this.mWMI.Size = new System.Drawing.Size(328, 22);
            this.mWMI.Text = "Инструментарий управления Windows®";
            this.mWMI.Click += new System.EventHandler(this.mWMI_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(325, 6);
            // 
            // mCompService
            // 
            this.mCompService.Name = "mCompService";
            this.mCompService.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.mCompService.Size = new System.Drawing.Size(328, 22);
            this.mCompService.Tag = "%systemroot%\\system32\\mmc.exe %systemroot%\\system32\\compmgmt.msc /computer:{0}";
            this.mCompService.Text = "Управление компьютером";
            this.mCompService.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mCompMSTSC
            // 
            this.mCompMSTSC.Name = "mCompMSTSC";
            this.mCompMSTSC.Size = new System.Drawing.Size(328, 22);
            this.mCompMSTSC.Tag = "%systemroot%\\system32\\mstsc.exe /v:{0}";
            this.mCompMSTSC.Text = "Подключение к удаленному рабочему столу";
            this.mCompMSTSC.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(325, 6);
            // 
            // mRadmin1
            // 
            this.mRadmin1.Name = "mRadmin1";
            this.mRadmin1.ShortcutKeyDisplayString = "Ctrl+Enter";
            this.mRadmin1.Size = new System.Drawing.Size(328, 22);
            this.mRadmin1.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0}";
            this.mRadmin1.Text = "Radmin® Управление";
            this.mRadmin1.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mRadmin2
            // 
            this.mRadmin2.Name = "mRadmin2";
            this.mRadmin2.Size = new System.Drawing.Size(328, 22);
            this.mRadmin2.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /noinput";
            this.mRadmin2.Text = "Radmin® Просмотр";
            this.mRadmin2.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mRadmin3
            // 
            this.mRadmin3.Name = "mRadmin3";
            this.mRadmin3.Size = new System.Drawing.Size(328, 22);
            this.mRadmin3.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /telnet";
            this.mRadmin3.Text = "Radmin® Телнет";
            this.mRadmin3.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mRadmin4
            // 
            this.mRadmin4.Name = "mRadmin4";
            this.mRadmin4.Size = new System.Drawing.Size(328, 22);
            this.mRadmin4.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /file";
            this.mRadmin4.Text = "Radmin® Передача файлов";
            this.mRadmin4.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mRadmin5
            // 
            this.mRadmin5.Name = "mRadmin5";
            this.mRadmin5.Size = new System.Drawing.Size(328, 22);
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
            this.mFolder.Size = new System.Drawing.Size(194, 22);
            this.mFolder.Tag = "";
            this.mFolder.Text = "\\\\COMPUTER\\FOLDER";
            this.mFolder.Visible = false;
            // 
            // mFolderOpen
            // 
            this.mFolderOpen.Name = "mFolderOpen";
            this.mFolderOpen.ShortcutKeyDisplayString = "Shift+Enter";
            this.mFolderOpen.Size = new System.Drawing.Size(267, 22);
            this.mFolderOpen.Tag = "{0}";
            this.mFolderOpen.Text = "Открыть в проводнике";
            this.mFolderOpen.Click += new System.EventHandler(this.mFolderOpen_Click);
            // 
            // mFAROpen
            // 
            this.mFAROpen.Name = "mFAROpen";
            this.mFAROpen.ShortcutKeyDisplayString = "Ctrl+Enter";
            this.mFAROpen.Size = new System.Drawing.Size(267, 22);
            this.mFAROpen.Tag = "FAR.EXE {0}";
            this.mFAROpen.Text = "Открыть в Far Manager";
            this.mFAROpen.Click += new System.EventHandler(this.mFolderOpen_Click);
            // 
            // mSeparatorAdmin
            // 
            this.mSeparatorAdmin.Name = "mSeparatorAdmin";
            this.mSeparatorAdmin.Size = new System.Drawing.Size(191, 6);
            this.mSeparatorAdmin.Visible = false;
            // 
            // mCopyMenu
            // 
            this.mCopyMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mCopySelected});
            this.mCopyMenu.Name = "mCopyMenu";
            this.mCopyMenu.Size = new System.Drawing.Size(194, 22);
            this.mCopyMenu.Text = "Copy";
            this.mCopyMenu.DropDownOpening += new System.EventHandler(this.mCopyMenu_DropDownOpening);
            // 
            // mCopySelected
            // 
            this.mCopySelected.Name = "mCopySelected";
            this.mCopySelected.ShortcutKeyDisplayString = "Ctrl+C";
            this.mCopySelected.Size = new System.Drawing.Size(220, 22);
            this.mCopySelected.Text = "Copy selected items";
            this.mCopySelected.Click += new System.EventHandler(this.CopySelectedOnClick);
            // 
            // mSeparatorSend
            // 
            this.mSeparatorSend.Name = "mSeparatorSend";
            this.mSeparatorSend.Size = new System.Drawing.Size(191, 6);
            this.mSeparatorSend.Visible = false;
            // 
            // mSendToNewTab
            // 
            this.mSendToNewTab.Name = "mSendToNewTab";
            this.mSendToNewTab.Size = new System.Drawing.Size(194, 22);
            this.mSendToNewTab.Text = "Send to another tab...";
            this.mSendToNewTab.Visible = false;
            this.mSendToNewTab.Click += new System.EventHandler(this.mSendToNewTab_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(191, 6);
            // 
            // mContextClose
            // 
            this.mContextClose.Name = "mContextClose";
            this.mContextClose.Size = new System.Drawing.Size(194, 22);
            this.mContextClose.Text = "Close";
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
            this.ePath.Visible = false;
            this.ePath.DoubleClick += new System.EventHandler(this.ePath_DoubleClick);
            this.ePath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ePath_KeyDown);
            this.ePath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvComps_KeyPress);
            // 
            // LV
            // 
            this.LV.BackColor = System.Drawing.SystemColors.Window;
            this.LV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LV.ContextMenuStrip = this.popComps;
            this.LV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LV.ForeColor = System.Drawing.SystemColors.WindowText;
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
            this.LV.ColumnRightClick += new System.EventHandler<System.Windows.Forms.ColumnClickEventArgs>(this.LV_ColumnRightClick);
            this.LV.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LV_ColumnClick);
            this.LV.ColumnReordered += new System.Windows.Forms.ColumnReorderedEventHandler(this.LV_ColumnReordered);
            this.LV.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.LV_ColumnWidthChanged);
            this.LV.ItemActivate += new System.EventHandler(this.lvComps_ItemActivate);
            this.LV.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvComps_ItemSelectionChanged);
            this.LV.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvComps_KeyDown);
            this.LV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvComps_KeyPress);
            this.LV.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LV_MouseDown);
            this.LV.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LV_MouseMove);
            this.LV.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LV_MouseUp);
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
            // popCompsOld
            // 
            this.popCompsOld.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem11,
            this.toolStripSeparator4,
            this.toolStripMenuItem14,
            this.toolStripSeparator5,
            this.toolStripMenuItem16,
            this.toolStripSeparator8,
            this.toolStripMenuItem17});
            this.popCompsOld.Name = "popComps";
            this.popCompsOld.Size = new System.Drawing.Size(195, 132);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripSeparator2,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripSeparator3,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10});
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(194, 22);
            this.toolStripMenuItem1.Tag = "";
            this.toolStripMenuItem1.Text = "\\\\COMPUTER";
            this.toolStripMenuItem1.Visible = false;
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.ShortcutKeyDisplayString = "Shift+Enter";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(328, 22);
            this.toolStripMenuItem2.Tag = "\\\\{0}";
            this.toolStripMenuItem2.Text = "Открыть в проводнике";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.ShortcutKeyDisplayString = "";
            this.toolStripMenuItem3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F1)));
            this.toolStripMenuItem3.Size = new System.Drawing.Size(328, 22);
            this.toolStripMenuItem3.Text = "Инструментарий управления Windows®";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(325, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.toolStripMenuItem4.Size = new System.Drawing.Size(328, 22);
            this.toolStripMenuItem4.Tag = "%systemroot%\\system32\\mmc.exe %systemroot%\\system32\\compmgmt.msc /computer:{0}";
            this.toolStripMenuItem4.Text = "Управление компьютером";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(328, 22);
            this.toolStripMenuItem5.Tag = "%systemroot%\\system32\\mstsc.exe /v:{0}";
            this.toolStripMenuItem5.Text = "Подключение к удаленному рабочему столу";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(325, 6);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.ShortcutKeyDisplayString = "Ctrl+Enter";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(328, 22);
            this.toolStripMenuItem6.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0}";
            this.toolStripMenuItem6.Text = "Radmin® Управление";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(328, 22);
            this.toolStripMenuItem7.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /noinput";
            this.toolStripMenuItem7.Text = "Radmin® Просмотр";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(328, 22);
            this.toolStripMenuItem8.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /telnet";
            this.toolStripMenuItem8.Text = "Radmin® Телнет";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(328, 22);
            this.toolStripMenuItem9.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /file";
            this.toolStripMenuItem9.Text = "Radmin® Передача файлов";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(328, 22);
            this.toolStripMenuItem10.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /shutdown";
            this.toolStripMenuItem10.Text = "Radmin® Выключение";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem12,
            this.toolStripMenuItem13});
            this.toolStripMenuItem11.Enabled = false;
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(194, 22);
            this.toolStripMenuItem11.Tag = "";
            this.toolStripMenuItem11.Text = "\\\\COMPUTER\\FOLDER";
            this.toolStripMenuItem11.Visible = false;
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.ShortcutKeyDisplayString = "Shift+Enter";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(267, 22);
            this.toolStripMenuItem12.Tag = "{0}";
            this.toolStripMenuItem12.Text = "Открыть в проводнике";
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.ShortcutKeyDisplayString = "Ctrl+Enter";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(267, 22);
            this.toolStripMenuItem13.Tag = "FAR.EXE {0}";
            this.toolStripMenuItem13.Text = "Открыть в Far Manager";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(191, 6);
            this.toolStripSeparator4.Visible = false;
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem15});
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(194, 22);
            this.toolStripMenuItem14.Text = "Copy";
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.ShortcutKeyDisplayString = "Ctrl+C";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(220, 22);
            this.toolStripMenuItem15.Text = "Copy selected items";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(191, 6);
            this.toolStripSeparator5.Visible = false;
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(194, 22);
            this.toolStripMenuItem16.Text = "Send to another tab...";
            this.toolStripMenuItem16.Visible = false;
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(191, 6);
            // 
            // toolStripMenuItem17
            // 
            this.toolStripMenuItem17.Name = "toolStripMenuItem17";
            this.toolStripMenuItem17.Size = new System.Drawing.Size(194, 22);
            this.toolStripMenuItem17.Text = "Close";
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
            this.popCompsOld.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LanExchange.UI.ListViewer LV;
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
        private System.Windows.Forms.ToolStripSeparator mSeparatorSend;
        private System.Windows.Forms.ToolStripMenuItem mSendToNewTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mContextClose;
        private System.Windows.Forms.ToolStripMenuItem mWMI;
        private FilterView pFilter;
        private System.Windows.Forms.TextBox ePath;
        private System.Windows.Forms.ToolStripMenuItem mCopyMenu;
        private System.Windows.Forms.ToolStripMenuItem mCopySelected;
        public System.Windows.Forms.ContextMenuStrip popCompsOld;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem16;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem17;
    }
}
