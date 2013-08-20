namespace WMIViewer
{
    partial class WMIAuthForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.picShield = new System.Windows.Forms.PictureBox();
            this.lMessage = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.eUserName = new System.Windows.Forms.TextBox();
            this.ePassword = new System.Windows.Forms.TextBox();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.chSavePassword = new System.Windows.Forms.CheckBox();
            this.Error = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picShield)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Error)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.picShield);
            this.panel1.Controls.Add(this.lMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 80);
            this.panel1.TabIndex = 0;
            // 
            // picShield
            // 
            this.picShield.Location = new System.Drawing.Point(12, 12);
            this.picShield.Name = "picShield";
            this.picShield.Size = new System.Drawing.Size(32, 32);
            this.picShield.TabIndex = 1;
            this.picShield.TabStop = false;
            // 
            // lMessage
            // 
            this.lMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lMessage.Location = new System.Drawing.Point(50, 12);
            this.lMessage.Name = "lMessage";
            this.lMessage.Size = new System.Drawing.Size(335, 56);
            this.lMessage.TabIndex = 0;
            this.lMessage.Text = "Не удалось подключиться под учётной записью {0}.\nУкажите имя пользователя и парол" +
    "ь для повторной попытки подключения.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Пользователь:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Пароль:";
            // 
            // eUserName
            // 
            this.eUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eUserName.Location = new System.Drawing.Point(139, 90);
            this.eUserName.Name = "eUserName";
            this.eUserName.Size = new System.Drawing.Size(231, 20);
            this.eUserName.TabIndex = 3;
            // 
            // ePassword
            // 
            this.ePassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ePassword.Location = new System.Drawing.Point(139, 117);
            this.ePassword.Name = "ePassword";
            this.ePassword.Size = new System.Drawing.Size(231, 20);
            this.ePassword.TabIndex = 4;
            this.ePassword.UseSystemPasswordChar = true;
            // 
            // bOK
            // 
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(174, 190);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(90, 23);
            this.bOK.TabIndex = 6;
            this.bOK.Text = "Подключиться";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(280, 190);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(90, 23);
            this.bCancel.TabIndex = 7;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // chSavePassword
            // 
            this.chSavePassword.AutoSize = true;
            this.chSavePassword.Location = new System.Drawing.Point(53, 152);
            this.chSavePassword.Name = "chSavePassword";
            this.chSavePassword.Size = new System.Drawing.Size(319, 17);
            this.chSavePassword.TabIndex = 5;
            this.chSavePassword.Text = "Сохранить учётные данные на время работы программы.";
            this.chSavePassword.UseVisualStyleBackColor = true;
            // 
            // Error
            // 
            this.Error.ContainerControl = this;
            // 
            // WMIAuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 225);
            this.Controls.Add(this.chSavePassword);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.ePassword);
            this.Controls.Add(this.eUserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WMIAuthForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WMI-подключение к \\\\{0}";
            this.Load += new System.EventHandler(this.WMIAuthForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WMIAuthForm_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picShield)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Error)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picShield;
        private System.Windows.Forms.Label lMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox eUserName;
        private System.Windows.Forms.TextBox ePassword;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.CheckBox chSavePassword;
        private System.Windows.Forms.ErrorProvider Error;
    }
}