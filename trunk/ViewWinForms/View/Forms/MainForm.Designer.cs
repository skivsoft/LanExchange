using ViewWinForms.View.Components;

namespace ViewWinForms.View.Forms
{
    partial class MainForm
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
            this.pTop = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lCaption = new System.Windows.Forms.Label();
            this.lDesc = new System.Windows.Forms.Label();
            this.panelView1 = new ViewWinForms.View.Components.PanelView();
            this.statusView1 = new ViewWinForms.View.Components.StatusView();
            this.pTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.White;
            this.pTop.Controls.Add(this.picLogo);
            this.pTop.Controls.Add(this.lCaption);
            this.pTop.Controls.Add(this.lDesc);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(438, 90);
            this.pTop.TabIndex = 14;
            // 
            // picLogo
            // 
            this.picLogo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picLogo.Location = new System.Drawing.Point(16, 16);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(48, 48);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLogo.TabIndex = 2;
            this.picLogo.TabStop = false;
            // 
            // lCaption
            // 
            this.lCaption.AutoSize = true;
            this.lCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lCaption.ForeColor = System.Drawing.Color.Navy;
            this.lCaption.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lCaption.Location = new System.Drawing.Point(70, 12);
            this.lCaption.Name = "lCaption";
            this.lCaption.Size = new System.Drawing.Size(56, 18);
            this.lCaption.TabIndex = 0;
            this.lCaption.Text = "caption";
            // 
            // lDesc
            // 
            this.lDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lDesc.Location = new System.Drawing.Point(70, 36);
            this.lDesc.Name = "lDesc";
            this.lDesc.Size = new System.Drawing.Size(353, 51);
            this.lDesc.TabIndex = 1;
            this.lDesc.Text = "description";
            // 
            // panelView1
            // 
            this.panelView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelView1.Location = new System.Drawing.Point(0, 90);
            this.panelView1.Name = "panelView1";
            this.panelView1.Size = new System.Drawing.Size(438, 411);
            this.panelView1.TabIndex = 0;
            // 
            // statusView1
            // 
            this.statusView1.AutoSize = true;
            this.statusView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusView1.Location = new System.Drawing.Point(0, 501);
            this.statusView1.Name = "statusView1";
            this.statusView1.Size = new System.Drawing.Size(438, 22);
            this.statusView1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 523);
            this.Controls.Add(this.panelView1);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.statusView1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pTop.ResumeLayout(false);
            this.pTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public PanelView panelView1;
        public StatusView statusView1;
        private System.Windows.Forms.Panel pTop;
        public System.Windows.Forms.PictureBox picLogo;
        public System.Windows.Forms.Label lCaption;
        public System.Windows.Forms.Label lDesc;


    }
}