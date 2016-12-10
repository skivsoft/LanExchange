using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Presentation.WinForms.Forms
{
    internal sealed partial class EditForm : Form, IEditView
    {
        private readonly IEditPresenter presenter;

        public EditForm(IEditPresenter presenter)
        {
            this.presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));

            InitializeComponent();
            presenter.Initialize(this);
        }

        public event EventHandler ViewClosed;

        [Localizable(false)]
        public void SetColumns(IList<PanelColumnHeader> columns)
        {
            const int SPACE = 20;
            const int LINE_DELTA = 30;
            const int EDIT_WIDTH = 300;
            const int START_TABINDEX = 2;

            var top = SPACE;
            var maxWidth = 0;
            var labelHeight = 0;
            for (int index = 0; index < columns.Count; index++)
            {
                var label = new Label();
                label.AutoSize = true;
                label.UseMnemonic = true;
                label.Text = "&" + columns[index].Text + ":";
                label.TabIndex = START_TABINDEX + index*2;
                label.SetBounds(SPACE, top, 0, 0);
                Controls.Add(label);
                if (label.Width > maxWidth)
                {
                    maxWidth = label.Width;
                    labelHeight = label.Height;
                }
                top += LINE_DELTA;
            }
            Width = SPACE*3 + maxWidth + EDIT_WIDTH + SystemInformation.FixedFrameBorderSize.Width * 2;
            Height = top + 100;
            top = SPACE;
            for (int index = 0; index < columns.Count; index++)
            {
                var edit = new TextBox();
                edit.SetBounds(SPACE*2 + maxWidth, top-(20-labelHeight)/2, EDIT_WIDTH, 20);
                edit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                edit.TabIndex = START_TABINDEX + index * 2 + 1;
                Controls.Add(edit);
                if (ActiveControl == null)
                    ActiveControl = edit;
                top += LINE_DELTA;
            }
        }

        public bool ShowModalDialog()
        {
            return ShowDialog() == DialogResult.OK;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            presenter.PerformOk();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            presenter.PerformCancel();
        }

        private void EditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                presenter.PerformCancel();
                e.Handled = true;
            }
        }
    }
}