namespace LanExchange.Plugin.Network
{
    partial class SettingsTabNetwork
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
            this.chShowPrinters = new System.Windows.Forms.CheckBox();
            this.chShowHiddenShares = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chShowPrinters
            // 
            this.chShowPrinters.AutoSize = true;
            this.chShowPrinters.Location = new System.Drawing.Point(16, 36);
            this.chShowPrinters.Name = "chShowPrinters";
            this.chShowPrinters.Size = new System.Drawing.Size(90, 17);
            this.chShowPrinters.TabIndex = 14;
            this.chShowPrinters.Text = "Show printers";
            this.chShowPrinters.UseVisualStyleBackColor = true;
            // 
            // chShowHiddenShares
            // 
            this.chShowHiddenShares.AutoSize = true;
            this.chShowHiddenShares.Location = new System.Drawing.Point(16, 16);
            this.chShowHiddenShares.Name = "chShowHiddenShares";
            this.chShowHiddenShares.Size = new System.Drawing.Size(122, 17);
            this.chShowHiddenShares.TabIndex = 13;
            this.chShowHiddenShares.Text = "Show hidden shares";
            this.chShowHiddenShares.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(16, 56);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(127, 17);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "Show share full name";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // SettingsTabNetwork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.chShowPrinters);
            this.Controls.Add(this.chShowHiddenShares);
            this.Name = "SettingsTabNetwork";
            this.Size = new System.Drawing.Size(226, 122);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chShowPrinters;
        private System.Windows.Forms.CheckBox chShowHiddenShares;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
