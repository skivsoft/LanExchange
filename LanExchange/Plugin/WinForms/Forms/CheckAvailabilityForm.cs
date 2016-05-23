using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.Plugin.WinForms.Forms
{
    public partial class CheckAvailabilityForm : EscapeForm, ICheckAvailabilityWindow
    {
        private const int DELAY_FOR_SHOW = 250;

        private readonly IImageManager imageManager;
        private PanelItemBase currentItem;
        private readonly Thread thread;
        private volatile bool doneAndAvailable;

        public event EventHandler ViewClosed;

        public CheckAvailabilityForm(IImageManager imageManager)
        {
            Contract.Requires<ArgumentNullException>(imageManager != null);

            this.imageManager = imageManager;

            InitializeComponent();
            thread = new Thread(ThreadProc);
        }

        public PanelItemBase CurrentItem
        {
            get { return currentItem; }
            set
            {
                currentItem = value;
                if (currentItem != null)
                {
                    picObject.Image = imageManager.GetSmallImage(currentItem.ImageName);
                    Icon = imageManager.GetSmallIcon(currentItem.ImageName);
                    lObject.Text = currentItem.Name;
                    toolTip.SetToolTip(lObject, currentItem.FullName);
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
            thread.Start(currentItem);
        }

        public void WaitAndShow()
        {
            var startTime = DateTime.UtcNow;
            TimeSpan delta;
            do
            {
                delta = DateTime.UtcNow - startTime;
            } while (!doneAndAvailable && delta.Milliseconds < DELAY_FOR_SHOW);
            if (!doneAndAvailable)
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
            doneAndAvailable = true;
            if (RunAction != null)
                RunAction();
            if (Visible)
                Invoke(new Action(Close));
        }

        private void bRun_Click(object sender, EventArgs e)
        {
            Close();
            if (!doneAndAvailable && RunAction != null)
                RunAction();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CheckAvailabilityForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread.Abort();
        }


        public bool DoneAndAvailable
        {
            get { return doneAndAvailable; }
       }


        public object CallerControl { get; set; }
    }
}
