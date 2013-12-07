using System;
using System.Windows;
using LanExchange.SDK.UI;

namespace LanExchange.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application,  IAppPresenter
    {
        public void ApplicationRun(IMainView view)
        {
            InitializeComponent();
            Run();
        }

        [STAThread]
        void AppStartup(object sender, StartupEventArgs args)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        public void TranslateOpenForms()
        {
        }
    }
}
