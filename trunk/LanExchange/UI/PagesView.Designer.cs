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
            this.mAfterNewTab = new System.Windows.Forms.ToolStripSeparator();
            this.mCloseTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mRenameTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mBeforeSelectTab = new System.Windows.Forms.ToolStripSeparator();
            this.mSelectTab = new System.Windows.Forms.ToolStripMenuItem();
            this.Pages = new System.Windows.Forms.TabControl();
            this.popPages.SuspendLayout();
            this.SuspendLayout();
            // 
            // popPages
            // 
            this.popPages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mNewTab,
            this.mAfterNewTab,
            this.mCloseTab,
            this.mRenameTab,
            this.mBeforeSelectTab,
            this.mSelectTab});
            this.popPages.Name = "popPages";
            this.popPages.Size = new System.Drawing.Size(194, 126);
            this.popPages.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.popPages_Closed);
            this.popPages.Opening += new System.ComponentModel.CancelEventHandler(this.popPages_Opening);
            this.popPages.Opened += new System.EventHandler(this.popPages_Opened);
            this.popPages.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.popPages_ItemClicked);
            // 
            // mNewTab
            // 
            this.mNewTab.Name = "mNewTab";
            this.mNewTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.mNewTab.Size = new System.Drawing.Size(193, 22);
            this.mNewTab.Text = "New tab";
            this.mNewTab.Click += new System.EventHandler(this.mNewTab_Click);
            // 
            // mAfterNewTab
            // 
            this.mAfterNewTab.Name = "mAfterNewTab";
            this.mAfterNewTab.Size = new System.Drawing.Size(190, 6);
            // 
            // mCloseTab
            // 
            this.mCloseTab.Name = "mCloseTab";
            this.mCloseTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.mCloseTab.Size = new System.Drawing.Size(193, 22);
            this.mCloseTab.Text = "Close tab";
            this.mCloseTab.Click += new System.EventHandler(this.mCloseTab_Click);
            // 
            // mRenameTab
            // 
            this.mRenameTab.Name = "mRenameTab";
            this.mRenameTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mRenameTab.Size = new System.Drawing.Size(193, 22);
            this.mRenameTab.Text = "Tab properties";
            this.mRenameTab.Click += new System.EventHandler(this.mRenameTab_Click);
            // 
            // mBeforeSelectTab
            // 
            this.mBeforeSelectTab.Name = "mBeforeSelectTab";
            this.mBeforeSelectTab.Size = new System.Drawing.Size(190, 6);
            // 
            // mSelectTab
            // 
            this.mSelectTab.Name = "mSelectTab";
            this.mSelectTab.Size = new System.Drawing.Size(193, 22);
            this.mSelectTab.Text = "Select tab";
            this.mSelectTab.DropDownOpening += new System.EventHandler(this.mSelectTab_DropDownOpening);
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
        private System.Windows.Forms.ToolStripMenuItem mCloseTab;
        private System.Windows.Forms.ToolStripMenuItem mRenameTab;
        private System.Windows.Forms.ToolStripMenuItem mSelectTab;
        internal System.Windows.Forms.TabControl Pages;
        private System.Windows.Forms.ToolStripSeparator mBeforeSelectTab;
        private System.Windows.Forms.ToolStripSeparator mAfterNewTab;
    }
}
