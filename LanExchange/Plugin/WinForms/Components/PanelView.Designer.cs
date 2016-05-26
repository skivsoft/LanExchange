namespace LanExchange.Plugin.WinForms.Components
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
            this.mDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mAfterDelete = new System.Windows.Forms.ToolStripSeparator();
            this.mNewItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LV = new ListViewer(userService);
            this.pFilter = new FilterView();
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
            this.mAfterDelete,
            this.mNewItem});
            this.popComps.Name = "popComps";
            this.popComps.Size = new System.Drawing.Size(153, 148);
            this.popComps.Opening += new System.ComponentModel.CancelEventHandler(this.popComps_Opening);
            // 
            // mComp
            // 
            this.mComp.Enabled = false;
            this.mComp.Name = "mComp";
            this.mComp.Size = new System.Drawing.Size(152, 22);
            this.mComp.Tag = "";
            // 
            // mAfterComp
            // 
            this.mAfterComp.Name = "mAfterComp";
            this.mAfterComp.Size = new System.Drawing.Size(149, 6);
            // 
            // mCopyMenu
            // 
            this.mCopyMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mCopySelected});
            this.mCopyMenu.Name = "mCopyMenu";
            this.mCopyMenu.Size = new System.Drawing.Size(152, 22);
            this.mCopyMenu.Text = global::LanExchange.Properties.Resources.mCopyMenu_Text;
            this.mCopyMenu.DropDownOpening += new System.EventHandler(this.mCopyMenu_DropDownOpening);
            // 
            // mCopySelected
            // 
            this.mCopySelected.Name = "mCopySelected";
            this.mCopySelected.ShortcutKeyDisplayString = global::LanExchange.Properties.Resources.KeyCtrlC;
            this.mCopySelected.Size = new System.Drawing.Size(222, 22);
            this.mCopySelected.Text = global::LanExchange.Properties.Resources.KeyCtrlC_;
            this.mCopySelected.Click += new System.EventHandler(this.CopySelectedOnClick);
            // 
            // mPaste
            // 
            this.mPaste.Name = "mPaste";
            this.mPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mPaste.Size = new System.Drawing.Size(152, 22);
            this.mPaste.Text = global::LanExchange.Properties.Resources.mPaste_Text;
            this.mPaste.Click += new System.EventHandler(this.mPaste_Click);
            // 
            // mDelete
            // 
            this.mDelete.Name = "mDelete";
            this.mDelete.ShortcutKeyDisplayString = global::LanExchange.Properties.Resources.KeyDel;
            this.mDelete.Size = new System.Drawing.Size(152, 22);
            this.mDelete.Text = global::LanExchange.Properties.Resources.mDelete_Text;
            this.mDelete.Click += new System.EventHandler(this.mDelete_Click);
            // 
            // mAfterDelete
            // 
            this.mAfterDelete.Name = "mAfterDelete";
            this.mAfterDelete.Size = new System.Drawing.Size(149, 6);
            this.mAfterDelete.Visible = false;
            // 
            // mNewItem
            // 
            this.mNewItem.Name = "mNewItem";
            this.mNewItem.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.mNewItem.Size = new System.Drawing.Size(152, 22);
            this.mNewItem.Text = "New item";
            this.mNewItem.Visible = false;
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
            this.LV.Location = new System.Drawing.Point(0, 0);
            this.LV.Margin = new System.Windows.Forms.Padding(0);
            this.LV.Name = "LV";
            this.LV.ShowItemToolTips = true;
            this.LV.Size = new System.Drawing.Size(423, 368);
            this.LV.TabIndex = 26;
            this.LV.ToolTipActive = false;
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
            this.pFilter.Presenter = null;
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
            this.Controls.Add(this.pFilter);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PanelView";
            this.Size = new System.Drawing.Size(423, 398);
            this.RightToLeftChanged += new System.EventHandler(this.PanelView_RightToLeftChanged);
            this.popComps.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ListViewer LV;
        public System.Windows.Forms.ContextMenuStrip popComps;
        public System.Windows.Forms.ToolStripMenuItem mComp;
        private System.Windows.Forms.ToolStripSeparator mAfterComp;
        private System.Windows.Forms.ToolStripSeparator mAfterDelete;
        private FilterView pFilter;
        private System.Windows.Forms.ToolStripMenuItem mCopyMenu;
        private System.Windows.Forms.ToolStripMenuItem mCopySelected;
        private System.Windows.Forms.ToolStripMenuItem mPaste;
        private System.Windows.Forms.ToolStripMenuItem mDelete;
        private System.Windows.Forms.ToolStripMenuItem mNewItem;
    }
}
