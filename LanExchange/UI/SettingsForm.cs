using System;
using System.Windows.Forms;
using LanExchange.Presenter;
using LanExchange.SDK;

namespace LanExchange.UI
{
    public partial class SettingsForm : EscapeForm, ISettingsView
    {
        /// <summary>
        /// This field for external use.
        /// </summary>
        public static SettingsForm Instance;

        private readonly ParamsPresenter m_Presenter;

        public SettingsForm()
        {
            InitializeComponent();
            m_Presenter = new ParamsPresenter(this);
            m_Presenter.LoadFromModel();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            m_Presenter.SaveToModel();
            Close();
        }

        public bool IsAutoRun
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

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Instance = null;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
