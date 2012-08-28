namespace LanExchange.View.Components
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
            this.LV = new BrightIdeasSoftware.FastObjectListView();
            ((System.ComponentModel.ISupportInitialize)(this.LV)).BeginInit();
            this.SuspendLayout();
            // 
            // LV
            // 
            this.LV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV.EmptyListMsg = "";
            this.LV.FullRowSelect = true;
            this.LV.GridLines = true;
            this.LV.Location = new System.Drawing.Point(0, 0);
            this.LV.Name = "LV";
            this.LV.ShowGroups = false;
            this.LV.Size = new System.Drawing.Size(368, 520);
            this.LV.TabIndex = 0;
            this.LV.UseCompatibleStateImageBehavior = false;
            this.LV.View = System.Windows.Forms.View.Details;
            this.LV.VirtualMode = true;
            this.LV.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LV_KeyDown);
            this.LV.DoubleClick += new System.EventHandler(this.LV_DoubleClick);
            // 
            // PanelView
            // 
            this.Controls.Add(this.LV);
            this.Name = "PanelView";
            this.Size = new System.Drawing.Size(368, 520);
            ((System.ComponentModel.ISupportInitialize)(this.LV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        public BrightIdeasSoftware.FastObjectListView LV;
    }
}
