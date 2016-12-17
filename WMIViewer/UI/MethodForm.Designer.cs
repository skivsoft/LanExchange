using WMIViewer.Properties;

namespace WMIViewer.UI
{
    internal partial class MethodForm
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
            this.components = new System.ComponentModel.Container();
            this.pTop = new System.Windows.Forms.Panel();
            this.lDescription = new System.Windows.Forms.Label();
            this.lMethodName = new System.Windows.Forms.Label();
            this.LB = new System.Windows.Forms.ListBox();
            this.bRun = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pResult = new System.Windows.Forms.Panel();
            this.lResult = new System.Windows.Forms.Label();
            this.timerOK = new System.Windows.Forms.Timer(this.components);
            this.pTop.SuspendLayout();
            this.pResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.White;
            this.pTop.Controls.Add(this.lDescription);
            this.pTop.Controls.Add(this.lMethodName);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Left;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(250, 430);
            this.pTop.TabIndex = 6;
            // 
            // lDescription
            // 
            this.lDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lDescription.Location = new System.Drawing.Point(16, 33);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(222, 387);
            this.lDescription.TabIndex = 1;
            this.lDescription.Text = Resources.EmptyText;
            // 
            // lMethodName
            // 
            this.lMethodName.AutoSize = true;
            this.lMethodName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lMethodName.Location = new System.Drawing.Point(8, 12);
            this.lMethodName.Name = "lMethodName";
            this.lMethodName.Size = new System.Drawing.Size(24, 16);
            this.lMethodName.TabIndex = 0;
            this.lMethodName.Text = Resources.EmptyText;
            // 
            // LB
            // 
            this.LB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LB.FormattingEnabled = true;
            this.LB.Location = new System.Drawing.Point(256, 3);
            this.LB.Name = "LB";
            this.LB.Size = new System.Drawing.Size(531, 368);
            this.LB.TabIndex = 8;
            // 
            // bRun
            // 
            this.bRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bRun.Location = new System.Drawing.Point(581, 380);
            this.bRun.Name = "bRun";
            this.bRun.Size = new System.Drawing.Size(100, 40);
            this.bRun.TabIndex = 9;
            this.bRun.Text = Resources.MethodForm_bRun;
            this.bRun.UseVisualStyleBackColor = true;
            this.bRun.Click += new System.EventHandler(this.ButtonRun_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(687, 380);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(100, 40);
            this.bCancel.TabIndex = 10;
            this.bCancel.Text = Resources.ButtonCancel;
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 430);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(794, 22);
            this.statusStrip1.TabIndex = 11;
            // 
            // pResult
            // 
            this.pResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pResult.BackColor = System.Drawing.Color.Green;
            this.pResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pResult.Controls.Add(this.lResult);
            this.pResult.Location = new System.Drawing.Point(256, 380);
            this.pResult.Name = "pResult";
            this.pResult.Size = new System.Drawing.Size(319, 40);
            this.pResult.TabIndex = 12;
            // 
            // lResult
            // 
            this.lResult.BackColor = System.Drawing.SystemColors.Control;
            this.lResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lResult.Location = new System.Drawing.Point(0, 0);
            this.lResult.Name = "lResult";
            this.lResult.Size = new System.Drawing.Size(315, 36);
            this.lResult.TabIndex = 0;
            this.lResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerOK
            // 
            this.timerOK.Interval = 1000;
            this.timerOK.Tick += new System.EventHandler(this.TimerOK_Tick);
            // 
            // WMIMethodForm
            // 
            this.AcceptButton = this.bRun;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(794, 452);
            this.Controls.Add(this.pResult);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bRun);
            this.Controls.Add(this.LB);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.statusStrip1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WMIMethodForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = Resources.MethodForm_Caption;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WMIMethodForm_KeyDown);
            this.pTop.ResumeLayout(false);
            this.pTop.PerformLayout();
            this.pResult.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Label lMethodName;
        private System.Windows.Forms.ListBox LB;
        private System.Windows.Forms.Button bRun;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label lDescription;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel pResult;
        private System.Windows.Forms.Label lResult;
        private System.Windows.Forms.Timer timerOK;
    }
}