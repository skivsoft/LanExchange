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
            this.lInfoOS = new System.Windows.Forms.Label();
            this.imgInfo = new System.Windows.Forms.PictureBox();
            this.lInfoDesc = new System.Windows.Forms.Label();
            this.lInfoComp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // lInfoOS
            // 
            this.lInfoOS.AutoSize = true;
            this.lInfoOS.Location = new System.Drawing.Point(25, 37);
            this.lInfoOS.Name = "lInfoOS";
            this.lInfoOS.Size = new System.Drawing.Size(19, 13);
            this.lInfoOS.TabIndex = 7;
            this.lInfoOS.Text = "    ";
            // 
            // imgInfo
            // 
            this.imgInfo.Location = new System.Drawing.Point(3, 3);
            this.imgInfo.Name = "imgInfo";
            this.imgInfo.Size = new System.Drawing.Size(16, 16);
            this.imgInfo.TabIndex = 6;
            this.imgInfo.TabStop = false;
            // 
            // lInfoDesc
            // 
            this.lInfoDesc.AutoSize = true;
            this.lInfoDesc.Location = new System.Drawing.Point(25, 20);
            this.lInfoDesc.Name = "lInfoDesc";
            this.lInfoDesc.Size = new System.Drawing.Size(19, 13);
            this.lInfoDesc.TabIndex = 5;
            this.lInfoDesc.Text = "    ";
            // 
            // lInfoComp
            // 
            this.lInfoComp.AutoSize = true;
            this.lInfoComp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lInfoComp.Location = new System.Drawing.Point(25, 3);
            this.lInfoComp.Name = "lInfoComp";
            this.lInfoComp.Size = new System.Drawing.Size(23, 13);
            this.lInfoComp.TabIndex = 4;
            this.lInfoComp.Text = "    ";
            // 
            // InfoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lInfoOS);
            this.Controls.Add(this.imgInfo);
            this.Controls.Add(this.lInfoDesc);
            this.Controls.Add(this.lInfoComp);
            this.Name = "InfoView";
            this.Size = new System.Drawing.Size(486, 60);
            ((System.ComponentModel.ISupportInitialize)(this.imgInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lInfoOS;
        private System.Windows.Forms.PictureBox imgInfo;
        private System.Windows.Forms.Label lInfoDesc;
        private System.Windows.Forms.Label lInfoComp;
    }
}
