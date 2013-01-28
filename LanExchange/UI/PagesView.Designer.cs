namespace LanExchange.UI
{
    partial class PagesView
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
            this.popPages = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mNewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.mCloseTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mRenameTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mSelectTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.mTabParams = new System.Windows.Forms.ToolStripMenuItem();
            this.Pages = new System.Windows.Forms.TabControl();
            this.popPages.SuspendLayout();
            this.SuspendLayout();
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
            this.popPages.Size = new System.Drawing.Size(245, 148);
            this.popPages.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.popPages_Closed);
            this.popPages.Opening += new System.ComponentModel.CancelEventHandler(this.popPages_Opening);
            this.popPages.Opened += new System.EventHandler(this.popPages_Opened);
            this.popPages.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.popPages_ItemClicked);
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
            this.mSelectTab.DropDownOpening += new System.EventHandler(this.mSelectTab_DropDownOpening);
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
            // Pages
            // 
            this.Pages.AllowDrop = true;
            this.Pages.ContextMenuStrip = this.popPages;
            this.Pages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pages.Location = new System.Drawing.Point(0, 0);
            this.Pages.Multiline = true;
            this.Pages.Name = "Pages";
            this.Pages.SelectedIndex = 0;
            this.Pages.ShowToolTips = true;
            this.Pages.Size = new System.Drawing.Size(335, 296);
            this.Pages.TabIndex = 1;
            this.Pages.Selected += new System.Windows.Forms.TabControlEventHandler(this.Pages_Selected);
            this.Pages.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pages_DragDrop);
            this.Pages.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pages_DragEnter);
            this.Pages.DragOver += new System.Windows.Forms.DragEventHandler(this.Pages_DragOver);
            this.Pages.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pages_MouseDown);
            // 
            // PagesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Pages);
            this.Name = "PagesView";
            this.Size = new System.Drawing.Size(335, 296);
            this.popPages.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip popPages;
        private System.Windows.Forms.ToolStripMenuItem mNewTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem mCloseTab;
        private System.Windows.Forms.ToolStripMenuItem mRenameTab;
        private System.Windows.Forms.ToolStripMenuItem mSelectTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem mTabParams;
        internal System.Windows.Forms.TabControl Pages;
    }
}
