using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Presentation.WinForms.Controls
{
    internal sealed partial class InfoPanel : UserControl, IInfoView
    {
        private readonly IInfoPresenter presenter;

        private int numLines;
        private readonly IList<Label> lines;
        
        public InfoPanel(IInfoPresenter presenter)
        {
            if (presenter != null) throw new ArgumentNullException(nameof(presenter));

            InitializeComponent();
            this.presenter = presenter;
            presenter.Initialize(this);

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

        //private void popTop_Opening(object sender, CancelEventArgs e)
        //{
        //    var pv = Pages.ActivePanelView as PanelView;
        //    if (pv == null || pInfo.CurrentItem == null)
        //    {
        //        e.Cancel = true;
        //        return;
        //    }
        //    e.Cancel = !addonManager.BuildMenuForPanelItemType(popTop, pInfo.CurrentItem.GetType().Name);
        //    addonManager.SetupMenuForPanelItem(popTop, pInfo.CurrentItem);
        //}

        //public void ClearInfoPanel()
        //{
        //    pInfo.CurrentItem = null;
        //    pInfo.Picture.Image = null;
        //    for (int index = 0; index < pInfo.NumLines; index++)
        //        pInfo.SetLine(index, string.Empty);
        //    lItemsCount.Text = string.Empty;
        //}

        //private void FocusedItemChanged()
        //{
        //    View.Info.CurrentItem = panelItem;
        //    View.Info.NumLines = App.Config.NumInfoLines;
        //    var helper = new PanelModelCopyHelper(null, columnManager);
        //    helper.CurrentItem = panelItem;
        //    int index = 0;
        //    foreach (var column in helper.Columns)
        //    {
        //        View.Info.SetLine(index, helper.GetColumnValue(column.Index));
        //        ++index;
        //        if (index >= View.Info.NumLines) break;
        //    }
        //    for (int i = index; i < View.Info.NumLines; i++)
        //        View.Info.SetLine(i, string.Empty);
        //}

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Ctrl+Up/Ctrl+Down - change number of info lines
            if (e.Control && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                NumLines = NumLines + (e.KeyCode == Keys.Down ? +1 : -1);
                e.Handled = true;
            }
        }
    }
}
