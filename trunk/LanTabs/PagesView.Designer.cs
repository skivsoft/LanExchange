namespace LanTabs
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
            this.Pages = new System.Windows.Forms.TabControl();
            this.tabNew = new System.Windows.Forms.TabPage();
            this.popPages = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mNewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mReload = new System.Windows.Forms.ToolStripMenuItem();
            this.mCloseTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mCloseOther = new System.Windows.Forms.ToolStripMenuItem();
            this.mMoveToLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.mMoveToRight = new System.Windows.Forms.ToolStripMenuItem();
            this.Pages.SuspendLayout();
            this.popPages.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pages
            // 
            this.Pages.Controls.Add(this.tabNew);
            this.Pages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pages.Location = new System.Drawing.Point(0, 0);
            this.Pages.Name = "Pages";
            this.Pages.SelectedIndex = 0;
            this.Pages.Size = new System.Drawing.Size(384, 405);
            this.Pages.TabIndex = 0;
            this.Pages.Selected += new System.Windows.Forms.TabControlEventHandler(this.Pages_Selected);
            // 
            // tabNew
            // 
            this.tabNew.Location = new System.Drawing.Point(4, 22);
            this.tabNew.Name = "tabNew";
            this.tabNew.Padding = new System.Windows.Forms.Padding(3);
            this.tabNew.Size = new System.Drawing.Size(376, 379);
            this.tabNew.TabIndex = 1;
            this.tabNew.UseVisualStyleBackColor = true;
            // 
            // popPages
            // 
            this.popPages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mNewTab,
            this.toolStripSeparator1,
            this.mReload,
            this.mMoveToLeft,
            this.mMoveToRight,
            this.toolStripSeparator2,
            this.mCloseTab,
            this.mCloseOther});
            this.popPages.Name = "popPages";
            this.popPages.Size = new System.Drawing.Size(173, 170);
            // 
            // mNewTab
            // 
            this.mNewTab.Name = "mNewTab";
            this.mNewTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.mNewTab.Size = new System.Drawing.Size(172, 22);
            this.mNewTab.Text = "New tab";
            this.mNewTab.Click += new System.EventHandler(this.mNewTab_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // mReload
            // 
            this.mReload.Name = "mReload";
            this.mReload.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.mReload.Size = new System.Drawing.Size(172, 22);
            this.mReload.Text = "Reload";
            this.mReload.Click += new System.EventHandler(this.mReload_Click);
            // 
            // mCloseTab
            // 
            this.mCloseTab.Name = "mCloseTab";
            this.mCloseTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.mCloseTab.Size = new System.Drawing.Size(172, 22);
            this.mCloseTab.Text = "Close tab";
            this.mCloseTab.Click += new System.EventHandler(this.mCloseTab_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);
            // 
            // mCloseOther
            // 
            this.mCloseOther.Name = "mCloseOther";
            this.mCloseOther.Size = new System.Drawing.Size(172, 22);
            this.mCloseOther.Text = "Close other tabs";
            this.mCloseOther.Click += new System.EventHandler(this.mCloseOther_Click);
            // 
            // mMoveToLeft
            // 
            this.mMoveToLeft.Name = "mMoveToLeft";
            this.mMoveToLeft.Size = new System.Drawing.Size(172, 22);
            this.mMoveToLeft.Text = "Move to left";
            this.mMoveToLeft.Click += new System.EventHandler(this.mMoveToLeft_Click);
            // 
            // mMoveToRight
            // 
            this.mMoveToRight.Name = "mMoveToRight";
            this.mMoveToRight.Size = new System.Drawing.Size(172, 22);
            this.mMoveToRight.Text = "Move to right";
            this.mMoveToRight.Click += new System.EventHandler(this.mMoveToRight_Click);
            // 
            // PagesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.popPages;
            this.Controls.Add(this.Pages);
            this.Name = "PagesView";
            this.Size = new System.Drawing.Size(384, 405);
            this.Pages.ResumeLayout(false);
            this.popPages.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Pages;
        private System.Windows.Forms.TabPage tabNew;
        private System.Windows.Forms.ContextMenuStrip popPages;
        private System.Windows.Forms.ToolStripMenuItem mNewTab;
        private System.Windows.Forms.ToolStripMenuItem mCloseTab;
        private System.Windows.Forms.ToolStripMenuItem mReload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mMoveToLeft;
        private System.Windows.Forms.ToolStripMenuItem mMoveToRight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mCloseOther;
    }
}
