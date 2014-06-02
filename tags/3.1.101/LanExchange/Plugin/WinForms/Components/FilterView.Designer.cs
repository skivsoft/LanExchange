using System.Windows.Forms;
using LanExchange.Properties;

namespace LanExchange.Plugin.WinForms.Components
{
    partial class FilterView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private readonly System.ComponentModel.IContainer components = null;

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
            this.imgClear = new System.Windows.Forms.PictureBox();
            this.eFilter = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgClear)).BeginInit();
            this.SuspendLayout();
            // 
            // imgClear
            // 
            this.imgClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgClear.BackColor = System.Drawing.Color.Transparent;
            this.imgClear.Image = Resources.clear_normal;
            this.imgClear.Location = new System.Drawing.Point(447, 7);
            this.imgClear.Name = "imgClear";
            this.imgClear.Size = new System.Drawing.Size(16, 16);
            this.imgClear.TabIndex = 8;
            this.imgClear.TabStop = false;
            this.imgClear.Click += new System.EventHandler(this.imgClear_Click);
            this.imgClear.MouseLeave += new System.EventHandler(this.imgClear_MouseLeave);
            this.imgClear.MouseHover += new System.EventHandler(this.imgClear_MouseHover);
            // 
            // eFilter
            // 
            this.eFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eFilter.Location = new System.Drawing.Point(8, 5);
            this.eFilter.Name = "eFilter";
            this.eFilter.Size = new System.Drawing.Size(436, 20);
            this.eFilter.TabIndex = 9;
            this.eFilter.TextChanged += new System.EventHandler(this.eFilter_TextChanged);
            this.eFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.eFilter_KeyDown);
            // 
            // FilterView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.eFilter);
            this.Controls.Add(this.imgClear);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "FilterView";
            this.Size = new System.Drawing.Size(470, 30);
            ((System.ComponentModel.ISupportInitialize)(this.imgClear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox imgClear;
        public TextBox eFilter;
    }
}
