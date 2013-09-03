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
            this.mAfterComp = new System.Windows.Forms.ToolStripSeparator();
            this.mCopyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mCopySelected = new System.Windows.Forms.ToolStripMenuItem();
            this.mPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mAfterCopy = new System.Windows.Forms.ToolStripSeparator();
            this.mSendToNewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mContextProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.ePath = new System.Windows.Forms.TextBox();
            this.LV = new LanExchange.UI.ListViewer();
            this.pFilter = new FilterView();
            this.mDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.popComps.SuspendLayout();
            this.SuspendLayout();
            // 
            // popComps
            // 
            this.popComps.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mComp,
            this.mAfterComp,
            this.mCopyMenu,
            this.mPaste,
            this.mDelete,
            this.mAfterCopy,
            this.mSendToNewTab,
            this.toolStripSeparator6,
            this.mContextProperties});
            this.popComps.Name = "popComps";
            this.popComps.Size = new System.Drawing.Size(233, 176);
            this.popComps.Opening += new System.ComponentModel.CancelEventHandler(this.popComps_Opening);
            // 
            // mComp
            // 
            this.mComp.Enabled = false;
            this.mComp.Name = "mComp";
            this.mComp.Size = new System.Drawing.Size(232, 22);
            this.mComp.Tag = "";
            this.mComp.Text = "\\\\COMPUTER";
            // 
            // mAfterComp
            // 
            this.mAfterComp.Name = "mAfterComp";
            this.mAfterComp.Size = new System.Drawing.Size(229, 6);
            // 
            // mCopyMenu
            // 
            this.mCopyMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mCopySelected});
            this.mCopyMenu.Name = "mCopyMenu";
            this.mCopyMenu.Size = new System.Drawing.Size(232, 22);
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
            // mPaste
            // 
            this.mPaste.Name = "mPaste";
            this.mPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mPaste.Size = new System.Drawing.Size(232, 22);
            this.mPaste.Text = "Paste";
            this.mPaste.Click += new System.EventHandler(this.mPaste_Click);
            // 
            // mAfterCopy
            // 
            this.mAfterCopy.Name = "mAfterCopy";
            this.mAfterCopy.Size = new System.Drawing.Size(229, 6);
            // 
            // mSendToNewTab
            // 
            this.mSendToNewTab.Name = "mSendToNewTab";
            this.mSendToNewTab.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
            this.mSendToNewTab.Size = new System.Drawing.Size(232, 22);
            this.mSendToNewTab.Text = "Send to new tab";
            this.mSendToNewTab.Click += new System.EventHandler(this.mSendToNewTab_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(229, 6);
            // 
            // mContextProperties
            // 
            this.mContextProperties.Name = "mContextProperties";
            this.mContextProperties.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mContextProperties.Size = new System.Drawing.Size(232, 22);
            this.mContextProperties.Text = "Tab properties";
            this.mContextProperties.Click += new System.EventHandler(this.mContextProperties_Click);
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
            // mDelete
            // 
            this.mDelete.Name = "mDelete";
            this.mDelete.ShortcutKeyDisplayString = "Del";
            this.mDelete.Size = new System.Drawing.Size(232, 22);
            this.mDelete.Text = "Delete";
            this.mDelete.Click += new System.EventHandler(this.mDelete_Click);
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

        private LanExchange.UI.ListViewer LV;
        public System.Windows.Forms.ContextMenuStrip popComps;
        public System.Windows.Forms.ToolStripMenuItem mComp;
        private System.Windows.Forms.ToolStripSeparator mAfterComp;
        private System.Windows.Forms.ToolStripSeparator mAfterCopy;
        private System.Windows.Forms.ToolStripMenuItem mSendToNewTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mContextProperties;
        private FilterView pFilter;
        private System.Windows.Forms.TextBox ePath;
        private System.Windows.Forms.ToolStripMenuItem mCopyMenu;
        private System.Windows.Forms.ToolStripMenuItem mCopySelected;
        private System.Windows.Forms.ToolStripMenuItem mPaste;
        private System.Windows.Forms.ToolStripMenuItem mDelete;
    }
}
