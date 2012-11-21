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
            this.mRadmin6 = new System.Windows.Forms.ToolStripMenuItem();
            this.mRadmin7 = new System.Windows.Forms.ToolStripMenuItem();
            this.mRadmin8 = new System.Windows.Forms.ToolStripMenuItem();
            this.mFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.mFolderOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mFAROpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mSeparatorAdmin = new System.Windows.Forms.ToolStripSeparator();
            this.mLargeIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.mSmallIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.mList = new System.Windows.Forms.ToolStripMenuItem();
            this.mDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mCopyCompName = new System.Windows.Forms.ToolStripMenuItem();
            this.mCopyComment = new System.Windows.Forms.ToolStripMenuItem();
            this.mCopySelected = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.mSendToTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mSendToNewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mAfterSendTo = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mContextClose = new System.Windows.Forms.ToolStripMenuItem();
            this.ilLarge = new System.Windows.Forms.ImageList(this.components);
            this.ilSmall = new System.Windows.Forms.ImageList(this.components);
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.popTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mExit = new System.Windows.Forms.ToolStripMenuItem();
            this.DoBrowse = new System.ComponentModel.BackgroundWorker();
            this.BrowseTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lItemsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lCompName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.DoPing = new System.ComponentModel.BackgroundWorker();
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tipComps = new System.Windows.Forms.ToolTip(this.components);
            this.tsBottom = new System.Windows.Forms.Panel();
            this.imgClear = new System.Windows.Forms.PictureBox();
            this.eFilter = new System.Windows.Forms.TextBox();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.pInfo = new System.Windows.Forms.Panel();
            this.lInfoOS = new System.Windows.Forms.Label();
            this.imgInfo = new System.Windows.Forms.PictureBox();
            this.lInfoDesc = new System.Windows.Forms.Label();
            this.lInfoComp = new System.Windows.Forms.Label();
            this.lvComps = new LanExchange.CListViewEx();
            this.inputBox = new LanExchange.CInputBox(this.components);
            this.popComps.SuspendLayout();
            this.popTray.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.Pages.SuspendLayout();
            this.popPages.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tsBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgClear)).BeginInit();
            this.pInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // popComps
            // 
            this.popComps.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mComp,
            this.mFolder,
            this.mSeparatorAdmin,
            this.mLargeIcons,
            this.mSmallIcons,
            this.mList,
            this.mDetails,
            this.toolStripSeparator5,
            this.mCopyCompName,
            this.mCopyComment,
            this.mCopySelected,
            this.toolStripSeparator12,
            this.mSendToTab,
            this.toolStripSeparator6,
            this.mContextClose});
            this.popComps.Name = "popComps";
            this.popComps.Size = new System.Drawing.Size(266, 270);
            this.popComps.Opened += new System.EventHandler(this.popComps_Opened);
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
            this.mRadmin5,
            this.mRadmin6,
            this.mRadmin7,
            this.mRadmin8});
            this.mComp.Image = global::LanExchange.Properties.Resources.CompOff;
            this.mComp.Name = "mComp";
            this.mComp.Size = new System.Drawing.Size(265, 22);
            this.mComp.Tag = "computer";
            this.mComp.Text = "\\\\COMPUTER";
            this.mComp.Visible = false;
            // 
            // mCompOpen
            // 
            this.mCompOpen.Name = "mCompOpen";
            this.mCompOpen.ShortcutKeyDisplayString = "Shift+Enter";
            this.mCompOpen.Size = new System.Drawing.Size(323, 22);
            this.mCompOpen.Tag = "\\\\{0}";
            this.mCompOpen.Text = "Открыть в Проводнике";
            this.mCompOpen.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mWMIDescription
            // 
            this.mWMIDescription.Name = "mWMIDescription";
            this.mWMIDescription.Size = new System.Drawing.Size(323, 22);
            this.mWMIDescription.Text = "Редактировать описание";
            this.mWMIDescription.Click += new System.EventHandler(this.mWMIDescription_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(320, 6);
            // 
            // mCompService
            // 
            this.mCompService.Image = global::LanExchange.Properties.Resources.Service;
            this.mCompService.Name = "mCompService";
            this.mCompService.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.mCompService.Size = new System.Drawing.Size(323, 22);
            this.mCompService.Tag = "%systemroot%\\system32\\mmc.exe %systemroot%\\system32\\compmgmt.msc /computer:{0}";
            this.mCompService.Text = "Управление компьютером";
            this.mCompService.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mCompMSTSC
            // 
            this.mCompMSTSC.Image = global::LanExchange.Properties.Resources.MSTSC;
            this.mCompMSTSC.Name = "mCompMSTSC";
            this.mCompMSTSC.Size = new System.Drawing.Size(323, 22);
            this.mCompMSTSC.Tag = "%systemroot%\\system32\\mstsc.exe /v:{0}";
            this.mCompMSTSC.Text = "Подключение к удаленному рабочему столу";
            this.mCompMSTSC.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(320, 6);
            // 
            // mRadmin1
            // 
            this.mRadmin1.Image = ((System.Drawing.Image)(resources.GetObject("mRadmin1.Image")));
            this.mRadmin1.Name = "mRadmin1";
            this.mRadmin1.ShortcutKeyDisplayString = "Ctrl+Enter";
            this.mRadmin1.Size = new System.Drawing.Size(323, 22);
            this.mRadmin1.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0}";
            this.mRadmin1.Text = "Управление";
            this.mRadmin1.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mRadmin2
            // 
            this.mRadmin2.Image = ((System.Drawing.Image)(resources.GetObject("mRadmin2.Image")));
            this.mRadmin2.Name = "mRadmin2";
            this.mRadmin2.Size = new System.Drawing.Size(323, 22);
            this.mRadmin2.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /noinput";
            this.mRadmin2.Text = "Просмотр";
            this.mRadmin2.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mRadmin3
            // 
            this.mRadmin3.Image = ((System.Drawing.Image)(resources.GetObject("mRadmin3.Image")));
            this.mRadmin3.Name = "mRadmin3";
            this.mRadmin3.Size = new System.Drawing.Size(323, 22);
            this.mRadmin3.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /telnet";
            this.mRadmin3.Text = "Телнет";
            this.mRadmin3.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mRadmin4
            // 
            this.mRadmin4.Image = ((System.Drawing.Image)(resources.GetObject("mRadmin4.Image")));
            this.mRadmin4.Name = "mRadmin4";
            this.mRadmin4.Size = new System.Drawing.Size(323, 22);
            this.mRadmin4.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /file";
            this.mRadmin4.Text = "Передача файлов";
            this.mRadmin4.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mRadmin5
            // 
            this.mRadmin5.Image = ((System.Drawing.Image)(resources.GetObject("mRadmin5.Image")));
            this.mRadmin5.Name = "mRadmin5";
            this.mRadmin5.Size = new System.Drawing.Size(323, 22);
            this.mRadmin5.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /shutdown";
            this.mRadmin5.Text = "Выключение";
            this.mRadmin5.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mRadmin6
            // 
            this.mRadmin6.Image = ((System.Drawing.Image)(resources.GetObject("mRadmin6.Image")));
            this.mRadmin6.Name = "mRadmin6";
            this.mRadmin6.Size = new System.Drawing.Size(323, 22);
            this.mRadmin6.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /chat";
            this.mRadmin6.Text = "Текстовый чат";
            this.mRadmin6.Visible = false;
            this.mRadmin6.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mRadmin7
            // 
            this.mRadmin7.Image = ((System.Drawing.Image)(resources.GetObject("mRadmin7.Image")));
            this.mRadmin7.Name = "mRadmin7";
            this.mRadmin7.Size = new System.Drawing.Size(323, 22);
            this.mRadmin7.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /voice";
            this.mRadmin7.Text = "Голосовой чат";
            this.mRadmin7.Visible = false;
            this.mRadmin7.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mRadmin8
            // 
            this.mRadmin8.Image = ((System.Drawing.Image)(resources.GetObject("mRadmin8.Image")));
            this.mRadmin8.Name = "mRadmin8";
            this.mRadmin8.Size = new System.Drawing.Size(323, 22);
            this.mRadmin8.Tag = "\"%ProgramFiles(x86)%\\Radmin Viewer 3\\Radmin.exe\" /connect:{0} /message";
            this.mRadmin8.Text = "Текстовое сообщение";
            this.mRadmin8.Visible = false;
            this.mRadmin8.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mFolder
            // 
            this.mFolder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFolderOpen,
            this.toolStripSeparator8,
            this.mFAROpen});
            this.mFolder.Image = global::LanExchange.Properties.Resources.folder2_normal_16x16;
            this.mFolder.Name = "mFolder";
            this.mFolder.Size = new System.Drawing.Size(265, 22);
            this.mFolder.Tag = "folder";
            this.mFolder.Text = "\\\\COMPUTER\\FOLDER";
            this.mFolder.Visible = false;
            // 
            // mFolderOpen
            // 
            this.mFolderOpen.Name = "mFolderOpen";
            this.mFolderOpen.ShortcutKeyDisplayString = "Shift+Enter";
            this.mFolderOpen.Size = new System.Drawing.Size(267, 22);
            this.mFolderOpen.Tag = "{0}";
            this.mFolderOpen.Text = "Открыть в Проводнике";
            this.mFolderOpen.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(264, 6);
            // 
            // mFAROpen
            // 
            this.mFAROpen.Image = global::LanExchange.Properties.Resources.FAR_16x16;
            this.mFAROpen.Name = "mFAROpen";
            this.mFAROpen.ShortcutKeyDisplayString = "Ctrl+Enter";
            this.mFAROpen.Size = new System.Drawing.Size(267, 22);
            this.mFAROpen.Tag = "FAR.EXE {0}";
            this.mFAROpen.Text = "Менеджер FAR";
            this.mFAROpen.Click += new System.EventHandler(this.mCompOpen_Click);
            // 
            // mSeparatorAdmin
            // 
            this.mSeparatorAdmin.Name = "mSeparatorAdmin";
            this.mSeparatorAdmin.Size = new System.Drawing.Size(262, 6);
            this.mSeparatorAdmin.Visible = false;
            // 
            // mLargeIcons
            // 
            this.mLargeIcons.Name = "mLargeIcons";
            this.mLargeIcons.Size = new System.Drawing.Size(265, 22);
            this.mLargeIcons.Tag = "1";
            this.mLargeIcons.Text = "Крупные значки";
            this.mLargeIcons.Click += new System.EventHandler(this.mLargeIcons_Click);
            // 
            // mSmallIcons
            // 
            this.mSmallIcons.Name = "mSmallIcons";
            this.mSmallIcons.Size = new System.Drawing.Size(265, 22);
            this.mSmallIcons.Tag = "3";
            this.mSmallIcons.Text = "Мелкие значки";
            this.mSmallIcons.Click += new System.EventHandler(this.mLargeIcons_Click);
            // 
            // mList
            // 
            this.mList.Name = "mList";
            this.mList.Size = new System.Drawing.Size(265, 22);
            this.mList.Tag = "4";
            this.mList.Text = "Список";
            this.mList.Click += new System.EventHandler(this.mLargeIcons_Click);
            // 
            // mDetails
            // 
            this.mDetails.Checked = true;
            this.mDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mDetails.Name = "mDetails";
            this.mDetails.Size = new System.Drawing.Size(265, 22);
            this.mDetails.Tag = "2";
            this.mDetails.Text = "Таблица";
            this.mDetails.Click += new System.EventHandler(this.mLargeIcons_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(262, 6);
            // 
            // mCopyCompName
            // 
            this.mCopyCompName.Name = "mCopyCompName";
            this.mCopyCompName.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mCopyCompName.Size = new System.Drawing.Size(265, 22);
            this.mCopyCompName.Text = "Копировать «Сетевое имя»";
            this.mCopyCompName.Click += new System.EventHandler(this.mCopyCompName_Click);
            // 
            // mCopyComment
            // 
            this.mCopyComment.Name = "mCopyComment";
            this.mCopyComment.Size = new System.Drawing.Size(265, 22);
            this.mCopyComment.Text = "Копировать «Описание»";
            this.mCopyComment.Click += new System.EventHandler(this.mCopyComment_Click);
            // 
            // mCopySelected
            // 
            this.mCopySelected.Name = "mCopySelected";
            this.mCopySelected.Size = new System.Drawing.Size(265, 22);
            this.mCopySelected.Text = "Копировать выделенное";
            this.mCopySelected.Click += new System.EventHandler(this.mCopySelected_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(262, 6);
            // 
            // mSendToTab
            // 
            this.mSendToTab.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSendToNewTab,
            this.mAfterSendTo});
            this.mSendToTab.Name = "mSendToTab";
            this.mSendToTab.Size = new System.Drawing.Size(265, 22);
            this.mSendToTab.Text = "Отправить в другую вкладку";
            this.mSendToTab.DropDownOpened += new System.EventHandler(this.mSendToTab_DropDownOpened);
            // 
            // mSendToNewTab
            // 
            this.mSendToNewTab.Name = "mSendToNewTab";
            this.mSendToNewTab.Size = new System.Drawing.Size(166, 22);
            this.mSendToNewTab.Text = "В новую вкладку";
            // 
            // mAfterSendTo
            // 
            this.mAfterSendTo.Name = "mAfterSendTo";
            this.mAfterSendTo.Size = new System.Drawing.Size(163, 6);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(262, 6);
            // 
            // mContextClose
            // 
            this.mContextClose.Name = "mContextClose";
            this.mContextClose.Size = new System.Drawing.Size(265, 22);
            this.mContextClose.Text = "Закрыть";
            this.mContextClose.Click += new System.EventHandler(this.mContextClose_Click);
            // 
            // ilLarge
            // 
            this.ilLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilLarge.ImageStream")));
            this.ilLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.ilLarge.Images.SetKeyName(0, "CompOffBig.png");
            this.ilLarge.Images.SetKeyName(1, "Comp_Big_Blue.png");
            this.ilLarge.Images.SetKeyName(2, "Comp_Big_DarkMagenta.png");
            this.ilLarge.Images.SetKeyName(3, "Comp_Big_Gray.png");
            this.ilLarge.Images.SetKeyName(4, "Comp_Big_Green.png");
            this.ilLarge.Images.SetKeyName(5, "Comp_Big_Red.png");
            this.ilLarge.Images.SetKeyName(6, "folder2_hidden_32x32.png");
            this.ilLarge.Images.SetKeyName(7, "folder2_normal_32x32.png");
            this.ilLarge.Images.SetKeyName(8, "printer_32x32.png");
            this.ilLarge.Images.SetKeyName(9, "back_32x32.png");
            // 
            // ilSmall
            // 
            this.ilSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSmall.ImageStream")));
            this.ilSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.ilSmall.Images.SetKeyName(0, "CompOff.png");
            this.ilSmall.Images.SetKeyName(1, "Comp_Small_Blue.png");
            this.ilSmall.Images.SetKeyName(2, "Comp_Small_DarkMagenta.png");
            this.ilSmall.Images.SetKeyName(3, "Comp_Small_Gray.png");
            this.ilSmall.Images.SetKeyName(4, "Comp_Small_Green.png");
            this.ilSmall.Images.SetKeyName(5, "Comp_Small_Red.png");
            this.ilSmall.Images.SetKeyName(6, "folder2_hidden_16x16.png");
            this.ilSmall.Images.SetKeyName(7, "folder2_normal_16x16.png");
            this.ilSmall.Images.SetKeyName(8, "printer_16x16.png");
            this.ilSmall.Images.SetKeyName(9, "back_16x16.png");
            // 
            // TrayIcon
            // 
            this.TrayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.TrayIcon.BalloonTipTitle = "Оповещение";
            this.TrayIcon.ContextMenuStrip = this.popTray;
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "Общие папки";
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
            this.popTray.Size = new System.Drawing.Size(213, 104);
            this.popTray.Opening += new System.ComponentModel.CancelEventHandler(this.popTray_Opening);
            // 
            // mOpen
            // 
            this.mOpen.Name = "mOpen";
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
            // DoBrowse
            // 
            this.DoBrowse.WorkerSupportsCancellation = true;
            this.DoBrowse.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DoBrowse_DoWork);
            this.DoBrowse.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DoBrowse_RunWorkerCompleted);
            // 
            // BrowseTimer
            // 
            this.BrowseTimer.Interval = 300000;
            this.BrowseTimer.Tick += new System.EventHandler(this.BrowseTimer_Tick);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
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
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lItemsCount
            // 
            this.lItemsCount.Name = "lItemsCount";
            this.lItemsCount.Size = new System.Drawing.Size(471, 17);
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
            this.lCompName.Image = global::LanExchange.Properties.Resources.CompOff;
            this.lCompName.Name = "lCompName";
            this.lCompName.Size = new System.Drawing.Size(35, 17);
            this.lCompName.Text = "    ";
            this.lCompName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lCompName.Click += new System.EventHandler(this.lCompName_Click);
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
            this.lUserName.Image = global::LanExchange.Properties.Resources.UserName;
            this.lUserName.Name = "lUserName";
            this.lUserName.Size = new System.Drawing.Size(35, 17);
            this.lUserName.Text = "    ";
            this.lUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DoPing
            // 
            this.DoPing.WorkerSupportsCancellation = true;
            this.DoPing.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DoPing_DoWork);
            // 
            // Pages
            // 
            this.Pages.ContextMenuStrip = this.popPages;
            this.Pages.Controls.Add(this.tabPage1);
            this.Pages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pages.Location = new System.Drawing.Point(0, 60);
            this.Pages.Name = "Pages";
            this.Pages.SelectedIndex = 0;
            this.Pages.Size = new System.Drawing.Size(564, 428);
            this.Pages.TabIndex = 19;
            this.Pages.Selected += new System.Windows.Forms.TabControlEventHandler(this.Pages_Selected);
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
            this.popPages.Size = new System.Drawing.Size(245, 154);
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
            // mSaveTab
            // 
            this.mSaveTab.Name = "mSaveTab";
            this.mSaveTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mSaveTab.Size = new System.Drawing.Size(244, 22);
            this.mSaveTab.Text = "Сохранить вкладку";
            this.mSaveTab.Click += new System.EventHandler(this.mSaveTab_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(241, 6);
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
            this.toolStripSeparator11.Visible = false;
            // 
            // mListTab
            // 
            this.mListTab.Name = "mListTab";
            this.mListTab.Size = new System.Drawing.Size(244, 22);
            this.mListTab.Text = "Список вкладок";
            this.mListTab.Visible = false;
            this.mListTab.Click += new System.EventHandler(this.mListTab_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lvComps);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(556, 402);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tipComps
            // 
            this.tipComps.Active = false;
            this.tipComps.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.tipComps.ToolTipTitle = "Test";
            this.tipComps.Popup += new System.Windows.Forms.PopupEventHandler(this.tipComps_Popup);
            // 
            // tsBottom
            // 
            this.tsBottom.Controls.Add(this.imgClear);
            this.tsBottom.Controls.Add(this.eFilter);
            this.tsBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsBottom.Location = new System.Drawing.Point(0, 488);
            this.tsBottom.Name = "tsBottom";
            this.tsBottom.Size = new System.Drawing.Size(564, 32);
            this.tsBottom.TabIndex = 21;
            this.tsBottom.Visible = false;
            // 
            // imgClear
            // 
            this.imgClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgClear.BackColor = System.Drawing.Color.Transparent;
            this.imgClear.Image = global::LanExchange.Properties.Resources.clear_normal;
            this.imgClear.Location = new System.Drawing.Point(542, 8);
            this.imgClear.Name = "imgClear";
            this.imgClear.Size = new System.Drawing.Size(16, 16);
            this.imgClear.TabIndex = 6;
            this.imgClear.TabStop = false;
            this.tipComps.SetToolTip(this.imgClear, "Очистить фильтр");
            this.imgClear.Click += new System.EventHandler(this.pictureBox1_Click);
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
            this.eFilter.Size = new System.Drawing.Size(530, 20);
            this.eFilter.TabIndex = 4;
            this.tipComps.SetToolTip(this.eFilter, "Фильтрация по сетевым именам и описанию");
            this.eFilter.TextChanged += new System.EventHandler(this.eFilter_TextChanged);
            this.eFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.eFilter_KeyDown);
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "txt";
            this.dlgSave.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            this.dlgSave.RestoreDirectory = true;
            // 
            // pInfo
            // 
            this.pInfo.BackColor = System.Drawing.Color.White;
            this.pInfo.Controls.Add(this.lInfoOS);
            this.pInfo.Controls.Add(this.imgInfo);
            this.pInfo.Controls.Add(this.lInfoDesc);
            this.pInfo.Controls.Add(this.lInfoComp);
            this.pInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pInfo.Location = new System.Drawing.Point(0, 0);
            this.pInfo.Name = "pInfo";
            this.pInfo.Size = new System.Drawing.Size(564, 60);
            this.pInfo.TabIndex = 22;
            // 
            // lInfoOS
            // 
            this.lInfoOS.AutoSize = true;
            this.lInfoOS.Location = new System.Drawing.Point(30, 42);
            this.lInfoOS.Name = "lInfoOS";
            this.lInfoOS.Size = new System.Drawing.Size(19, 13);
            this.lInfoOS.TabIndex = 3;
            this.lInfoOS.Text = "    ";
            // 
            // imgInfo
            // 
            this.imgInfo.Location = new System.Drawing.Point(8, 8);
            this.imgInfo.Name = "imgInfo";
            this.imgInfo.Size = new System.Drawing.Size(16, 16);
            this.imgInfo.TabIndex = 2;
            this.imgInfo.TabStop = false;
            // 
            // lInfoDesc
            // 
            this.lInfoDesc.AutoSize = true;
            this.lInfoDesc.Location = new System.Drawing.Point(30, 25);
            this.lInfoDesc.Name = "lInfoDesc";
            this.lInfoDesc.Size = new System.Drawing.Size(19, 13);
            this.lInfoDesc.TabIndex = 1;
            this.lInfoDesc.Text = "    ";
            // 
            // lInfoComp
            // 
            this.lInfoComp.AutoSize = true;
            this.lInfoComp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lInfoComp.Location = new System.Drawing.Point(30, 8);
            this.lInfoComp.Name = "lInfoComp";
            this.lInfoComp.Size = new System.Drawing.Size(23, 13);
            this.lInfoComp.TabIndex = 0;
            this.lInfoComp.Text = "    ";
            // 
            // lvComps
            // 
            this.lvComps.ContextMenuStrip = this.popComps;
            this.lvComps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvComps.FullRowSelect = true;
            this.lvComps.GridLines = true;
            this.lvComps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvComps.HideSelection = false;
            this.lvComps.LargeImageList = this.ilLarge;
            this.lvComps.Location = new System.Drawing.Point(3, 3);
            this.lvComps.Name = "lvComps";
            this.lvComps.ShowGroups = false;
            this.lvComps.ShowItemToolTips = true;
            this.lvComps.Size = new System.Drawing.Size(550, 396);
            this.lvComps.SmallImageList = this.ilSmall;
            this.lvComps.TabIndex = 19;
            this.lvComps.UseCompatibleStateImageBehavior = false;
            this.lvComps.View = System.Windows.Forms.View.Details;
            this.lvComps.VirtualMode = true;
            this.lvComps.ItemActivate += new System.EventHandler(this.lvComps_ItemActivate);
            this.lvComps.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvComps_ItemSelectionChanged);
            this.lvComps.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvComps_RetrieveVirtualItem);
            this.lvComps.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvComps_KeyDown);
            this.lvComps.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvComps_KeyPress);
            // 
            // inputBox
            // 
            this.inputBox.ErrorMsgOnEmpty = null;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 542);
            this.Controls.Add(this.Pages);
            this.Controls.Add(this.tsBottom);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.popComps.ResumeLayout(false);
            this.popTray.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.Pages.ResumeLayout(false);
            this.popPages.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tsBottom.ResumeLayout(false);
            this.tsBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgClear)).EndInit();
            this.pInfo.ResumeLayout(false);
            this.pInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ContextMenuStrip popTray;
        private System.Windows.Forms.ToolStripMenuItem mExit;
        private System.Windows.Forms.ToolStripMenuItem mOpen;
        private System.ComponentModel.BackgroundWorker DoBrowse;
        private System.Windows.Forms.Timer BrowseTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ContextMenuStrip popComps;
        private System.Windows.Forms.ToolStripMenuItem mCopyCompName;
        private System.Windows.Forms.ToolStripMenuItem mCopyComment;
        private System.Windows.Forms.ToolStripMenuItem mCopySelected;
        private System.Windows.Forms.ToolStripMenuItem mCompOpen;
        private System.Windows.Forms.ToolStripSeparator mSeparatorAdmin;
        private System.Windows.Forms.ToolStripMenuItem mContextClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mLargeIcons;
        private System.Windows.Forms.ToolStripMenuItem mDetails;
        private System.Windows.Forms.ToolStripMenuItem mSmallIcons;
        private System.Windows.Forms.ToolStripMenuItem mList;
        public System.Windows.Forms.ImageList ilLarge;
        public System.Windows.Forms.ImageList ilSmall;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mComp;
        private System.Windows.Forms.ToolStripMenuItem mCompService;
        private System.Windows.Forms.ToolStripMenuItem mCompMSTSC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mRadmin1;
        private System.Windows.Forms.ToolStripMenuItem mRadmin2;
        private System.Windows.Forms.ToolStripMenuItem mRadmin3;
        private System.Windows.Forms.ToolStripMenuItem mRadmin4;
        private System.Windows.Forms.ToolStripMenuItem mRadmin5;
        private System.Windows.Forms.ToolStripMenuItem mRadmin6;
        private System.Windows.Forms.ToolStripMenuItem mRadmin7;
        private System.Windows.Forms.ToolStripMenuItem mRadmin8;
        private System.Windows.Forms.ToolStripMenuItem mWMIDescription;
        public CInputBox inputBox;
        private System.Windows.Forms.ToolStripMenuItem mSettings;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel lItemsCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lCompName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lUserName;
        private System.ComponentModel.BackgroundWorker DoPing;
        private System.Windows.Forms.TabControl Pages;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.ToolTip tipComps;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mAbout;
        private System.Windows.Forms.ToolStripMenuItem mFolder;
        public System.Windows.Forms.ToolStripMenuItem mFolderOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem mFAROpen;
        private System.Windows.Forms.Panel tsBottom;
        public System.Windows.Forms.TextBox eFilter;
        private System.Windows.Forms.ContextMenuStrip popPages;
        private System.Windows.Forms.ToolStripMenuItem mNewTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem mCloseTab;
        private System.Windows.Forms.ToolStripMenuItem mRenameTab;
        private System.Windows.Forms.ToolStripMenuItem mSaveTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem mListTab;
        private System.Windows.Forms.ToolStripMenuItem mSelectTab;
        public System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem mSendToTab;
        private System.Windows.Forms.ToolStripMenuItem mSendToNewTab;
        private System.Windows.Forms.ToolStripSeparator mAfterSendTo;
        public CListViewEx lvComps;
        private System.Windows.Forms.Panel pInfo;
        private System.Windows.Forms.Label lInfoComp;
        private System.Windows.Forms.PictureBox imgInfo;
        private System.Windows.Forms.Label lInfoDesc;
        private System.Windows.Forms.Label lInfoOS;
        private System.Windows.Forms.PictureBox imgClear;
    }
}

