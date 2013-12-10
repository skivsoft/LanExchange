using System;
using System.Windows;
using LanExchange.SDK.UI;
using LanExchange.SDK.Presenter;

namespace LanExchange.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class AppPresenter : Application,  IAppPresenter
    {
        private Window m_Window;

        public void Init()
        {
            InitializeComponent();
        }
        
        public void Run(IMainView view)
        {
            m_Window = view as Window;
            Run();
        }

        [STAThread]
        void AppStartup(object sender, StartupEventArgs args)
        {
            if (m_Window != null)
                m_Window.Show();
        }

        public void TranslateOpenForms()
        {
        }
    }
}
