namespace WMIViewer
{
    partial class WMISetupForm
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
            this.lDescription = new System.Windows.Forms.Label();
            this.lClassName = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.bDefault = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LB1 = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LB2 = new System.Windows.Forms.ListBox();
            this.pTop.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.White;
            this.pTop.Controls.Add(this.lDescription);
            this.pTop.Controls.Add(this.lClassName);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(694, 100);
            this.pTop.TabIndex = 6;
            // 
            // lDescription
            // 
            this.lDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lDescription.Location = new System.Drawing.Point(12, 37);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(670, 49);
            this.lDescription.TabIndex = 1;
            this.lDescription.Text = "    ";
            // 
            // lClassName
            // 
            this.lClassName.AutoSize = true;
            this.lClassName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lClassName.Location = new System.Drawing.Point(12, 12);
            this.lClassName.Name = "lClassName";
            this.lClassName.Size = new System.Drawing.Size(24, 16);
            this.lClassName.TabIndex = 0;
            this.lClassName.Text = "    ";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(492, 442);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(592, 442);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // bDefault
            // 
            this.bDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bDefault.Location = new System.Drawing.Point(8, 442);
            this.bDefault.Name = "bDefault";
            this.bDefault.Size = new System.Drawing.Size(90, 23);
            this.bDefault.TabIndex = 10;
            this.bDefault.Text = "Reset to default";
            this.bDefault.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LB1);
            this.groupBox1.Location = new System.Drawing.Point(8, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 320);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available classes";
            // 
            // LB1
            // 
            this.LB1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LB1.FormattingEnabled = true;
            this.LB1.Location = new System.Drawing.Point(3, 16);
            this.LB1.Name = "LB1";
            this.LB1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LB1.Size = new System.Drawing.Size(304, 301);
            this.LB1.TabIndex = 0;
            this.LB1.SelectedIndexChanged += new System.EventHandler(this.LB_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LB2);
            this.groupBox2.Location = new System.Drawing.Point(372, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 320);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected classes";
            // 
            // LB2
            // 
            this.LB2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LB2.FormattingEnabled = true;
            this.LB2.Location = new System.Drawing.Point(3, 16);
            this.LB2.Name = "LB2";
            this.LB2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LB2.Size = new System.Drawing.Size(304, 301);
            this.LB2.TabIndex = 0;
            this.LB2.SelectedIndexChanged += new System.EventHandler(this.LB_SelectedIndexChanged);
            // 
            // WMISetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 472);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bDefault);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "WMISetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select WMI Classes";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WMISetupForm_KeyDown);
            this.pTop.ResumeLayout(false);
            this.pTop.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Label lDescription;
        private System.Windows.Forms.Label lClassName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button bDefault;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox LB1;
        private System.Windows.Forms.ListBox LB2;
    }
}