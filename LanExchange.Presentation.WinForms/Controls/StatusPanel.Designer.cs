namespace LanExchange.Presentation.WinForms.Controls
{
    partial class StatusPanel
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
            this.Status = new System.Windows.Forms.StatusStrip();
            this.lItemsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusSep1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lCompName = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusSep2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status.SuspendLayout();
            this.SuspendLayout();
            // 
            // Status
            // 
            this.Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lItemsCount,
            this.StatusSep1,
            this.lCompName,
            this.StatusSep2,
            this.lUserName});
            this.Status.Location = new System.Drawing.Point(0, 23);
            this.Status.Name = "Status";
            this.Status.ShowItemToolTips = true;
            this.Status.Size = new System.Drawing.Size(493, 22);
            this.Status.TabIndex = 16;
            this.Status.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Status_MouseDoubleClick);
            // 
            // lItemsCount
            // 
            this.lItemsCount.Name = "lItemsCount";
            this.lItemsCount.Size = new System.Drawing.Size(439, 17);
            this.lItemsCount.Spring = true;
            this.lItemsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusSep1
            // 
            this.StatusSep1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.StatusSep1.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.StatusSep1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.StatusSep1.Name = "StatusSep1";
            this.StatusSep1.Size = new System.Drawing.Size(4, 17);
            // 
            // lCompName
            // 
            this.lCompName.Name = "lCompName";
            this.lCompName.Text = "    ";
            this.lCompName.Size = new System.Drawing.Size(0, 17);
            this.lCompName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lCompName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lCompName_MouseUp);
            // 
            // StatusSep2
            // 
            this.StatusSep2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.StatusSep2.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.StatusSep2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.StatusSep2.Name = "StatusSep2";
            this.StatusSep2.Size = new System.Drawing.Size(4, 17);
            // 
            // lUserName
            // 
            this.lUserName.Name = "lUserName";
            this.lUserName.Text = "    ";
            this.lUserName.Size = new System.Drawing.Size(0, 17);
            this.lUserName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lUserName_MouseUp);
            // 
            // StatusPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.Status);
            this.Name = "StatusPanel";
            this.Size = new System.Drawing.Size(493, 45);
            this.Status.ResumeLayout(false);
            this.Status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip Status;
        public System.Windows.Forms.ToolStripStatusLabel lItemsCount;
        private System.Windows.Forms.ToolStripStatusLabel StatusSep1;
        private System.Windows.Forms.ToolStripStatusLabel lCompName;
        private System.Windows.Forms.ToolStripStatusLabel StatusSep2;
        private System.Windows.Forms.ToolStripStatusLabel lUserName;
    }
}
