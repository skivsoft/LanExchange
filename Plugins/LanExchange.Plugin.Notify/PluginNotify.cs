using System;
using System.ComponentModel.Composition;
using System.Text;
using LanExchange.SDK;
using LanExchange.SDK.Extensions;
using System.Linq;

namespace LanExchange.Plugin.Notify
{
    [Export(typeof(IPlugin))]
    public class PluginNotify : IPlugin
    {
        private const int LANEX_PORT = 3003;
        private const string MSG_LANEX_NOTIFY = "LANEX:NOTIFY";

        private IServiceProvider serviceProvider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            var disposableManger = serviceProvider.Resolve<IDisposableManager>();
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
                    var pagesView = serviceProvider.Resolve<IPagesView>();
                    if (pagesView != null)
                        ReReadPlugin(msg[1], msg.Length > 2 ? msg[2] : String.Empty);
                    break;
            }
        }

        private void ReReadPlugin(string typeName, string subject)
        {
            var pagesPresenter = serviceProvider.Resolve<IPagesPresenter>();
            var mainView = serviceProvider.Resolve<IMainView>();
            if (pagesPresenter == null || mainView == null || pagesPresenter.Count == 0) return;
            lock (pagesPresenter)
                for (int index = 0; index < pagesPresenter.Count; index++)
                {
                    var model = pagesPresenter.GetItem(index);
                    var parent = model.CurrentPath.Peek();
                    if (parent.Where(item => item.GetType().Name.Equals(typeName) && item.IsRereadAccepted(subject)).Any())
                    {
                        model.AsyncRetrieveData(false);
                        break;
                    }
                }
        }
    }
}
