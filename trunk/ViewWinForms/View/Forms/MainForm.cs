using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ViewWinForms.View.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void SetupForm()
        {
            //TrayIcon.Visible = CanUseTray();
            // размещаем форму внизу справа
            Rectangle Rect = new Rectangle();
            Rect.Size = new Size(450, Screen.PrimaryScreen.WorkingArea.Height);
            Rect.Location = new Point(Screen.PrimaryScreen.WorkingArea.Left + (Screen.PrimaryScreen.WorkingArea.Width - Rect.Width),
                                      Screen.PrimaryScreen.WorkingArea.Top + (Screen.PrimaryScreen.WorkingArea.Height - Rect.Height));
            this.SetBounds(Rect.X, Rect.Y, Rect.Width, Rect.Height);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetupForm();
        }
    }
}
