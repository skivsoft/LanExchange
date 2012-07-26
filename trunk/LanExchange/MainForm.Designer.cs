namespace LanExchange
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
            this.statusView1 = new LanExchange.View.Components.StatusView();
            this.panelView1 = new LanExchange.View.Components.PanelView();
            this.SuspendLayout();
            // 
            // statusView1
            // 
            this.statusView1.AutoSize = true;
            this.statusView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusView1.Location = new System.Drawing.Point(0, 501);
            this.statusView1.Name = "statusView1";
            this.statusView1.Size = new System.Drawing.Size(504, 22);
            this.statusView1.TabIndex = 1;
            // 
            // panelView1
            // 
            this.panelView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelView1.Location = new System.Drawing.Point(0, 0);
            this.panelView1.Name = "panelView1";
            this.panelView1.Size = new System.Drawing.Size(504, 501);
            this.panelView1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 523);
            this.Controls.Add(this.panelView1);
            this.Controls.Add(this.statusView1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public View.Components.PanelView panelView1;
        public View.Components.StatusView statusView1;


    }
}