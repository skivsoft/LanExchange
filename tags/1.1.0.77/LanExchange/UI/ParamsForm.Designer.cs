namespace LanExchange.UI
{
    partial class ParamsForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chAdvanced = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.eRefreshTime = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chMinimized = new System.Windows.Forms.CheckBox();
            this.chAutorun = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bCancel = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.chShowHiddenShares = new System.Windows.Forms.CheckBox();
            this.chShowPrinters = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eRefreshTime)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(437, 267);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chShowPrinters);
            this.tabPage1.Controls.Add(this.chShowHiddenShares);
            this.tabPage1.Controls.Add(this.chAdvanced);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.eRefreshTime);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.chMinimized);
            this.tabPage1.Controls.Add(this.chAutorun);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(429, 241);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Общие";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chAdvanced
            // 
            this.chAdvanced.AutoSize = true;
            this.chAdvanced.Location = new System.Drawing.Point(18, 98);
            this.chAdvanced.Name = "chAdvanced";
            this.chAdvanced.Size = new System.Drawing.Size(288, 17);
            this.chAdvanced.TabIndex = 1;
            this.chAdvanced.Text = "Расширенный функционал для администрирования";
            this.chAdvanced.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(295, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "мин.";
            // 
            // eRefreshTime
            // 
            this.eRefreshTime.Location = new System.Drawing.Point(251, 122);
            this.eRefreshTime.Name = "eRefreshTime";
            this.eRefreshTime.Size = new System.Drawing.Size(40, 20);
            this.eRefreshTime.TabIndex = 2;
            this.eRefreshTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Интервал обновления списка компьютеров";
            // 
            // chMinimized
            // 
            this.chMinimized.AutoSize = true;
            this.chMinimized.Location = new System.Drawing.Point(18, 38);
            this.chMinimized.Name = "chMinimized";
            this.chMinimized.Size = new System.Drawing.Size(231, 17);
            this.chMinimized.TabIndex = 1;
            this.chMinimized.Text = "Запускать программу в свёрнутом виде";
            this.chMinimized.UseVisualStyleBackColor = true;
            // 
            // chAutorun
            // 
            this.chAutorun.AutoSize = true;
            this.chAutorun.Location = new System.Drawing.Point(18, 18);
            this.chAutorun.Name = "chAutorun";
            this.chAutorun.Size = new System.Drawing.Size(319, 17);
            this.chAutorun.TabIndex = 0;
            this.chAutorun.Text = "Запускать программу при входе пользователя в систему";
            this.chAutorun.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bCancel);
            this.panel1.Controls.Add(this.bSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 267);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(437, 40);
            this.panel1.TabIndex = 1;
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(349, 5);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 1;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bSave.Location = new System.Drawing.Point(259, 5);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(75, 23);
            this.bSave.TabIndex = 0;
            this.bSave.Text = "Сохранить";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // chShowHiddenShares
            // 
            this.chShowHiddenShares.AutoSize = true;
            this.chShowHiddenShares.Location = new System.Drawing.Point(18, 58);
            this.chShowHiddenShares.Name = "chShowHiddenShares";
            this.chShowHiddenShares.Size = new System.Drawing.Size(219, 17);
            this.chShowHiddenShares.TabIndex = 5;
            this.chShowHiddenShares.Text = "Показывать скрытые общие ресурсы";
            this.chShowHiddenShares.UseVisualStyleBackColor = true;
            // 
            // chShowPrinters
            // 
            this.chShowPrinters.AutoSize = true;
            this.chShowPrinters.Location = new System.Drawing.Point(18, 78);
            this.chShowPrinters.Name = "chShowPrinters";
            this.chShowPrinters.Size = new System.Drawing.Size(141, 17);
            this.chShowPrinters.TabIndex = 6;
            this.chShowPrinters.Text = "Показывать принтеры";
            this.chShowPrinters.UseVisualStyleBackColor = true;
            // 
            // ParamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 307);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParamsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки программы";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ParamsForm_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eRefreshTime)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.CheckBox chAutorun;
        private System.Windows.Forms.CheckBox chMinimized;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown eRefreshTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chAdvanced;
        private System.Windows.Forms.CheckBox chShowPrinters;
        private System.Windows.Forms.CheckBox chShowHiddenShares;
    }
}