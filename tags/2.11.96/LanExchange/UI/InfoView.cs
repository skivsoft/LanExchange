using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Intf;
using LanExchange.SDK;

namespace LanExchange.UI
{
    public partial class InfoView : UserControl, IInfoView
    {
        private int m_NumLines;
        private readonly Dictionary<int, Label> m_Lines;
        
        public InfoView()
        {
            InitializeComponent();
            m_Lines = new Dictionary<int, Label>();
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
                Visible = m_NumLines >= 3;
            }
        }

        private int GetLocationY(int index)
        {
            return 8 + index * 16;
        }

        public string GetLine(int index)
        {
            if (index < 0 || index > m_NumLines - 1)
                return string.Empty;
            Label control;
            if (m_Lines.TryGetValue(index, out control))
                return control.Text;
            return string.Empty;
        }

        private Label CreateLabelControl(int index)
        {
            var control = new Label();
            control.AutoSize = true;
            control.Location = new Point(50, GetLocationY(index));
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

        public void SetLine(int index, string text)
        {
            if (index < 0 || index > m_NumLines - 1)
                return;
            Label control;
            if (!m_Lines.TryGetValue(index, out control))
            {
                control = CreateLabelControl(index);
                Controls.Add(control);
                m_Lines.Add(index, control);
            }
            control.Text = text;
        }

        public PanelItemBase CurrentItem { get; set; }
    }
}
