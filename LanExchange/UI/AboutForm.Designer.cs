namespace LanExchange.UI
{
    public sealed partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.boxLicense = new System.Windows.Forms.RichTextBox();
            this.eEmail = new System.Windows.Forms.LinkLabel();
            this.lEmail = new System.Windows.Forms.Label();
            this.eWeb = new System.Windows.Forms.LinkLabel();
            this.lWeb = new System.Windows.Forms.Label();
            this.eVersion = new System.Windows.Forms.Label();
            this.bClose = new System.Windows.Forms.Button();
            this.bShowLicense = new System.Windows.Forms.Button();
            this.lCopyright = new System.Windows.Forms.Label();
            this.eLicense = new System.Windows.Forms.Label();
            this.lLicense = new System.Windows.Forms.Label();
            this.lVersion = new System.Windows.Forms.Label();
            this.eCopyright = new System.Windows.Forms.Label();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // boxLicense
            // 
            resources.ApplyResources(this.boxLicense, "boxLicense");
            this.boxLicense.BackColor = System.Drawing.SystemColors.Window;
            this.boxLicense.Name = "boxLicense";
            this.boxLicense.ReadOnly = true;
            // 
            // eEmail
            // 
            resources.ApplyResources(this.eEmail, "eEmail");
            this.eEmail.Name = "eEmail";
            this.eEmail.TabStop = true;
            this.eEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.eEmail_LinkClicked);
            // 
            // lEmail
            // 
            resources.ApplyResources(this.lEmail, "lEmail");
            this.lEmail.BackColor = System.Drawing.SystemColors.Control;
            this.lEmail.Name = "lEmail";
            // 
            // eWeb
            // 
            resources.ApplyResources(this.eWeb, "eWeb");
            this.eWeb.Name = "eWeb";
            this.eWeb.TabStop = true;
            this.eWeb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.eWeb_LinkClicked);
            // 
            // lWeb
            // 
            resources.ApplyResources(this.lWeb, "lWeb");
            this.lWeb.BackColor = System.Drawing.SystemColors.Control;
            this.lWeb.Name = "lWeb";
            // 
            // eVersion
            // 
            resources.ApplyResources(this.eVersion, "eVersion");
            this.eVersion.Name = "eVersion";
            // 
            // bClose
            // 
            resources.ApplyResources(this.bClose, "bClose");
            this.bClose.Name = "bClose";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // bShowLicense
            // 
            resources.ApplyResources(this.bShowLicense, "bShowLicense");
            this.bShowLicense.Name = "bShowLicense";
            this.bShowLicense.UseVisualStyleBackColor = true;
            this.bShowLicense.Click += new System.EventHandler(this.bShowLicense_Click);
            // 
            // lCopyright
            // 
            resources.ApplyResources(this.lCopyright, "lCopyright");
            this.lCopyright.BackColor = System.Drawing.SystemColors.Control;
            this.lCopyright.Name = "lCopyright";
            // 
            // eLicense
            // 
            resources.ApplyResources(this.eLicense, "eLicense");
            this.eLicense.Name = "eLicense";
            this.eLicense.TabStop = true;
            this.eLicense.UseCompatibleTextRendering = true;
            // 
            // lLicense
            // 
            resources.ApplyResources(this.lLicense, "lLicense");
            this.lLicense.BackColor = System.Drawing.SystemColors.Control;
            this.lLicense.Name = "lLicense";
            // 
            // lVersion
            // 
            resources.ApplyResources(this.lVersion, "lVersion");
            this.lVersion.BackColor = System.Drawing.SystemColors.Control;
            this.lVersion.Name = "lVersion";
            // 
            // eCopyright
            // 
            resources.ApplyResources(this.eCopyright, "eCopyright");
            this.eCopyright.Name = "eCopyright";
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Image = global::LanExchange.Properties.Resources.LanExchange_48x48;
            resources.ApplyResources(this.logoPictureBox, "logoPictureBox");
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.TabStop = false;
            // 
            // AboutForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.eEmail);
            this.Controls.Add(this.lEmail);
            this.Controls.Add(this.eWeb);
            this.Controls.Add(this.lWeb);
            this.Controls.Add(this.eVersion);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.bShowLicense);
            this.Controls.Add(this.lCopyright);
            this.Controls.Add(this.eLicense);
            this.Controls.Add(this.lLicense);
            this.Controls.Add(this.lVersion);
            this.Controls.Add(this.eCopyright);
            this.Controls.Add(this.logoPictureBox);
            this.Controls.Add(this.boxLicense);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AboutForm_FormClosed);
            this.Load += new System.EventHandler(this.AboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lVersion;
        private System.Windows.Forms.Label eCopyright;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label lLicense;
        private System.Windows.Forms.Label lCopyright;
        private System.Windows.Forms.Button bShowLicense;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.RichTextBox boxLicense;
        private System.Windows.Forms.Label eLicense;
        private System.Windows.Forms.Label eVersion;
        private System.Windows.Forms.Label lWeb;
        private System.Windows.Forms.LinkLabel eWeb;
        private System.Windows.Forms.Label lEmail;
        private System.Windows.Forms.LinkLabel eEmail;
    }
}
