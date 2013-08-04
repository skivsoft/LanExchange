namespace LanExchange.UI
{
    partial class SettingsTabUI
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
            this.PropGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // PropGrid
            // 
            this.PropGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropGrid.Location = new System.Drawing.Point(0, 0);
            this.PropGrid.Name = "PropGrid";
            this.PropGrid.Size = new System.Drawing.Size(352, 292);
            this.PropGrid.TabIndex = 0;
            // 
            // SettingsTabUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PropGrid);
            this.Name = "SettingsTabUI";
            this.Size = new System.Drawing.Size(352, 292);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid PropGrid;

    }
}
