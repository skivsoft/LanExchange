namespace LanExchange.UI
{
    partial class InfoView
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
            this.imgInfo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // imgInfo
            // 
            this.imgInfo.Location = new System.Drawing.Point(8, 8);
            this.imgInfo.Name = "imgInfo";
            this.imgInfo.Size = new System.Drawing.Size(32, 32);
            this.imgInfo.TabIndex = 6;
            this.imgInfo.TabStop = false;
            // 
            // InfoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.imgInfo);
            this.MinimumSize = new System.Drawing.Size(0, 64);
            this.Name = "InfoView";
            this.Size = new System.Drawing.Size(486, 64);
            ((System.ComponentModel.ISupportInitialize)(this.imgInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imgInfo;
    }
}
