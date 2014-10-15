using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.Plugin.WinForms.Forms
{
    public partial class CheckAvailabilityForm : EscapeForm, ICheckAvailabilityWindow
    {
        private const int DELAY_FOR_SHOW = 250;

        private readonly IImageManager m_ImageManager;
        private PanelItemBase m_CurrentItem;
        private readonly Thread m_Thread;
        private volatile bool m_DoneAndAvailable;

        public CheckAvailabilityForm(IImageManager imageManager)
        {
            InitializeComponent();

            m_ImageManager = imageManager;
            m_Thread = new Thread(ThreadProc);
        }

        public PanelItemBase CurrentItem
        {
            get { return m_CurrentItem; }
            set
            {
                m_CurrentItem = value;
                if (m_CurrentItem != null)
                {
                    picObject.Image = m_ImageManager.GetSmallImage(m_CurrentItem.ImageName);
                    Icon = m_ImageManager.GetSmallIcon(m_CurrentItem.ImageName);
                    lObject.Text = m_CurrentItem.Name;
                    toolTip.SetToolTip(lObject, m_CurrentItem.FullName);
                }
            }
        }

        public string RunText
        {
            get { return bRun.Text; }
            set { bRun.Text = value; }
        }

        public Image RunImage
        {
            get { return bRun.Image; }
            set { bRun.Image = value; }
        }

        public Action RunAction { get; set; }

        public Func<PanelItemBase, bool> AvailabilityChecker { get; set; }

        public void StartChecking()
        {
            m_Thread.Start(m_CurrentItem);
        }

        public void WaitAndShow()
        {
            var startTime = DateTime.UtcNow;
            TimeSpan delta;
            do
            {
                delta = DateTime.UtcNow - startTime;
            } while (!m_DoneAndAvailable && delta.Milliseconds < DELAY_FOR_SHOW);
            if (!m_DoneAndAvailable)
                Show();
        }

        private void ThreadProc(object arg)
        {
            if (AvailabilityChecker == null || arg == null)
                return;

            var panelItem = arg as PanelItemBase;
            if (panelItem == null) return;

            bool available = false;
            while (!available) 
            {
                try
                {
                    //Thread.Sleep(10000);
                    available = AvailabilityChecker(panelItem);
                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }
            }
            m_DoneAndAvailable = true;
            if (RunAction != null)
                RunAction();
            if (Visible)
                Invoke(new Action(Close));
        }

        private void bRun_Click(object sender, EventArgs e)
        {
            Close();
            if (!m_DoneAndAvailable && RunAction != null)
                RunAction();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CheckAvailabilityForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_Thread.Abort();
        }


        public bool DoneAndAvailable
        {
            get { return m_DoneAndAvailable; }
       }


        public object CallerControl { get; set; }
    }
}
