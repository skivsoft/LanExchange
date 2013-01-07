using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GongSolutions.Shell;
using GongSolutions.Shell.Interop;
using System.Runtime.InteropServices;

namespace LanExchange.View.Forms
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
            Rect.Size = new Size(this.Width, this.Height);
            Rect.Location = new Point(Screen.PrimaryScreen.WorkingArea.Left + (Screen.PrimaryScreen.WorkingArea.Width - Rect.Width),
                                      Screen.PrimaryScreen.WorkingArea.Top + (Screen.PrimaryScreen.WorkingArea.Height - Rect.Height));
            this.SetBounds(Rect.X, Rect.Y, Rect.Width, Rect.Height);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetupForm();
            treeView1.ExpandAll();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void picLogo_Paint(object sender, PaintEventArgs e)
        {
            /*
            IntPtr tmpPidl;
            SHFILEINFO shinfo = new SHFILEINFO();
            Shell32.SHGetSpecialFolderLocation(IntPtr.Zero, CSIDL.DRIVES, out tmpPidl);
            try
            {
                Shell32.SHGetFileInfo(tmpPidl, 0, out shinfo, Marshal.SizeOf(shinfo), SHGFI.PIDL | SHGFI.DISPLAYNAME | SHGFI.ICON | SHGFI.SMALLICON);
            }
            finally
            {
                Marshal.FreeCoTaskMem(tmpPidl);
            }

            SystemImageList.DrawSmallImage(e.Graphics, new Point(0, 0), shinfo.iIcon, false);
             */
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void менюToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}
