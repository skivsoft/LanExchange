using System;
using System.Diagnostics;
using System.Threading;
using LanExchange.SDK;
using System.Diagnostics.Contracts;

namespace LanExchange.Misc.Impl
{
    internal class PanelUpdaterImpl : IPanelUpdater
    {
        private readonly IImageManager imageManager;
        private readonly IPagesPresenter pagesPresenter;

        private const int NODISPLAY_DELAY = 500;
        private Thread setTabImageThread;
        private Thread updateThread;
        private bool isClearFilter;

        public PanelUpdaterImpl(
            IImageManager imageManager,
            IPagesPresenter pagesPresenter)
        {
            Contract.Requires<ArgumentNullException>(imageManager != null);
            Contract.Requires<ArgumentNullException>(pagesPresenter != null);

            this.imageManager = imageManager;
            this.pagesPresenter = pagesPresenter;
        }

        public void Dispose()
        {
            Stop();
        }

        public void Stop()
        {
            if (setTabImageThread != null)
            {
                setTabImageThread.Abort();
                setTabImageThread = null;
            }
            if (updateThread != null)
            {
                updateThread.Abort();
                updateThread = null;
            }
        }

        public void Start(IPanelModel model, bool clearFilter)
        {
            setTabImageThread = new Thread(SetTabImageThread);
            updateThread = new Thread(UpdateThread);
            isClearFilter = clearFilter;
            setTabImageThread.Start(model);
            updateThread.Start(model);
        }

        private void SetTabImageThread(object argument)
        {
            var model = argument as IPanelModel;
            if (model == null) return;
            var helper = new AnimationHelper(AnimationHelper.WORKING);
            try
            {
                var count = 0;
                while (true)
                {
                    if (count >= NODISPLAY_DELAY / AnimationHelper.DELAY)
                    {
                        var imageName = helper.GetNextImageName();
                        App.MainView.SafeInvoke(new SetTabImageDelegate(SetTabImageInvoked), model, imageName);
                    }
                    Thread.Sleep(AnimationHelper.DELAY);
                    count++;
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }
            App.MainView.SafeInvoke(new SetTabImageDelegate(SetTabImageInvoked), model, model.ImageName);
        }

        private void SetTabImageInvoked(IPanelModel model, string imageName)
        {
            if (model == null) return;
            var index = pagesPresenter.IndexOf(model);
            if (index != -1)
                pagesPresenter.View.SetTabImage(index, imageManager.IndexOf(imageName));
        }

        private void UpdateThread(object argument)
        {
            var model = argument as IPanelModel;
            if (model == null) return;
            var fillerResult = model.RetrieveData(RetrieveMode.Async, isClearFilter);
            App.MainView.SafeInvoke(new SetFillerResultDelegate(UpdateThreadInvoked), model, fillerResult);
            setTabImageThread.Interrupt();
        }

        private void UpdateThreadInvoked(IPanelModel model, PanelFillerResult fillerResult)
        {
            if (model != null)
                model.SetFillerResult(fillerResult, isClearFilter);
        }
    }
}
