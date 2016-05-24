using LanExchange.SDK;
using System;
using System.Diagnostics.Contracts;
using System.Windows.Forms;

namespace LanExchange.Plugin.WinForms.Forms
{
    public partial class EscapeForm : Form
    {
        protected readonly ITranslationService translationService;

        public EscapeForm(ITranslationService translationService)
        {
            Contract.Requires<ArgumentNullException>(translationService != null);

            this.translationService = translationService;

            InitializeComponent();
            SetupRightToLeft();
        }

        private void SetupRightToLeft()
        {
            if (translationService.RightToLeft)
            {
                RightToLeftLayout = true;
                RightToLeft = RightToLeft.Yes;
            }
        }

        private void EscapeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
                e.Handled = true;
            }
        }
    }
}
