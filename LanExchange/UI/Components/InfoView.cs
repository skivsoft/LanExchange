using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.UI.WinForms
{
    public partial class InfoView : UserControl, IInfoView
    {
        private int m_NumLines;
        private readonly IList<Label> m_Lines;
        private PanelItemBase m_CurrentItem;
        
        public InfoView()
        {
            InitializeComponent();
            m_Lines = new List<Label>();
        }

        public PictureBox Picture
        {
            get { return imgInfo; }
        }

        public int NumLines
        {
            get { return m_NumLines; }
            set
            {
                m_NumLines = value;
                Height = GetLocationY(m_NumLines) + 8;
                for (int i = m_Lines.Count; i < m_NumLines; i++ )
                {
                    var control = CreateLabelControl(i);
                    Controls.Add(control);
                    m_Lines.Add(control);
                }
                for (int i = m_Lines.Count - 1; i > m_NumLines - 1; i--)
                {
                    var label = m_Lines[i];
                    m_Lines.RemoveAt(i);
                    label.Dispose();
                }
            }
        }

        private int GetLocationX()
        {
            return RightToLeft == RightToLeft.No ? 50 : Width - 50;
        }

        private int GetLocationY(int index)
        {
            return 8 + index * 16;
        }

        public string GetLine(int index)
        {
            if (index < 0 || index > m_NumLines - 1)
                return string.Empty;
            return m_Lines[index].Text;
        }

        public void SetLine(int index, string text)
        {
            if (index < 0 || index > m_NumLines - 1)
                return;
            m_Lines[index].Text = text;
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
            get { return m_CurrentItem; }
            set
            {
                m_CurrentItem = value;
                if (m_CurrentItem != null)
                {
                    Picture.Image = App.Images.GetLargeImage(m_CurrentItem.ImageName);
                    App.MainView.SetToolTip(Picture, m_CurrentItem.ImageLegendText);
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
