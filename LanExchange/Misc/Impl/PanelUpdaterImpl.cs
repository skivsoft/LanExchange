using System;
using System.Diagnostics;
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
            m_ClearFilter = clearFilter;
            m_SetTabImageThread.Start(model);
            m_UpdateThread.Start(model);
        }

        private static void SetTabImageThread(object argument)
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

        private static void SetTabImageInvoked(IPanelModel model, string imageName)
        {
            if (model == null) return;
            var pagesPresenter = App.MainPages;
            var index = pagesPresenter.IndexOf(model);
            if (index != -1)
                pagesPresenter.View.SetTabImage(index, App.Images.IndexOf(imageName));
        }

        private void UpdateThread(object argument)
        {
            var model = argument as IPanelModel;
            if (model == null) return;
            var fillerResult = model.RetrieveData(RetrieveMode.Async, m_ClearFilter);
            App.MainView.SafeInvoke(new SetFillerResultDelegate(UpdateThreadInvoked), model, fillerResult);
            m_SetTabImageThread.Interrupt();
        }

        private void UpdateThreadInvoked(IPanelModel model, PanelFillerResult fillerResult)
        {
            if (model != null)
                model.SetFillerResult(fillerResult, m_ClearFilter);
        }
    }
}
