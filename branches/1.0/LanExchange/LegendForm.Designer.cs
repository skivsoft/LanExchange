namespace LanExchange
{
    partial class LegendForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Компьютер найден в результате обзора сети.", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Компьютер не доступен посредством PING.", 5);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Компьютер с запущенной программой LanExchange.", 4);
            this.lvLegend = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // lvLegend
            // 
            this.lvLegend.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvLegend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLegend.FullRowSelect = true;
            this.lvLegend.GridLines = true;
            this.lvLegend.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.lvLegend.Location = new System.Drawing.Point(0, 0);
            this.lvLegend.Name = "lvLegend";
            this.lvLegend.Size = new System.Drawing.Size(364, 164);
            this.lvLegend.TabIndex = 0;
            this.lvLegend.UseCompatibleStateImageBehavior = false;
            this.lvLegend.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Описание";
            this.columnHeader1.Width = 350;
            // 
            // LegendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 164);
            this.Controls.Add(this.lvLegend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LegendForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Легенда";
            this.Load += new System.EventHandler(this.LegendForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LegendForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvLegend;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}