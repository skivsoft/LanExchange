using WMIViewer.Properties;

namespace WMIViewer
{
    partial class EditPropertyForm
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
            this.lProperty = new System.Windows.Forms.Label();
            this.eProp = new System.Windows.Forms.TextBox();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lClass = new System.Windows.Forms.Label();
            this.eClass = new System.Windows.Forms.TextBox();
            this.pTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.White;
            this.pTop.Controls.Add(this.lDescription);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(494, 80);
            this.pTop.TabIndex = 0;
            // 
            // lDescription
            // 
            this.lDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lDescription.Location = new System.Drawing.Point(12, 9);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(470, 57);
            this.lDescription.TabIndex = 1;
            this.lDescription.Text = Resources.EmptyText;
            // 
            // lProperty
            // 
            this.lProperty.AutoSize = true;
            this.lProperty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lProperty.Location = new System.Drawing.Point(12, 133);
            this.lProperty.Name = "lProperty";
            this.lProperty.Size = new System.Drawing.Size(33, 13);
            this.lProperty.TabIndex = 2;
            this.lProperty.Text = Resources.EmptyText;
            // 
            // eProp
            // 
            this.eProp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eProp.Location = new System.Drawing.Point(15, 150);
            this.eProp.Name = "eProp";
            this.eProp.Size = new System.Drawing.Size(467, 20);
            this.eProp.TabIndex = 3;
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(316, 186);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 4;
            this.bOK.Text = Resources.EditProperty_OK;
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(407, 186);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 5;
            this.bCancel.Text = Resources.EditProperty_Cancel;
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 1);
            this.panel1.TabIndex = 6;
            // 
            // lClass
            // 
            this.lClass.AutoSize = true;
            this.lClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lClass.Location = new System.Drawing.Point(12, 91);
            this.lClass.Name = "lClass";
            this.lClass.Size = new System.Drawing.Size(37, 13);
            this.lClass.TabIndex = 7;
            this.lClass.Text = Resources.EmptyText;
            // 
            // eClass
            // 
            this.eClass.Location = new System.Drawing.Point(15, 107);
            this.eClass.Name = "eClass";
            this.eClass.ReadOnly = true;
            this.eClass.Size = new System.Drawing.Size(467, 20);
            this.eClass.TabIndex = 8;
            // 
            // WMIEditProperty
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(494, 223);
            this.Controls.Add(this.eClass);
            this.Controls.Add(this.lClass);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.eProp);
            this.Controls.Add(this.lProperty);
            this.Controls.Add(this.pTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "WMIEditProperty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.pTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Label lDescription;
        private System.Windows.Forms.Label lProperty;
        private System.Windows.Forms.TextBox eProp;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lClass;
        private System.Windows.Forms.TextBox eClass;
    }
}