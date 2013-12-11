using System;
using System.Text;
using LanExchange.SDK;
using LanExchange.SDK.Presenter;
using LanExchange.SDK.UI;

namespace LanExchange.Plugin.Notify
{
    public class PluginNotify : IPlugin
    {
        private const int LANEX_PORT = 3333;
        private const string MSG_LANEX_NOTIFY = "LANEX:NOTIFY";

        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;

            var disposableManger = (IDisposableManager) serviceProvider.GetService(typeof (IDisposableManager));
            if (disposableManger != null)
            {
                var listener = new UdpListener(LANEX_PORT);
                listener.MessageReceived += OnMessageReceived;
                disposableManger.RegisterInstance(listener);
            }
        }

        private void OnMessageReceived(object sender, MessageEventArgs e)
        {
            var msg = Encoding.Default.GetString(e.Data).Split('|');
            switch (msg[0])
            {
                case MSG_LANEX_NOTIFY:
                    var pagesView = (IPagesView)m_Provider.GetService(typeof(IPagesView));
                    if (pagesView != null)
                        ReReadPlugin(msg[1], msg.Length > 2 ? msg[2] : String.Empty);
                    break;
            }
        }

        private void ReReadPlugin(string typeName, string subject)
        {
            var pagesPresenter = (IPagesPresenter)m_Provider.GetService(typeof(IPagesPresenter));
            var mainView = (IMainView)m_Provider.GetService(typeof(IMainView));
            if (pagesPresenter == null || mainView == null || pagesPresenter.Count == 0) return;
            lock (pagesPresenter)
                for (int index = 0; index < pagesPresenter.Count; index++)
                {
                    var model = pagesPresenter.GetItem(index);
                    var parent = model.CurrentPath.Peek();
                    if (parent == null) continue;
                    if (parent.GetType().Name.Equals(typeName))
                        if (parent.IsRereadAccepted(subject))
                        {
                            model.AsyncRetrieveData(false);
                            break;
                        }
                }
        }
    }
}
