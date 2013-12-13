using System;
using System.Threading;
using LanExchange.SDK;

namespace LanExchange.Misc.Impl
{
    internal class PanelUpdaterImpl : IPanelUpdater
    {
        private const int NODISPLAY_DELAY = 500;
        private Thread m_SetTabImageThread;
        private Thread m_UpdateThread;
        private bool m_ClearFilter;

        public PanelUpdaterImpl()
        {
        }

        public void Dispose()
        {
            Stop();
        }

        public void Stop()
        {
            if (m_SetTabImageThread != null)
            {
                m_SetTabImageThread.Abort();
                m_SetTabImageThread = null;
            }
            if (m_UpdateThread != null)
            {
                m_UpdateThread.Abort();
                m_UpdateThread = null;
            }
        }

        public void Start(IPanelModel model, bool clearFilter)
        {
            m_SetTabImageThread = new Thread(SetTabImageThread);
            m_UpdateThread = new Thread(UpdateThread);
            m_SetTabImageThread.Start(model);
            m_ClearFilter = clearFilter;
            m_UpdateThread.Start(model);
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
                        App.MainPages.View.Invoke(new SetTabImageDelegate(SetTabImageInvoked), model, imageName);
                    }
                    Thread.Sleep(AnimationHelper.DELAY);
                    count++;
                }
            }
            catch (Exception)
            {
                
            }
            var parent = model.CurrentPath.Item[0];
            if (parent.Parent != null)
                parent = parent.Parent;
            App.MainPages.View.Invoke(new SetTabImageDelegate(SetTabImageInvoked), model, parent.ImageName);
        }

        private static void SetTabImageInvoked(IPanelModel model, string imageName)
        {
            if (model == null) return;
            var pagesPresenter = App.MainPages;
            var index = pagesPresenter.IndexOf(model);
            if (index != -1)
                lock (App.Images)
                {
                    pagesPresenter.View.SetTabImage(index, App.Images.IndexOf(imageName));
                }
        }

        private void UpdateThread(object argument)
        {
            var model = argument as IPanelModel;
            if (model == null) return;
            var fillerResult = model.RetrieveData(RetrieveMode.Async, m_ClearFilter);
            App.MainPages.View.Invoke(new SetFillerResultDelegate(UpdateThreadInvoked), model, fillerResult);
            m_SetTabImageThread.Interrupt();
        }

        private void UpdateThreadInvoked(IPanelModel model, PanelFillerResult fillerResult)
        {
            if (model != null)
                model.SetFillerResult(fillerResult, m_ClearFilter);
        }
    }
}
