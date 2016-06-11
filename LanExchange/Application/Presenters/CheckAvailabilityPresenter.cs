using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Threading;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Presenters
{
    internal sealed class CheckAvailabilityPresenter : PresenterBase<ICheckAvailabilityWindow>, ICheckAvailabilityPresenter
    {
        private const int DELAY_FOR_SHOW = 250;

        private readonly IImageManager imageManager;
        private Thread thread;
        private volatile bool doneAndAvailable;

        public CheckAvailabilityPresenter(IImageManager imageManager)
        {
            Contract.Requires<ArgumentNullException>(imageManager != null);
            this.imageManager = imageManager;
        }

        protected override void InitializePresenter()
        {
            thread = new Thread(ThreadProc);
            View.ViewClosed += ViewOnViewClosed;
        }

        private void ViewOnViewClosed(object sender, EventArgs e)
        {
            thread.Abort();
        }

        private void ThreadProc(object arg)
        {
            if (View.AvailabilityChecker == null || arg == null)
                return;

            //var panelItem = arg as PanelItemBase;
            //if (panelItem == null) return;

            bool available = false;
            while (!available)
            {
                try
                {
                    //Thread.Sleep(10000);
                    available = View.AvailabilityChecker();
                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }
            }
            doneAndAvailable = true;
            View.RunAction?.Invoke();
            View.InvokeClose();
        }

        public void OnCurrentItemChanged()
        {
            // TODO hide model
            //var currentItem = View.CurrentItem;
            //if (currentItem == null) return;

            //View.ObjectImage = imageManager.GetSmallImage(currentItem.ImageName);
            //View.ObjectText = currentItem.Name;
            //View.Icon = imageManager.GetSmallIcon(currentItem.ImageName);
            //View.SetToolTip(currentItem.FullName);
        }

        public void StartChecking()
        {
            // TODO hide model
            //thread.Start(View.CurrentItem);
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
                View.Visible = true;
        }

        public void PerformOk()
        {
            View.Close();
            if (!doneAndAvailable && View.RunAction != null)
                View.RunAction();
        }

        public void PerformCancel()
        {
            View.Close();
        }
    }
}
