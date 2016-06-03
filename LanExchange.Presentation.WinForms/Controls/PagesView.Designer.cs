using LanExchange.Presentation.WinForms.Properties;

namespace LanExchange.Presentation.WinForms.Controls
{
    public partial class PagesView
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
            this.mReRead = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mCloseTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mCloseOther = new System.Windows.Forms.ToolStripMenuItem();
            this.Pages = new System.Windows.Forms.TabControl();
            this.popPages.SuspendLayout();
            this.SuspendLayout();
            // 
            // popPages
            // 
            this.popPages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mReRead,
            this.toolStripSeparator2,
            this.mCloseTab,
            this.mCloseOther});
            this.popPages.Name = "popPages";
            this.popPages.Size = new System.Drawing.Size(169, 76);
            this.popPages.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.popPages_Closed);
            this.popPages.Opened += new System.EventHandler(this.popPages_Opened);
            this.popPages.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.popPages_ItemClicked);
            // 
            // mReRead
            // 
            this.mReRead.Name = "mReRead";
            this.mReRead.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.mReRead.Size = new System.Drawing.Size(168, 22);
            this.mReRead.Text = Resources.mReRead_Text;
            this.mReRead.Click += new System.EventHandler(this.mReRead_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(165, 6);
            // 
            // mCloseTab
            // 
            this.mCloseTab.Name = "mCloseTab";
            this.mCloseTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.mCloseTab.Size = new System.Drawing.Size(168, 22);
            this.mCloseTab.Text = Resources.mCloseTab_Text;
            this.mCloseTab.Click += new System.EventHandler(this.mCloseTab_Click);
            // 
            // mCloseOther
            // 
            this.mCloseOther.Name = "mCloseOther";
            this.mCloseOther.Size = new System.Drawing.Size(168, 22);
            this.mCloseOther.Text = Resources.mCloseOther_Text;
            this.mCloseOther.Click += new System.EventHandler(this.mCloseOther_Click);
            // 
            // Pages
            // 
            this.Pages.AllowDrop = true;
            this.Pages.ContextMenuStrip = this.popPages;
            this.Pages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pages.ItemSize = new System.Drawing.Size(50, 18);
            this.Pages.Location = new System.Drawing.Point(0, 0);
            this.Pages.Name = "Pages";
            this.Pages.SelectedIndex = 0;
            this.Pages.Size = new System.Drawing.Size(484, 621);
            this.Pages.TabIndex = 1;
            this.Pages.Selected += new System.Windows.Forms.TabControlEventHandler(this.Pages_Selected);
            this.Pages.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Pages_KeyDown);
            this.Pages.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Pages_KeyUp);
            this.Pages.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pages_MouseDown);
            // 
            // PagesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.popPages;
            this.Controls.Add(this.Pages);
            this.Name = "PagesView";
            this.Size = new System.Drawing.Size(484, 621);
            this.RightToLeftChanged += new System.EventHandler(this.PagesView_RightToLeftChanged);
            this.popPages.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip popPages;
        public System.Windows.Forms.TabControl Pages;
        private System.Windows.Forms.ToolStripMenuItem mReRead;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mCloseTab;
        private System.Windows.Forms.ToolStripMenuItem mCloseOther;
    }
}
