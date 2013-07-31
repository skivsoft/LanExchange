namespace LanExchange.UI
{
    partial class SettingsForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.bCancel = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.tvSettings = new System.Windows.Forms.TreeView();
            this.pTop = new System.Windows.Forms.Panel();
            this.lTop = new System.Windows.Forms.Label();
            this.pContent = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.pTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bCancel);
            this.panel1.Controls.Add(this.bSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 333);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(594, 40);
            this.panel1.TabIndex = 1;
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(506, 9);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(80, 23);
            this.bCancel.TabIndex = 1;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bSave.Location = new System.Drawing.Point(412, 9);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(80, 23);
            this.bSave.TabIndex = 0;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // tvSettings
            // 
            this.tvSettings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvSettings.Location = new System.Drawing.Point(0, 0);
            this.tvSettings.Name = "tvSettings";
            this.tvSettings.Size = new System.Drawing.Size(150, 333);
            this.tvSettings.TabIndex = 2;
            this.tvSettings.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSettings_AfterSelect);
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.Silver;
            this.pTop.Controls.Add(this.lTop);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(150, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(444, 32);
            this.pTop.TabIndex = 3;
            // 
            // lTop
            // 
            this.lTop.BackColor = System.Drawing.Color.DarkGray;
            this.lTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lTop.ForeColor = System.Drawing.Color.White;
            this.lTop.Location = new System.Drawing.Point(0, 0);
            this.lTop.Name = "lTop";
            this.lTop.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lTop.Size = new System.Drawing.Size(444, 32);
            this.lTop.TabIndex = 0;
            this.lTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pContent
            // 
            this.pContent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pContent.Location = new System.Drawing.Point(150, 32);
            this.pContent.Name = "pContent";
            this.pContent.Size = new System.Drawing.Size(444, 301);
            this.pContent.TabIndex = 4;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 373);
            this.Controls.Add(this.pContent);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.tvSettings);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preferences";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            this.panel1.ResumeLayout(false);
            this.pTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.TreeView tvSettings;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Label lTop;
        private System.Windows.Forms.Panel pContent;
    }
}