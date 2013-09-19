using System;
using System.Windows.Forms;

namespace WMIViewer
{
    public partial class WMIEditProperty : Form, IWMIView
    {
        private WMIPresenter m_Presenter;

        public WMIEditProperty(WMIPresenter presenter)
        {
            m_Presenter = presenter;
            InitializeComponent();
        }

        public ListView LV
        {
            get { return null; }
        }

        public ContextMenuStrip MENU
        {
            get { return null; }
        }
    }
}
