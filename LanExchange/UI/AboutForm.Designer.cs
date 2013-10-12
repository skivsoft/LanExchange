using LanExchange.Properties;

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

        #region Windows Form Designer generated code!

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
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
            this.lTwitter = new System.Windows.Forms.Label();
            this.eTwitter = new System.Windows.Forms.LinkLabel();
            this.tipAbout = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // boxLicense
            // 
            this.boxLicense.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxLicense.BackColor = System.Drawing.SystemColors.Window;
            this.boxLicense.Location = new System.Drawing.Point(16, 16);
            this.boxLicense.Name = "boxLicense";
            this.boxLicense.ReadOnly = true;
            this.boxLicense.Size = new System.Drawing.Size(360, 309);
            this.boxLicense.TabIndex = 38;
            this.boxLicense.Text = resources.GetString("boxLicense.Text");
            this.boxLicense.Visible = false;
            // 
            // eEmail
            // 
            this.eEmail.AutoSize = true;
            this.eEmail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.eEmail.Location = new System.Drawing.Point(93, 229);
            this.eEmail.Name = "eEmail";
            this.eEmail.Size = new System.Drawing.Size(31, 13);
            this.eEmail.TabIndex = 45;
            this.eEmail.TabStop = true;
            this.eEmail.Text = Resources.EmptyText;
            this.eEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.eEmail_LinkClicked);
            // 
            // lEmail
            // 
            this.lEmail.AutoSize = true;
            this.lEmail.BackColor = System.Drawing.SystemColors.Control;
            this.lEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lEmail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lEmail.Location = new System.Drawing.Point(80, 216);
            this.lEmail.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lEmail.Name = "lEmail";
            this.lEmail.Size = new System.Drawing.Size(37, 13);
            this.lEmail.TabIndex = 44;
            this.lEmail.Text = Resources.AboutForm_Email;
            this.lEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // eWeb
            // 
            this.eWeb.AutoSize = true;
            this.eWeb.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.eWeb.Location = new System.Drawing.Point(93, 152);
            this.eWeb.Name = "eWeb";
            this.eWeb.Size = new System.Drawing.Size(27, 13);
            this.eWeb.TabIndex = 43;
            this.eWeb.TabStop = true;
            this.eWeb.Text = Resources.EmptyText;
            this.eWeb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.eWeb_LinkClicked);
            // 
            // lWeb
            // 
            this.lWeb.AutoSize = true;
            this.lWeb.BackColor = System.Drawing.SystemColors.Control;
            this.lWeb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lWeb.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lWeb.Location = new System.Drawing.Point(80, 136);
            this.lWeb.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lWeb.Name = "lWeb";
            this.lWeb.Size = new System.Drawing.Size(61, 13);
            this.lWeb.TabIndex = 42;
            this.lWeb.Text = Resources.AboutForm_Webpage;
            this.lWeb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // eVersion
            // 
            this.eVersion.AutoSize = true;
            this.eVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.eVersion.Location = new System.Drawing.Point(93, 32);
            this.eVersion.Name = "eVersion";
            this.eVersion.Size = new System.Drawing.Size(31, 13);
            this.eVersion.TabIndex = 41;
            this.eVersion.Text = Resources.EmptyText;
            // 
            // bClose
            // 
            this.bClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bClose.Location = new System.Drawing.Point(276, 340);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(100, 23);
            this.bClose.TabIndex = 36;
            this.bClose.Text = Resources.Close;
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // bShowLicense
            // 
            this.bShowLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bShowLicense.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bShowLicense.Location = new System.Drawing.Point(166, 340);
            this.bShowLicense.Name = "bShowLicense";
            this.bShowLicense.Size = new System.Drawing.Size(100, 23);
            this.bShowLicense.TabIndex = 35;
            this.bShowLicense.UseVisualStyleBackColor = true;
            this.bShowLicense.Click += new System.EventHandler(this.bShowLicense_Click);
            // 
            // lCopyright
            // 
            this.lCopyright.AutoSize = true;
            this.lCopyright.BackColor = System.Drawing.SystemColors.Control;
            this.lCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lCopyright.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lCopyright.Location = new System.Drawing.Point(80, 96);
            this.lCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lCopyright.Name = "lCopyright";
            this.lCopyright.Size = new System.Drawing.Size(60, 13);
            this.lCopyright.TabIndex = 34;
            this.lCopyright.Text = Resources.AboutForm_Copyright;
            this.lCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // eLicense
            // 
            this.eLicense.AutoSize = true;
            this.eLicense.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.eLicense.Location = new System.Drawing.Point(93, 72);
            this.eLicense.Name = "eLicense";
            this.eLicense.Size = new System.Drawing.Size(167, 17);
            this.eLicense.TabIndex = 33;
            this.eLicense.TabStop = true;
            this.eLicense.Text = Resources.AboutForm_MIT;
            this.eLicense.UseCompatibleTextRendering = true;
            // 
            // lLicense
            // 
            this.lLicense.AutoSize = true;
            this.lLicense.BackColor = System.Drawing.SystemColors.Control;
            this.lLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lLicense.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lLicense.Location = new System.Drawing.Point(80, 56);
            this.lLicense.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lLicense.Name = "lLicense";
            this.lLicense.Size = new System.Drawing.Size(51, 13);
            this.lLicense.TabIndex = 31;
            this.lLicense.Text = Resources.AboutForm_License;
            this.lLicense.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lVersion
            // 
            this.lVersion.AutoSize = true;
            this.lVersion.BackColor = System.Drawing.SystemColors.Control;
            this.lVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lVersion.Location = new System.Drawing.Point(80, 16);
            this.lVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lVersion.Name = "lVersion";
            this.lVersion.Size = new System.Drawing.Size(49, 13);
            this.lVersion.TabIndex = 19;
            this.lVersion.Text = Resources.AboutForm_Version;
            this.lVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // eCopyright
            // 
            this.eCopyright.AutoSize = true;
            this.eCopyright.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.eCopyright.Location = new System.Drawing.Point(93, 112);
            this.eCopyright.Name = "eCopyright";
            this.eCopyright.Size = new System.Drawing.Size(51, 13);
            this.eCopyright.TabIndex = 28;
            this.eCopyright.Text = Resources.EmptyText;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Image = global::LanExchange.Properties.Resources.LanExchange_48x48;
            this.logoPictureBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.logoPictureBox.Location = new System.Drawing.Point(16, 16);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(48, 48);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 13;
            this.logoPictureBox.TabStop = false;
            // 
            // lTwitter
            // 
            this.lTwitter.AutoSize = true;
            this.lTwitter.BackColor = System.Drawing.SystemColors.Control;
            this.lTwitter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lTwitter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lTwitter.Location = new System.Drawing.Point(80, 175);
            this.lTwitter.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lTwitter.Name = "lTwitter";
            this.lTwitter.Size = new System.Drawing.Size(46, 13);
            this.lTwitter.TabIndex = 47;
            this.lTwitter.Text = Resources.AboutForm_Twitter;
            this.lTwitter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // eTwitter
            // 
            this.eTwitter.AutoSize = true;
            this.eTwitter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.eTwitter.Location = new System.Drawing.Point(93, 191);
            this.eTwitter.Name = "eTwitter";
            this.eTwitter.Size = new System.Drawing.Size(35, 13);
            this.eTwitter.TabIndex = 48;
            this.eTwitter.TabStop = true;
            this.eTwitter.Text = Resources.EmptyText;
            this.eTwitter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.eTwitter_LinkClicked);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 373);
            this.Controls.Add(this.eTwitter);
            this.Controls.Add(this.lTwitter);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = Resources.AboutForm_Title;
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
        private System.Windows.Forms.Label lTwitter;
        private System.Windows.Forms.LinkLabel eTwitter;
        private System.Windows.Forms.ToolTip tipAbout;
    }
}
