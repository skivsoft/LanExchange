using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.UI
{
    public partial class InfoView : UserControl, IInfoView
    {
        private int m_CountLines;
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

        public int CountLines
        {
            get { return m_CountLines; }
            set
            {
                m_CountLines = value;
                Height = GetLocationY(m_CountLines) + 8;
                Visible = m_CountLines >= 3;
            }
        }

        private int GetLocationY(int index)
        {
            return 8 + index * 16;
        }

        public string GetLine(int index)
        {
            if (index < 0 || index > m_CountLines - 1)
                return string.Empty;
            Label control;
            if (m_Lines.TryGetValue(index, out control))
                return control.Text;
            return string.Empty;
        }

        public void SetLine(int index, string text)
        {
            if (index < 0 || index > m_CountLines - 1)
                return;
            Label control;
            if (!m_Lines.TryGetValue(index, out control))
            {
                control = new Label();
                control.AutoSize = true;
                control.Location = new Point(50, GetLocationY(index));
                if (index == 0)
                    control.Font = new Font(control.Font, FontStyle.Bold);
                Controls.Add(control);
                m_Lines.Add(index, control);
            }
            control.Text = text;
        }
    }
}
