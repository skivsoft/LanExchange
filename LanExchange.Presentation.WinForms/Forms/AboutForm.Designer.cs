using LanExchange.Presentation.WinForms.Properties;

namespace LanExchange.Presentation.WinForms.Forms
{
    internal sealed partial class AboutForm
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
            this.components = new System.ComponentModel.Container();
            this.eWeb = new System.Windows.Forms.LinkLabel();
            this.lWeb = new System.Windows.Forms.Label();
            this.eVersion = new System.Windows.Forms.Label();
            this.bClose = new System.Windows.Forms.Button();
            this.bShowDetails = new System.Windows.Forms.Button();
            this.lCopyright = new System.Windows.Forms.Label();
            this.eLicense = new System.Windows.Forms.Label();
            this.lLicense = new System.Windows.Forms.Label();
            this.lVersion = new System.Windows.Forms.Label();
            this.eCopyright = new System.Windows.Forms.Label();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.tipAbout = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // eWeb
            // 
            this.eWeb.AutoSize = true;
            this.eWeb.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.eWeb.Location = new System.Drawing.Point(93, 152);
            this.eWeb.Name = "eWeb";
            this.eWeb.Size = new System.Drawing.Size(19, 13);
            this.eWeb.TabIndex = 43;
            this.eWeb.TabStop = true;
            this.eWeb.Text = "    ";
            this.eWeb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.EditWeb_LinkClicked);
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
            this.lWeb.Size = new System.Drawing.Size(23, 13);
            this.lWeb.TabIndex = 42;
            this.lWeb.Text = "    ";
            this.lWeb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // eVersion
            // 
            this.eVersion.AutoSize = true;
            this.eVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.eVersion.Location = new System.Drawing.Point(93, 32);
            this.eVersion.Name = "eVersion";
            this.eVersion.Size = new System.Drawing.Size(19, 13);
            this.eVersion.TabIndex = 41;
            this.eVersion.Text = "    ";
            // 
            // bClose
            // 
            this.bClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bClose.Location = new System.Drawing.Point(266, 209);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(100, 23);
            this.bClose.TabIndex = 36;
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // bShowDetails
            // 
            this.bShowDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bShowDetails.AutoSize = true;
            this.bShowDetails.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bShowDetails.Location = new System.Drawing.Point(156, 209);
            this.bShowDetails.Name = "bShowDetails";
            this.bShowDetails.Size = new System.Drawing.Size(100, 23);
            this.bShowDetails.TabIndex = 35;
            this.bShowDetails.UseVisualStyleBackColor = true;
            this.bShowDetails.Click += new System.EventHandler(this.ButtonShowDetails_Click);
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
            this.lCopyright.Size = new System.Drawing.Size(23, 13);
            this.lCopyright.TabIndex = 34;
            this.lCopyright.Text = "    ";
            this.lCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // eLicense
            // 
            this.eLicense.AutoSize = true;
            this.eLicense.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.eLicense.Location = new System.Drawing.Point(93, 72);
            this.eLicense.Name = "eLicense";
            this.eLicense.Size = new System.Drawing.Size(16, 17);
            this.eLicense.TabIndex = 33;
            this.eLicense.Text = "    ";
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
            this.lLicense.Size = new System.Drawing.Size(23, 13);
            this.lLicense.TabIndex = 31;
            this.lLicense.Text = "    ";
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
            this.lVersion.Size = new System.Drawing.Size(23, 13);
            this.lVersion.TabIndex = 19;
            this.lVersion.Text = "    ";
            this.lVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // eCopyright
            // 
            this.eCopyright.AutoSize = true;
            this.eCopyright.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.eCopyright.Location = new System.Drawing.Point(93, 112);
            this.eCopyright.Name = "eCopyright";
            this.eCopyright.Size = new System.Drawing.Size(19, 13);
            this.eCopyright.TabIndex = 28;
            this.eCopyright.Text = "    ";
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Image = Resources.LanExchange_48x48;
            this.logoPictureBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.logoPictureBox.Location = new System.Drawing.Point(16, 16);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(48, 48);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 13;
            this.logoPictureBox.TabStop = false;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bClose;
            this.ClientSize = new System.Drawing.Size(384, 242);
            this.Controls.Add(this.eWeb);
            this.Controls.Add(this.lWeb);
            this.Controls.Add(this.eVersion);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.bShowDetails);
            this.Controls.Add(this.lCopyright);
            this.Controls.Add(this.eLicense);
            this.Controls.Add(this.lLicense);
            this.Controls.Add(this.lVersion);
            this.Controls.Add(this.eCopyright);
            this.Controls.Add(this.logoPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.RightToLeftChanged += new System.EventHandler(this.AboutForm_RightToLeftChanged);
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
        private System.Windows.Forms.Button bShowDetails;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Label eLicense;
        private System.Windows.Forms.Label eVersion;
        private System.Windows.Forms.Label lWeb;
        private System.Windows.Forms.LinkLabel eWeb;
        private System.Windows.Forms.ToolTip tipAbout;
    }
}
