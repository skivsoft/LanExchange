namespace LanExchange.UI
{
    partial class SettingsTabGeneral
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
            this.chAdvanced = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.eRefreshTime = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chMinimized = new System.Windows.Forms.CheckBox();
            this.chAutorun = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.eRefreshTime)).BeginInit();
            this.SuspendLayout();
            // 
            // chAdvanced
            // 
            this.chAdvanced.AutoSize = true;
            this.chAdvanced.Location = new System.Drawing.Point(16, 56);
            this.chAdvanced.Name = "chAdvanced";
            this.chAdvanced.Size = new System.Drawing.Size(206, 17);
            this.chAdvanced.TabIndex = 8;
            this.chAdvanced.Text = "Advanced functional for administration";
            this.chAdvanced.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "minutes";
            // 
            // eRefreshTime
            // 
            this.eRefreshTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eRefreshTime.Location = new System.Drawing.Point(244, 94);
            this.eRefreshTime.Name = "eRefreshTime";
            this.eRefreshTime.Size = new System.Drawing.Size(40, 20);
            this.eRefreshTime.TabIndex = 10;
            this.eRefreshTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Update interval for lists";
            // 
            // chMinimized
            // 
            this.chMinimized.AutoSize = true;
            this.chMinimized.Location = new System.Drawing.Point(16, 36);
            this.chMinimized.Name = "chMinimized";
            this.chMinimized.Size = new System.Drawing.Size(145, 17);
            this.chMinimized.TabIndex = 9;
            this.chMinimized.Text = "Minimize program on start";
            this.chMinimized.UseVisualStyleBackColor = true;
            // 
            // chAutorun
            // 
            this.chAutorun.AutoSize = true;
            this.chAutorun.Location = new System.Drawing.Point(16, 16);
            this.chAutorun.Name = "chAutorun";
            this.chAutorun.Size = new System.Drawing.Size(170, 17);
            this.chAutorun.TabIndex = 7;
            this.chAutorun.Text = "Start program when user logon";
            this.chAutorun.UseVisualStyleBackColor = true;
            // 
            // SettingsTabGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chAdvanced);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.eRefreshTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chMinimized);
            this.Controls.Add(this.chAutorun);
            this.Name = "SettingsTabGeneral";
            this.Size = new System.Drawing.Size(339, 175);
            ((System.ComponentModel.ISupportInitialize)(this.eRefreshTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chAdvanced;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown eRefreshTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chMinimized;
        private System.Windows.Forms.CheckBox chAutorun;
    }
}
