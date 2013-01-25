using System;
using System.Windows.Forms;
using LanExchange.Interface;
using LanExchange.Presenter;

namespace LanExchange.UI
{
    public partial class ParamsForm : Form, IParamsView
    {
        /// <summary>
        /// This field for external use.
        /// </summary>
        public static ParamsForm Instance;

        private readonly ParamsPresenter m_Presenter;

        public ParamsForm()
        {
            InitializeComponent();
            m_Presenter = new ParamsPresenter(this);
            m_Presenter.LoadFromModel();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            m_Presenter.SaveToModel();
        }

        private void ParamsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                e.Handled = true;
            }
        }

        public bool IsAutorun
        {
            get { return chAutorun.Checked; }
            set { chAutorun.Checked = value; }
        }

        public bool RunMinimized
        {
            get { return chMinimized.Checked; }
            set { chMinimized.Checked = value; }
        }

        public bool AdvancedMode
        {
            get { return chAdvanced.Checked; }
            set { chAdvanced.Checked = value; }
        }

        public decimal RefreshTimeInMin
        {
            get { return eRefreshTime.Value; }
            set { eRefreshTime.Value = value; }
        }

        public bool ShowHiddenShares
        {
            get { return chShowHiddenShares.Checked; }
            set { chShowHiddenShares.Checked = value; }
        }
        
        public bool ShowPrinters 
        {
            get { return chShowPrinters.Checked; }
            set { chShowPrinters.Checked = value; }
        }
    }
}
