using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SkivSoft.LanExchange
{
    public partial class MainForm : Form
    {
        public static TMainApp MainApp;

        public MainForm()
        {
            InitializeComponent();
        }

        public void ActivateFromNewInstance()
        {
            //F.bReActivate = true;
            //F.IsFormVisible = true;
            this.Activate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }


        private void SetupForm()
        {
            // размещаем форму внизу справа
            Rectangle Rect = new Rectangle();
            Rect.Size = new Size(450, Screen.PrimaryScreen.WorkingArea.Height);
            Rect.Location = new Point(Screen.PrimaryScreen.WorkingArea.Left + (Screen.PrimaryScreen.WorkingArea.Width - Rect.Width),
                                      Screen.PrimaryScreen.WorkingArea.Top + (Screen.PrimaryScreen.WorkingArea.Height - Rect.Height));
            this.SetBounds(Rect.X, Rect.Y, Rect.Width, Rect.Height);
            // выводим имя компьютера
            lCompName.Text = MainApp.ComputerName;
            // выводим имя пользователя
            lUserName.Text = MainApp.UserName;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (MainForm.MainApp == null)
                return;
            MainApp.LogPrint("MainForm load");
            SetupForm();

            //MainForm.MainApp.LogPrint("MainForm_Load");
            listView1.Items.Clear();
            foreach (var Pair in MainApp.plugins)
            {
                ListViewItem Item = new ListViewItem();
                Item.Text = Pair.Value.Name + " " + Pair.Value.Version;
                Item.SubItems.Add(Pair.Value.Author);
                Item.SubItems.Add(Pair.Value.Description);
                listView1.Items.Add(Item);
            }
        }
    }
}
