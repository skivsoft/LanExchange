namespace ViewWinForms.View.Forms
{
    partial class AboutForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lVersion = new System.Windows.Forms.Label();
            this.lAppName = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lCopyright = new System.Windows.Forms.Label();
            this.bevelControl3 = new Bevel.BevelControl();
            this.bevelControl1 = new Bevel.BevelControl();
            this.bevelControl2 = new Bevel.BevelControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(398, 181);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lVersion);
            this.panel1.Controls.Add(this.lAppName);
            this.panel1.Controls.Add(this.picLogo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 140);
            this.panel1.TabIndex = 9;
            // 
            // lVersion
            // 
            this.lVersion.AutoSize = true;
            this.lVersion.Location = new System.Drawing.Point(112, 65);
            this.lVersion.Name = "lVersion";
            this.lVersion.Size = new System.Drawing.Size(41, 13);
            this.lVersion.TabIndex = 8;
            this.lVersion.Text = "version";
            // 
            // lAppName
            // 
            this.lAppName.AutoSize = true;
            this.lAppName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lAppName.Location = new System.Drawing.Point(111, 30);
            this.lAppName.Name = "lAppName";
            this.lAppName.Size = new System.Drawing.Size(37, 24);
            this.lAppName.TabIndex = 7;
            this.lAppName.Text = "title";
            // 
            // picLogo
            // 
            this.picLogo.Location = new System.Drawing.Point(30, 30);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(48, 48);
            this.picLogo.TabIndex = 6;
            this.picLogo.TabStop = false;
            // 
            // lCopyright
            // 
            this.lCopyright.AutoSize = true;
            this.lCopyright.Location = new System.Drawing.Point(27, 191);
            this.lCopyright.Name = "lCopyright";
            this.lCopyright.Size = new System.Drawing.Size(50, 13);
            this.lCopyright.TabIndex = 10;
            this.lCopyright.Text = "copyright";
            // 
            // bevelControl3
            // 
            this.bevelControl3.BevelStyle = Bevel.BevelStyle.Lowered;
            this.bevelControl3.BevelType = Bevel.BevelType.TopLine;
            this.bevelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.bevelControl3.HighlightColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bevelControl3.Location = new System.Drawing.Point(0, 140);
            this.bevelControl3.Name = "bevelControl3";
            this.bevelControl3.ShadowColor = System.Drawing.SystemColors.ButtonShadow;
            this.bevelControl3.Size = new System.Drawing.Size(494, 14);
            this.bevelControl3.TabIndex = 11;
            this.bevelControl3.Text = "bevelControl3";
            // 
            // bevelControl1
            // 
            this.bevelControl1.BevelStyle = Bevel.BevelStyle.Lowered;
            this.bevelControl1.BevelType = Bevel.BevelType.TopLine;
            this.bevelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bevelControl1.HighlightColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bevelControl1.Location = new System.Drawing.Point(0, 140);
            this.bevelControl1.Name = "bevelControl1";
            this.bevelControl1.ShadowColor = System.Drawing.SystemColors.ButtonShadow;
            this.bevelControl1.Size = new System.Drawing.Size(494, 8);
            this.bevelControl1.TabIndex = 11;
            this.bevelControl1.Text = "bevelControl1";
            // 
            // bevelControl2
            // 
            this.bevelControl2.BevelStyle = Bevel.BevelStyle.Lowered;
            this.bevelControl2.BevelType = Bevel.BevelType.TopLine;
            this.bevelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.bevelControl2.HighlightColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bevelControl2.Location = new System.Drawing.Point(0, 140);
            this.bevelControl2.Name = "bevelControl2";
            this.bevelControl2.ShadowColor = System.Drawing.SystemColors.ButtonShadow;
            this.bevelControl2.Size = new System.Drawing.Size(494, 15);
            this.bevelControl2.TabIndex = 11;
            this.bevelControl2.Text = "bevelControl2";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(494, 221);
            this.Controls.Add(this.bevelControl3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lCopyright);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AboutForm";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lVersion;
        private System.Windows.Forms.Label lAppName;
        public System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lCopyright;
        private Bevel.BevelControl bevelControl1;
        private Bevel.BevelControl bevelControl2;
        private Bevel.BevelControl bevelControl3;

    }
}