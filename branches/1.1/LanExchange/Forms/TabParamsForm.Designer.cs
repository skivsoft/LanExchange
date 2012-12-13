using System.Windows.Forms;
namespace LanExchange.Forms
{
    partial class TabParamsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvDomains = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rbDontScan = new System.Windows.Forms.RadioButton();
            this.rbSelected = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvDomains);
            this.groupBox1.Controls.Add(this.rbDontScan);
            this.groupBox1.Controls.Add(this.rbSelected);
            this.groupBox1.Controls.Add(this.rbAll);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 299);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Сканирование сети";
            // 
            // lvDomains
            // 
            this.lvDomains.CheckBoxes = true;
            this.lvDomains.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvDomains.FullRowSelect = true;
            this.lvDomains.GridLines = true;
            this.lvDomains.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvDomains.HideSelection = false;
            this.lvDomains.Location = new System.Drawing.Point(17, 88);
            this.lvDomains.MultiSelect = false;
            this.lvDomains.Name = "lvDomains";
            this.lvDomains.Size = new System.Drawing.Size(163, 197);
            this.lvDomains.TabIndex = 3;
            this.lvDomains.UseCompatibleStateImageBehavior = false;
            this.lvDomains.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Группа";
            this.columnHeader1.Width = 140;
            // 
            // rbDontScan
            // 
            this.rbDontScan.AutoSize = true;
            this.rbDontScan.Checked = true;
            this.rbDontScan.Location = new System.Drawing.Point(15, 19);
            this.rbDontScan.Name = "rbDontScan";
            this.rbDontScan.Size = new System.Drawing.Size(155, 17);
            this.rbDontScan.TabIndex = 2;
            this.rbDontScan.TabStop = true;
            this.rbDontScan.Text = "Отключить сканирование";
            this.rbDontScan.UseVisualStyleBackColor = true;
            this.rbDontScan.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // rbSelected
            // 
            this.rbSelected.AutoSize = true;
            this.rbSelected.Location = new System.Drawing.Point(15, 65);
            this.rbSelected.Name = "rbSelected";
            this.rbSelected.Size = new System.Drawing.Size(123, 17);
            this.rbSelected.TabIndex = 1;
            this.rbSelected.Text = "Выбранные группы";
            this.rbSelected.UseVisualStyleBackColor = true;
            this.rbSelected.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(15, 42);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(70, 17);
            this.rbAll.TabIndex = 0;
            this.rbAll.Text = "Вся сеть";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(236, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(236, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // TabParamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 323);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TabParamsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройка вкладки";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TabParamsForm_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbDontScan;
        private System.Windows.Forms.RadioButton rbSelected;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private ListView lvDomains;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}