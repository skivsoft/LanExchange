using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.SDK;
using System.Diagnostics.Contracts;

namespace LanExchange.Plugin.WinForms.Components
{
    public partial class InfoView : UserControl, IInfoView
    {
        private readonly IImageManager imageManager;

        private int numLines;
        private readonly IList<Label> lines;
        private PanelItemBase currentItem;
        
        public InfoView(IImageManager imageManager)
        {
            Contract.Requires<ArgumentNullException>(imageManager != null);

            this.imageManager = imageManager;

            InitializeComponent();
            lines = new List<Label>();
        }

        public PictureBox Picture
        {
            get { return imgInfo; }
        }

        public int NumLines
        {
            get { return numLines; }
            set
            {
                numLines = value;
                Height = GetLocationY(numLines) + 8;
                for (int i = lines.Count; i < numLines; i++ )
                {
                    var control = CreateLabelControl(i);
                    Controls.Add(control);
                    lines.Add(control);
                }
                for (int i = lines.Count - 1; i > numLines - 1; i--)
                {
                    var label = lines[i];
                    lines.RemoveAt(i);
                    label.Dispose();
                }
            }
        }

        private int GetLocationX()
        {
            return RightToLeft == RightToLeft.No ? 50 : Width - 50;
        }

        private static int GetLocationY(int index)
        {
            return 8 + index * 16;
        }

        public string GetLine(int index)
        {
            if (index < 0 || index > numLines - 1)
                return string.Empty;
            return lines[index].Text;
        }

        public void SetLine(int index, string text)
        {
            if (index < 0 || index > numLines - 1)
                return;
            lines[index].Text = text;
        }

        private Label CreateLabelControl(int index)
        {
            var control = new Label();
            control.AutoSize = true;
            control.Location = new Point(GetLocationX(), GetLocationY(index));
            if (index == 0)
                control.Font = new Font(control.Font, FontStyle.Bold);
            control.MouseDown += ControlOnMouseDown;
            return control;
        }

        /// <summary>
        /// Drag label text from InfoView and drop into external application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlOnMouseDown(object sender, MouseEventArgs e)
        {
            var label = sender as Label;
            if (label != null && e.Button == MouseButtons.Left)
            {
                var obj = new DataObject();
                obj.SetText(label.Text, TextDataFormat.UnicodeText);
                DoDragDrop(obj, DragDropEffects.Copy);
            }
        }

        public PanelItemBase CurrentItem
        {
            get { return currentItem; }
            set
            {
                currentItem = value;
                if (currentItem != null)
                {
                    Picture.Image = imageManager.GetLargeImage(currentItem.ImageName);
                    App.MainView.SetToolTip(Picture, currentItem.ImageLegendText);
                }
            }
        }

        private void InfoView_RightToLeftChanged(object sender, EventArgs e)
        {
            //for (int index = 0; index < m_Lines.Count - 1; index++ )
            //{
            //    m_Lines[index].RightToLeft = RightToLeft;
            //    m_Lines[index].Location = new Point(GetLocationX(), GetLocationY(index));
            //}
        }
    }
}
