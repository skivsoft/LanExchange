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
            this.chMinimized = new System.Windows.Forms.CheckBox();
            this.chAutorun = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chAdvanced
            // 
            this.chAdvanced.AutoSize = true;
            this.chAdvanced.Location = new System.Drawing.Point(16, 56);
            this.chAdvanced.Name = "chAdvanced";
            this.chAdvanced.Size = new System.Drawing.Size(198, 17);
            this.chAdvanced.TabIndex = 8;
            this.chAdvanced.Text = "Advanced features for administration";
            this.chAdvanced.UseVisualStyleBackColor = true;
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
            this.Controls.Add(this.chMinimized);
            this.Controls.Add(this.chAutorun);
            this.Name = "SettingsTabGeneral";
            this.Size = new System.Drawing.Size(339, 175);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chAdvanced;
        private System.Windows.Forms.CheckBox chMinimized;
        private System.Windows.Forms.CheckBox chAutorun;
    }
}
