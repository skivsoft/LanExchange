using System;
using System.ComponentModel;
using LanExchange.Base;
using LanExchange.SDK;

namespace LanExchange.Plugin.WinForms.Forms
{
    public partial class CheckAvailabilityForm : EscapeForm, ICheckAvailabilityView
    {
        private PanelItemBase m_CurrentItem;
        private AddonMenuItem m_MenuItem;

        public CheckAvailabilityForm()
        {
            InitializeComponent();
        }

        public PanelItemBase CurrentItem
        {
            get { return m_CurrentItem; }
            set
            {
                m_CurrentItem = value;
                if (m_CurrentItem != null)
                {
                    picObject.Image = App.Images.GetSmallImage(m_CurrentItem.ImageName);
                    Icon = App.Images.GetSmallIcon(m_CurrentItem.ImageName);
                    lObject.Text = m_CurrentItem.Name;
                    toolTip.SetToolTip(lObject, m_CurrentItem.FullName);
                }
            }
        }

        public AddonMenuItem MenuItem
        {
            get { return m_MenuItem; }
            set
            {
                m_MenuItem = value;
                bRun.Text = m_MenuItem.Text;
                if (m_MenuItem.ProgramValue != null)
                    bRun.Image = m_MenuItem.ProgramValue.ProgramImage;
            }
        }

        [Localizable(false)]
        public void PrepareForm()
        {
            Text = string.Format("{0} — {1}", m_CurrentItem.Name, m_MenuItem.Text);
        }

        private void bRun_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
