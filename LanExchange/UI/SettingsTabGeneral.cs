using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.UI
{
    public partial class SettingsTabGeneral : UserControl
    {
        public SettingsTabGeneral()
        {
            InitializeComponent();
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
    }
}
